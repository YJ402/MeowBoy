using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum UIState
{
    Main_Game =0,
    Mini_Game =5,
    Mini_GameOver,

}

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI message;

    [SerializeField] private BaseUI[] UIs;

    //MiniGameUI gameUI;
    //MiniGameOverUI gameOverUI;
    //MainGameUI mainGameUI;
    private UIState currentState;

    private void Awake()
    {
        Instance = this;

        if (UIs != null)
        {
            foreach (var ui in UIs)
            {
                ui?.Init(this);
            }
        }
    }

    public TextMeshProUGUI GetMessageObject()
    {
        return message;
    }

    public void ChangeState(UIState state)
    {
        currentState = state;

        foreach (var ui in UIs)
        {
            ui.SetActive(state);
        }
        //gameUI.SetActive(state);
        //gameOverUI.SetActive(state);
        //mainGameUI.SetActive(state);
    }
}
