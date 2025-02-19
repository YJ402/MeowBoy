using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinigameMapHandler : MonoBehaviour
{
    public GameObject mapPrefab;

    GameObject standard;
    Tilemap floorTile;
    BoundsInt bound;
    float mapWidth;

    public Action<Collider2D> action;

    private void Start()
    {
        action += DestroyMap;
        action += CreateMap;

        standard = (mapPrefab.transform.GetChild(0).gameObject);
        floorTile = standard.GetComponent<Tilemap>();
        bound = floorTile.cellBounds;
        mapWidth = bound.size.x;
    }


    public void CreateMap(Collider2D lastMap)
    {
        //standard = (lastMap.transform.GetChild(0).gameObject);
        //floorTile = standard.GetComponent<Tilemap>();
        // bound = floorTile.cellBounds;
        float newMapX = 2*mapWidth + lastMap.transform.position.x;

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

    public void CreateDestroyMap(Collider2D collision)
    {
        action?.Invoke(collision);
    }
}
