using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManger : MonoBehaviour
{
    PlayerSpawnHandler playerSpawnHandler;
    Transform playerSpawnLocation;

    CameraSpawnHandler cameraSpawnHandler;

    private void Awake()
    {
        playerSpawnHandler = GetComponent<PlayerSpawnHandler>();
        //playerSpawnLocation = GetComponentInChildren<GameObject>();
        playerSpawnLocation = transform.GetChild(0);
        cameraSpawnHandler = GetComponent<CameraSpawnHandler>();
        cameraSpawnHandler.SpawnCamera();

    }

    private void Start()
    {

        playerSpawnHandler.SpawnPlayer(playerSpawnLocation.transform.position);

    }
}
