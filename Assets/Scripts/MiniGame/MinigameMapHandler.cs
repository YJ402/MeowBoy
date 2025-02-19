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
        //난이도 계산기 = 맵 면적 등등 고려
        // 1. 맵의 밀도: 난이도가 높아질수록 밀도를 높여.(로그 함수로 높아지게)
        /////////////// 2. 생존 구멍: 난이도가 높아져도 최소한의 생존구멍은 있어야해. >> 지금은 좀 짜기 어려워 보임.
        //3. 장애물과 코인의 비율: 비슷하게 유지해도 될듯? 8:2 정도? obstacleToCoinRatio
        //4. 기타 계산에 쓰일 요소들: 맵의 면적(mapWidth * mapHeight), 

        float mapDensity = (difficulty * densityMultiplier * Mathf.Log(difficulty + 5, 2)) / 100; // 1스테이지일때는 밀도가 대략 20, 30 스테이지일때는 밀도가 대략 50정도의 둔화곡선.
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
