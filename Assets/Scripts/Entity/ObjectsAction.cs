using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class ObjectsAction : MonoBehaviour
{
    InteractionHandler interactionHandler;

    //[SerializeField] private TextMeshPro popup;
    private TextMeshProUGUI message;

    [SerializeField] private bool onMessage;
    [SerializeField] private string messageText;
    public string MessageText { get { return messageText; } }
    public bool OnMessage { get { return onMessage; } }
    [SerializeField] private float messageFontSize;
    public float MessageFontSize { get { return messageFontSize; } }
    [SerializeField] private float messagePosY;
    public float MessagePosY { get { return messagePosY; } }
    private void Start()
    {
        interactionHandler = GetComponentInParent<InteractionHandler>();
        message = UIManager.Instance.GetMessageObject();
    }

    public virtual void Init()
    {
        interactionHandler.Activate += Active;
        interactionHandler.Act += Act;
        interactionHandler.DeActivate += DeActive;
    }
    public virtual void Active()
    {
        if (onMessage)
            DisplayMessage();
    }

    public virtual void Act()
    {
    }

    public virtual void DeActive()
    {
        if (onMessage)
            RemoveMessage();
    }

    private void DisplayMessage()
    {
        message.text = MessageText;
        message.fontSize = MessageFontSize;
        message.transform.position = new Vector2(transform.parent.position.x, transform.parent.position.y + MessagePosY);
        message.gameObject.SetActive(true);
    }

    private void RemoveMessage()
    {
        message.gameObject.SetActive(false);
    }
}
