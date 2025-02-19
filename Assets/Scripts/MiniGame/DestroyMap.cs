using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMap : MonoBehaviour
{
    public MinigameMapHandler minigameMapHandler;

    private void Start()
    {
        minigameMapHandler = FindObjectOfType<MinigameMapHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
            minigameMapHandler.CreateDestroyMap(collision);
    }
}
