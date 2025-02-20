using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxAction : ObjectsAction
{


    public override void Act()
    {
        base.Act();
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return null;  
        SceneManager.LoadScene("MiniGameScene");
        GameManager.Instance.currentScene = GameManager.SceneType.MiniGameScene;
        UIManager.Instance.ChangeState(UIState.Mini_Game);
        Debug.Log(GameManager.Instance.currentScene);
    }


}
