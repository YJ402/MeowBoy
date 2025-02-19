using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private Camera camera;
    CameraController cameraController;
    
    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
        cameraController = camera.gameObject.GetComponent<CameraController>();
    }

    protected override void Update()
    {
        base.Update();
        cameraController.FollowPlayer(transform.position);
        
    }
    //protected override void HandleAction() // 플레이어만 마우스 방향에 반응해서 movement/look방향 결정
    //{
    //    float horizontal = Input.GetAxisRaw("Horizontal");
    //    float vertical = Input.GetAxisRaw("Vertical");
    //    movementDirection = new Vector2(horizontal, vertical).normalized;

    //    Vector2 mousePosition = Input.mousePosition;
    //    Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
    //    lookDirection = (worldPos - (Vector2)transform.position); // look방향은 플레이어 위치에서 상대적 마우스 위치로 결정

    //    if (lookDirection.magnitude < .9f) // Vector.magnitude: 벡터의 크기. 조건문: 벡터의 크기가 너무 작다면, 0으로 하고, 아니라면 정규화하여 방향을 남김.
    //    {
    //        lookDirection = Vector2.zero;
    //    }
    //    else
    //    {
    //        lookDirection = lookDirection.normalized;
    //    }
    //}

    void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>().normalized;
    }

    void OnLook(InputValue inputValue)
    {
        //Vector2 mousePosition = Input.mousePosition;
        //Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
        //lookDirection = (worldPos - (Vector2)transform.position); // look방향은 플레이어 위치에서 상대적 마우스 위치로
        Vector2 worldPos = camera.ScreenToWorldPoint(inputValue.Get<Vector2>());
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < .9f) // Vector.magnitude: 벡터의 크기. 조건문: 벡터의 크기가 너무 작다면, 0으로 하고, 아니라면 정규화하여 방향을 남김.
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }

    void OnFire(InputValue inputValue)
    {
        //if (EventSystem.current.IsPointerOverGameObject()) // ?
        //    return;

        isAttacking = inputValue.isPressed;
    }
}
