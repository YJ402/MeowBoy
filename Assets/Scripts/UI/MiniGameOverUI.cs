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

    [SerializeField] TextMeshProUGUI currentScore;
    [SerializeField] TextMeshProUGUI bestScore;

    // Start is called before the first frame update
    public override void Init(UIManager _uimanager)
    {
        base.Init(_uimanager);
        RetryButton.onClick.AddListener(OnClickRetryButton);
        ExitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnClickRetryButton()
    {
        GameManager.Instance.LoadScene(1);
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

    public override void UpdateInfo()
    {
        //Debug.Log("2-2¹ø.");
        //if (currentScore == null)
        //    Debug.Log("#currentScore ³Î");
        //if (bestScore == null)
        //    Debug.Log("#bestScore ³Î");
        currentScore.text = MiniGameManager.M_instance.Matchsocre.ToString();
        bestScore.text = PlayerPrefs.GetInt("MinibestScore").ToString();
    }
}
