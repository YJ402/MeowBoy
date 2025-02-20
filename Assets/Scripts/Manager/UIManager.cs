using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum UIState
{
    Main_Game,
    Mini_GameOver,
    Mini_Game
}

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI message;

    MiniGameUI gameUI;
    MiniGameOverUI gameOverUI;
    MainGameUI mainGameUI;
    private UIState currentState;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        gameUI = GetComponentInChildren<MiniGameUI>(true); // true: 비활성화된 오브젝트도 탐색 범위에 포함
        gameOverUI = GetComponentInChildren<MiniGameOverUI>(true);
        mainGameUI = GetComponentInChildren<MainGameUI>(true);

        gameUI.Init(this);
        gameOverUI.Init(this);
        mainGameUI.Init(this);

        ChangeState(UIState.Mini_Game);
    }

    public TextMeshProUGUI GetMessageObject()
    {
        return message;
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        gameUI.SetActive(state);
        gameOverUI.SetActive(state);
        mainGameUI.SetActive(state);
    }
}
