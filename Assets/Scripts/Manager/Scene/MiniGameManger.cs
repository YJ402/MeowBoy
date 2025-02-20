using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManger : _SceneManager
{
    public static MiniGameManger M_instance;

    PlayerSpawnHandler playerSpawnHandler;
    Transform playerSpawnLocation;

    CameraSpawnHandler cameraSpawnHandler;

    MinigameMapHandler minigameMapHandler;

    int Matchsocre;

    [SerializeField] private GameObject destroyLinePrefab;


    [SerializeField] private TextMeshProUGUI coin;

    [SerializeField] private TextMeshProUGUI currentCoin;
    [SerializeField] private TextMeshProUGUI bestCoint;


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

    protected void Start()
    {
        playerSpawnHandler.SpawnPlayer(playerSpawnLocation.transform.position);
        Instantiate(destroyLinePrefab, Camera.main.transform);
    }

    public void AddScore(int score)
    {
        Matchsocre += score;
        Debug.Log($"{Matchsocre}");
        coin.text = score.ToString();
    }

    public void Die()
    {
        int temp;

        //플레이어의 지갑에 추가
        GameManager.Instance.AddScore(Matchsocre);

        //최고점수 기록
        temp = PlayerPrefs.HasKey("MinibestScore") ? PlayerPrefs.GetInt("MinibestScore") : int.MinValue;
        if (temp < Matchsocre)
        {
            PlayerPrefs.SetInt("MinibestScore", Matchsocre);
        }

        //종료 UI호출
        UIManager.Instance.ChangeState(UIState.Mini_GameOver);
    }
}
