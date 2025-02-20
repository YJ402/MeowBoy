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

        GameManager.Instance.LoadScene(1);
        //StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return null;  
        GameManager.Instance.LoadScene(1);
        //UIManager.Instance.ChangeState(UIState.Mini_Game);
    }


}
