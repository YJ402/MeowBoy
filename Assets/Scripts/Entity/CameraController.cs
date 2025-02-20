using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraController : MonoBehaviour
{
    //bool isEndOfMap = false;

    //public GameObject levelCollision;

    //Collider2D top;
    //Collider2D bottom;
    //Collider2D left;
    //Collider2D right;

    bool endOfLeft = false;
    bool endOfRight = false;
    bool endOfTop = false;
    bool endOfBottom = false;

    Vector3 lastPosition = Vector3.zero;
    Vector3 expectedPosition = Vector3.zero;




    //private void Start()
    //{
    //    levelCollision = FindAnyObjectByType().
    //}
    public void FollowPlayer(Vector3 playerPosition)
    {
        expectedPosition = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);

        if ((endOfLeft && lastPosition.x > expectedPosition.x)|| (endOfRight && lastPosition.x < expectedPosition.x))
            expectedPosition.x = lastPosition.x;

        if ((endOfTop && lastPosition.y < expectedPosition.y)|| (endOfBottom && lastPosition.y > expectedPosition.y))
            expectedPosition.y = lastPosition.y;

        //if (endOfLeft && lastPosition.x > expectedPosition.x)
        //    expectedPosition.x = lastPosition.x;
        //if (endOfRight && lastPosition.x < expectedPosition.x)
        //    expectedPosition.x = lastPosition.x;
        //if (endOfTop && lastPosition.y < expectedPosition.y)
        //    expectedPosition.y = lastPosition.y;
        //if (endOfBottom && lastPosition.y > expectedPosition.y)
        //    expectedPosition.y = lastPosition.y;

        transform.position = expectedPosition;

        lastPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
            endOfLeft = true;
        else if(collision.gameObject.layer == 10)
            endOfRight = true;

        if (collision.gameObject.layer == 11)
            endOfBottom = true;
        else if (collision.gameObject.layer == 12)
            endOfTop = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
            endOfLeft = false;
        else if (collision.gameObject.layer == 10)
            endOfRight = false;

        if (collision.gameObject.layer == 11)
            endOfBottom = false;
        else if (collision.gameObject.layer == 12)
            endOfTop = false;
    }
}
