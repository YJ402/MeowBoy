using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class DeadLineController : MonoBehaviour
{
    Transform parent;
    [SerializeField] float secondsPerMap = 5f;
    float speed;
    ResourceController resourceController;
    PlayerController playerController;
    private void Start()
    {
        speed = 34 / secondsPerMap;
        parent = transform.parent;
    }
    void Update()
    {
        parent.position = new Vector2(parent.position.x + speed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.TryGetComponent<PlayerController>(out playerController) && playerController.Alive)
        {

            resourceController = collision.gameObject .GetComponent<ResourceController>();
            resourceController.ChangeHealth(-float.MaxValue);
            MiniGameManager.M_instance.DieEvent();
        }
    }
}
