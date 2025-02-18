using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Range(1f, 100f)][SerializeField] private int health = 10;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = Mathf.Clamp(value, 0, 20);
        }
    }
    [Range(1f, 20f)][SerializeField] private float speed = 3;
    public float Speed
    {
        get { return speed; }
        set => speed = Mathf.Clamp(value, 0, 20); // 한줄 코드면 람다가 더 편함
    }
}
