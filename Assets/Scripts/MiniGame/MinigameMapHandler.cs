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
        //floorTile.CompressBounds(); // 타일맵에 남아있는 공백을 바운더리에서 없애주는 구문

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
        //난이도 계산기 = 맵 면적 등등 고려
        // 1. 맵의 밀도: 난이도가 높아질수록 밀도를 높여.(로그 함수로 높아지게)
        /////////////// 2. 생존 경로: 난이도가 높아져도 최소한의 생존경로는 있어야해. >> 지금은 좀 짜기 어려워 보임.
        //3. 장애물과 코인의 비율: 8:2 정도? obstacleToCoinRatio
        //4. 기타 계산에 쓰일 요소들: 맵의 면적(mapWidth * mapHeight), 

        // 기본 시작값 조정. 
        float baseValue = 0.08f;  // 초기값 약 50개 유지
        // x = 2.5, y = 1.5로 설정하여 로그 곡선의 기울기를 조절
        float logComponent = Mathf.Log(2.5f, 1.5f);
        // Sigmoid 스타일의 보정 적용 - 스케일 다운
        float correction = 0.8f / (1f + Mathf.Exp(-difficulty / 40f));
        // 최종 밀도 계산
        float density = baseValue + ((difficulty * logComponent * correction) / 100f);
        // 최대값 제한 (약 300개 오브젝트에 해당하는 밀도) // 200개로 제한하려면 0.316f;
        float mapDensity = Mathf.Min(density, 0.47f);
        //Thanks To Claude
        //difficulty : 오브젝트 - 1: 50 - 100 : 200 - 200: 260

        float mapSize = mapWidth * mapHeight;
        int obstaclesCount = (int)(mapSize * mapDensity * (1- coinToObstacleRatio)); 
        int coinsCount = (int)(mapSize * mapDensity * coinToObstacleRatio);

        GameObject temp;

        for (int i = 0; i < obstaclesCount; i++)
        {
            int rand = Random.Range(0, obstaclePrefab.Length);
            temp = Instantiate(obstaclePrefab[rand]);
            temp.transform.SetParent(newMap);
            obstacles.Enqueue(temp); // queue할 필요 없겠네.
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

            //이미 위치한 지점인지 체크
            if (matrix[keyRow, keyColumn] == false)
            {
                //위치 시키기
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
                //무효로 하고 한바퀴 돌기.
                continue;
            }
            i++;
        }
    }
}
