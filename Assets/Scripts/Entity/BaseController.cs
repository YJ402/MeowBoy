using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0f;

    protected AnimationHandler animationHandler;

    protected StatHandler statHandler;

    //게임에서 사용할 무기를 담는 변수
    [SerializeField] public WeaponHandler weaponPrefab; 
    protected WeaponHandler weaponHandler;

    protected bool isAttacking;
    private float timeSinceLastAttack = float.MaxValue; // 왜 맥스로 초기화한 이유: 게임 시작하자마자 공격이 가능하게 하려고.

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        //statHandler = GetComponent<StatHandler>();
        //statHandler = gameObject.AddComponent<StatHandler>(); //왜 Add지?  이거 때매 플레이어에 스탯 두개 붙음
        statHandler = GetComponent<StatHandler>(); 


        if (weaponPrefab != null) // 게임 상에서 다룰 무기를 정하는 로직.
            weaponHandler = Instantiate(weaponPrefab, weaponPivot); // 인스펙터 상에서 할당돼있는 프리팹이 있으면 그걸로 정하고,
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>(); // 자식객체에 무기가 있으면 그걸로 정함.
    }

    protected virtual void Start() // 가상, 추상 메서드는 당연히 private일 수 없음.
    {

    }

    protected virtual void Update()
    {
        //HandleAction();
        Rotate(lookDirection);
        HandleAttackDelay();
    }

    protected void FixedUpdate()
    {
        Movement(MovementDirection);
        if (knockbackDuration > 0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    //protected virtual void HandleAction()
    //{

    //}

    private void Movement(Vector2 direction)
    {
        direction = direction * statHandler.Speed; // statHandler의 값을 가져옴. C#이랑 달리 유니티에선 스크립트 하나를 복사할 수가 있네. 
        if (knockbackDuration > 0f)
        {
            direction *= 0.5f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f; //양의 x축이 0도를 하는 것을 기준으로 반시계(+), 시계(-)방향으로 각도가 계산됨. -90° < rotZ < 90° 은 벡터가 오른쪽 반구에 위치하는 것을 의미.

        characterRenderer.flipX = isLeft;

        if (weaponPivot = null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }

        weaponHandler?.Rotate(isLeft); // 
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
        // 공격자의 위치 - 피격자의 위치 = 피격자가 공격자를 바라보는 벡터. (-)를 붙여서 벡터 방향을 반대로 설정. 
        // normalized: 벡터의 크기를 1로 정규화하면, 벡터의 크기는 사라지고 방향만 남음.
    }

    private void HandleAttackDelay() // 
    {
        if (weaponHandler == null)
            return;

        if (timeSinceLastAttack <= weaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if(isAttacking && timeSinceLastAttack > weaponHandler.Delay)
        {
            timeSinceLastAttack = 0; // 왜 맥스가 아니고 0일까?
            Attack();
        }
    }

    protected virtual void Attack()
    {
        if(lookDirection != Vector2.zero)
            weaponHandler.Attack();
    }
}
