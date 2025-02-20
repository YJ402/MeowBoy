using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGameOverUI : BaseUI
{
    [SerializeField] private Button RetryButton;
    [SerializeField] private Button ExitButton;



    // Start is called before the first frame update
    public override void Init(UIManager _uimanager)
    {
        base.Init(_uimanager);
        RetryButton.onClick.AddListener(OnClickRetryButton);
        ExitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnClickRetryButton()
    {
        GameManager.Instance.LoadScene(5);
        UIManager.Instance.ChangeState(UIState.Mini_Game);
    }

    private void OnClickExitButton()
    {
        GameManager.Instance.LoadScene(0);
        UIManager.Instance.ChangeState(UIState.Main_Game);
    }

    protected override UIState GetUIState()
    {
        return UIState.Mini_GameOver;
    }
}
