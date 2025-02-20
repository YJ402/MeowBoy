using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    MainGameScene = 0,
    MiniGameScene=5
}

public class GameManager : MonoBehaviour
{




    public static GameManager Instance;

    private int totalScore;
    public int TotalScore {  get { return totalScore; } }

    public SceneType currentScene = SceneType.MainGameScene;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        totalScore += score;
    }

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
        GameManager.Instance.currentScene = (SceneType)i;
        UIManager.Instance.ChangeState((UIState)i);
    }
}
