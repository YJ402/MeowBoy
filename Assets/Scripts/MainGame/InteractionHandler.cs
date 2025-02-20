using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

//[System.Serializable]
//public class KeyAction
//{
//    public KeyCode key;
//    public Act action;
//}
public class InteractionHandler : MonoBehaviour
{
    Animator animator;

    ObjectsAction objectsAction;
    //[SerializeField] List<KeyAction> actions = new List<KeyAction>();
    //[SerializeField] Dictionary<KeyCode, GameObject> Action;
    //[SerializeField] GameObject[] Action2;

    public Action Act;
    public Action Activate;
    public Action DeActivate;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        objectsAction = GetComponentInChildren<ObjectsAction>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            ActiveAnimation();
            objectsAction.Init();
            Activate?.Invoke();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Act?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            DeActiveAnimation();
            DeActivate?.Invoke();
        }
    }
    void ActiveAnimation()
    {
        animator.SetBool("IsActive", true);
    }

    void DeActiveAnimation()
    {
        animator.SetBool("IsActive", false);
    }
}
