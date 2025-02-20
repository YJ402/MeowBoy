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
        gameObject.SetActive(GetUIState() == state); // 커렌트 UI가 아닌 애들은 알아서 꺼지도록.
    }
}
