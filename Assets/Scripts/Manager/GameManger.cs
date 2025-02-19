using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger Instance;

    private int totalScore;
    public int TotalScore {  get { return totalScore; } }

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
