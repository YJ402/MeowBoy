using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManger : MonoBehaviour
{
    public static MiniGameManger M_instance;

    PlayerSpawnHandler playerSpawnHandler;
    Transform playerSpawnLocation;

    CameraSpawnHandler cameraSpawnHandler;

    MinigameMapHandler minigameMapHandler;

    int Matchsocre;

    private void Awake()
    {
        M_instance = this;

        playerSpawnHandler = GetComponent<PlayerSpawnHandler>();
        //playerSpawnLocation = GetComponentInChildren<GameObject>();
        playerSpawnLocation = transform.GetChild(0);
        cameraSpawnHandler = GetComponent<CameraSpawnHandler>();
        cameraSpawnHandler.SpawnCamera();

        minigameMapHandler = GetComponent<MinigameMapHandler>();

    }

    private void Start()
    {
        playerSpawnHandler.SpawnPlayer(playerSpawnLocation.transform.position);
    }

    public void AddScore(int score)
    {
        Matchsocre += score;
        Debug.Log($"{Matchsocre}");
    }
    private void EndMiniGame(int Matchsocre)
    {
        GameManger.Instance.AddScore(Matchsocre);
    }
}
