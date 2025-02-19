using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinigameMapHandler : MonoBehaviour
{
    public GameObject mapPrefab;

    GameObject standard;
    Tilemap floorTile;
    BoundsInt bound;
    float mapWidth;
    float mapHeight;

    int difficulty = 0;

    [SerializeField] private GameObject[] obstacle;
    [SerializeField] private GameObject[] coin;

    private Queue<GameObject> obstacles;
    private Queue<GameObject> coins;

    public Action<Collider2D> action;

    [SerializeField] private float densityMultiplier = 1f;
    [SerializeField] private float obstacleToCoinRatio = 0.2f;

    private void Start()
    {
        action += DestroyMap;
        action += CreateMap;

        standard = (mapPrefab.transform.GetChild(0).gameObject);
        floorTile = standard.GetComponent<Tilemap>();
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

        Instantiate(mapPrefab, new Vector2(newMapX, 0), Quaternion.identity);
    }

    void DestroyMap(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            GameObject parent = collision.gameObject.transform.parent.gameObject;
            Destroy(parent);
        }
    }



    void CreateRandomObjects()
    {
        //���̵� ���� = �� ���� ��� ���
        // 1. ���� �е�: ���̵��� ���������� �е��� ����.(�α� �Լ��� ��������)
        /////////////// 2. ���� ����: ���̵��� �������� �ּ����� ���������� �־����. >> ������ �� ¥�� ����� ����.
        //3. ��ֹ��� ������ ����: ����ϰ� �����ص� �ɵ�? 8:2 ����? obstacleToCoinRatio
        //4. ��Ÿ ��꿡 ���� ��ҵ�: ���� ����(mapWidth * mapHeight), 

        float mapDensity = (difficulty * densityMultiplier * Mathf.Log(difficulty + 5, 2)) / 100; // 1���������϶��� �е��� �뷫 20, 30 ���������϶��� �е��� �뷫 50������ ��ȭ�.
        float mapSize = mapWidth * mapHeight;
        int obstaclesCount = (int)(mapSize * mapDensity * 0.8);
        int coinsCount = (int)(mapSize * mapDensity * 0.2);
    }

    void PlaceObjects()
    {

    }

    void DestroyObjects(int numOfLastObstacles, int numOfLastCoins)
    {
        for(int i = 0; i < numOfLastObstacles; i++)
        {
            Destroy(obstacles.Dequeue());
        }
        for (int i = 0; i < numOfLastCoins; i++)
        {
            Destroy(coins.Dequeue());
        }
    }
}
