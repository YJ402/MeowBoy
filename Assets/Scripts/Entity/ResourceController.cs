using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChangeHealthResultType
{
    fail,
    success_alive,
    success_die,
    success_alreadydie,
}
public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHandler animationHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth {  get; private set; }
    public float MaxHealth => statHandler.Health;


    private void Awake()
    {
        statHandler = GetComponent<StatHandler>();
        baseController = GetComponent<BaseController>();    
        animationHandler = GetComponent<AnimationHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = statHandler.Health; 
    }

    // Update is called once per frame
    void Update()
    {
        if(timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if(timeSinceLastChange >= healthChangeDelay)
            {
                animationHandler.InvincibilityEnd();
            }
        }
    }

    public ChangeHealthResultType ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
            return ChangeHealthResultType.fail;

        timeSinceLastChange = 0;    
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if(change< 0)
        {
            animationHandler.Damage();
        }

        if (CurrentHealth <= 0 && baseController.Alive)
        {
            Death();
            return ChangeHealthResultType.success_die;
        }
        else { return ChangeHealthResultType.success_alreadydie; }


        Debug.Log("남은 체력: " + CurrentHealth);

        return ChangeHealthResultType.success_alive;

    }

    private void Death()
    {
        animationHandler.Death();
        baseController.Die();
    }
}
