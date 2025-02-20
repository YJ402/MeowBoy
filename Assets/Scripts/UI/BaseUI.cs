using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uimanager;

    public virtual void Init(UIManager _uimanager)
    {
        uimanager = _uimanager;
        gameObject.SetActive(false);
    }

    protected abstract UIState GetUIState();
    public void SetActive(UIState state)
    {
        gameObject.SetActive(GetUIState() == state); // Ŀ��Ʈ UI�� �ƴ� �ֵ��� �˾Ƽ� ��������.
    }
}
