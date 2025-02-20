using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uimanager;

    public virtual void Init(UIManager _uimanager)
    {
        uimanager = _uimanager;
    }

    protected abstract UIState GetUIState();
    public void SetActive(UIState state)
    {
        if(GetUIState() == state)
        {
            gameObject.SetActive(true);
            UpdateInfo();
        }
        else { gameObject.SetActive(false);  }
    }

    public virtual void UpdateInfo()
    {
    }

}
