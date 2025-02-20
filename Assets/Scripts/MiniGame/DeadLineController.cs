using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLineController : MonoBehaviour
{
    Transform parent;
    [SerializeField] float secondsPerMap = 5f;
    float speed;
    private void Start()
    {
        speed = 34 / secondsPerMap;
        parent = transform.parent;
    }
    void Update()
    {
        parent.position = new Vector2(parent.position.x + speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MiniGameManger.M_instance.Die();
    }


}
