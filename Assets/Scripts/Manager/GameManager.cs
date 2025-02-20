using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum SceneType
    {
        MainScene,
        MiniGameScene

    }
    public static GameManager Instance;

    private int totalScore;
    public int TotalScore {  get { return totalScore; } }

    public SceneType currentScene = SceneType.MiniGameScene;

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
}
