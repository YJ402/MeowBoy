using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


public class MinigameMapHandler : MonoBehaviour
{
    public GameObject mapPrefab;

    GameObject standard;
    Tilemap floorTile;
    BoundsInt bound;
    float mapWidth;
    float mapHeight;

    int difficulty = 1;

    [SerializeField] private GameObject[] obstaclePrefab;
    [SerializeField] private GameObject[] coinPrefab;

    private Queue<GameObject> obstacles = new Queue<GameObject>();
    private Queue<GameObject> coins = new Queue<GameObject>();

    public Action<Collider2D> action;

    [SerializeField] private float densityMultiplier = 1f;
    [SerializeField] private float coinToObstacleRatio;

    private void Start()
    {
        action += DestroyMap;
        action += CreateMap;

        standard = (mapPrefab.transform.GetChild(0).gameObject);
        floorTile = standard.GetComponent<Tilemap>();
        //floorTile.CompressBounds(); // Ÿ�ϸʿ� �����ִ� ������ �ٿ�������� �����ִ� ����

        bound = floorTile.cellBounds;

        mapWidth = bound.size.x;
        mapHeight = bound.size.y;
    }

    public void CreateDestroyMap(Collider2D collision)
    {
        action?.Invoke(collision);
        difficulty++;
    }

    public void CreateMap(Collider2D lastMap)
    {
        //standard = (lastMap.transform.GetChild(0).gameObject);
        //floorTile = standard.GetComponent<Tilemap>();
        // bound = floorTile.cellBounds;
        float newMapX = 2 * mapWidth + lastMap.transform.position.x;

        GameObject newMap = Instantiate(mapPrefab, new Vector2(newMapX, 0), Quaternion.identity);

        CreateRandomObjects(newMap.transform);
        PlaceObjects(newMap.transform);
    }

    void DestroyMap(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            GameObject parent = collision.gameObject.transform.parent.gameObject;
            Destroy(parent);
        }
    }



    void CreateRandomObjects(Transform newMap)
    {
        //���̵� ���� = �� ���� ��� ���
        // 1. ���� �е�: ���̵��� ���������� �е��� ����.(�α� �Լ��� ��������)
        /////////////// 2. ���� ���: ���̵��� �������� �ּ����� ������δ� �־����. >> ������ �� ¥�� ����� ����.
        //3. ��ֹ��� ������ ����: 8:2 ����? obstacleToCoinRatio
        //4. ��Ÿ ��꿡 ���� ��ҵ�: ���� ����(mapWidth * mapHeight), 

        // �⺻ ���۰� ����. 
        float baseValue = 0.08f;  // �ʱⰪ �� 50�� ����
        // x = 2.5, y = 1.5�� �����Ͽ� �α� ��� ���⸦ ����
        float logComponent = Mathf.Log(2.5f, 1.5f);
        // Sigmoid ��Ÿ���� ���� ���� - ������ �ٿ�
        float correction = 0.8f / (1f + Mathf.Exp(-difficulty / 40f));
        // ���� �е� ���
        float density = baseValue + ((difficulty * logComponent * correction) / 100f);
        // �ִ밪 ���� (�� 300�� ������Ʈ�� �ش��ϴ� �е�) // 200���� �����Ϸ��� 0.316f;
        float mapDensity = Mathf.Min(density, 0.47f);
        //Thanks To Claude
        //difficulty : ������Ʈ - 1: 50 - 100 : 200 - 200: 260

        float mapSize = mapWidth * mapHeight;
        int obstaclesCount = (int)(mapSize * mapDensity * (1- coinToObstacleRatio)); 
        int coinsCount = (int)(mapSize * mapDensity * coinToObstacleRatio);

        GameObject temp;

        for (int i = 0; i < obstaclesCount; i++)
        {
            int rand = Random.Range(0, obstaclePrefab.Length);
            temp = Instantiate(obstaclePrefab[rand]);
            temp.transform.SetParent(newMap);
            obstacles.Enqueue(temp); // queue�� �ʿ� ���ڳ�.
        }

        for (int i = 0; i < coinsCount; i++)
        {
            int rand = Random.Range(0, coinPrefab.Length);
            temp = Instantiate(coinPrefab[rand]);
            temp.transform.SetParent(newMap);
            coins.Enqueue(temp);
        }

    }

    void PlaceObjects(Transform newMap)
    {
        List<int> column = new List<int>();
        List<int> row = new List<int>();
        bool[,] matrix = new bool[(int)mapHeight, (int)mapWidth];
        int rand = 0;
        int keyColumn;
        int keyRow;

        for (int i = 0; i < mapWidth; i++)
        {
            column.Add(i);
        }

        for (int i = 0; i < mapHeight; i++)
        {
            row.Add(i);
        }

        int condition = obstacles.Count + coins.Count;
        for (int i = 0; i < condition;)
        {
            rand = Random.Range(0, column.Count);
            keyColumn = column[rand];

            rand = Random.Range(0, row.Count);
            keyRow = row[rand];

            //�̹� ��ġ�� �������� üũ
            if (matrix[keyRow, keyColumn] == false)
            {
                //��ġ ��Ű��
                if (obstacles.Count != 0)
                {
                    obstacles.Dequeue().transform.localPosition = new Vector2(keyColumn - mapWidth / 2f, keyRow - mapHeight / 2f);
                }
                else
                {
                    coins.Dequeue().transform.localPosition = new Vector2(keyColumn - mapWidth / 2f, keyRow - mapHeight / 2f);
                }

                matrix[keyRow, keyColumn] = true;
            }
            else
            {
                //��ȿ�� �ϰ� �ѹ��� ����.
                continue;
            }
            i++;
        }
    }
}
