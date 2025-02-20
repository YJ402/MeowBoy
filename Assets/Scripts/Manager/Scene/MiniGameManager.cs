using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : _SceneManager
{
    public static MiniGameManager M_instance;

    PlayerSpawnHandler playerSpawnHandler;
    Transform playerSpawnLocation;

    CameraSpawnHandler cameraSpawnHandler;

    MinigameMapHandler minigameMapHandler;

    ResourceController resourceController;

    public int Matchsocre;

    [SerializeField] private GameObject destroyLinePrefab;


    [SerializeField] private TextMeshProUGUI coin;

    [SerializeField] private TextMeshProUGUI currentCoin;
    [SerializeField] private TextMeshProUGUI bestCoin;

    GameObject player;

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
        player = playerSpawnHandler.SpawnPlayer(playerSpawnLocation.transform.position);
        resourceController = player.GetComponent<ResourceController>();
        Instantiate(destroyLinePrefab, Camera.main.transform);
        UIManager.Instance.ChangeState(UIState.Mini_Game);
    }

    public void AddScore(int score)
    {
        Matchsocre += score;
        //Debug.Log($"{Matchsocre}");
        coin.text = Matchsocre.ToString();
    }

    public void DieEvent()
    {
        Debug.Log("사망 이벤트 발생");

        //플레이어의 지갑에 추가
        GameManager.Instance.AddScore(Matchsocre);

        //최고점수 기록
        int temp = PlayerPrefs.HasKey("MinibestScore") ? PlayerPrefs.GetInt("MinibestScore") : int.MinValue;
        if (temp < Matchsocre)
        {
            PlayerPrefs.SetInt("MinibestScore", Matchsocre);
        }
        //종료 UI호출
        UIManager.Instance.ChangeState(UIState.Mini_GameOver);
    }
}
