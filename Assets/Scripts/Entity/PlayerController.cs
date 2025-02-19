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
    //protected override void HandleAction() // �÷��̾ ���콺 ���⿡ �����ؼ� movement/look���� ����
    //{
    //    float horizontal = Input.GetAxisRaw("Horizontal");
    //    float vertical = Input.GetAxisRaw("Vertical");
    //    movementDirection = new Vector2(horizontal, vertical).normalized;

    //    Vector2 mousePosition = Input.mousePosition;
    //    Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
    //    lookDirection = (worldPos - (Vector2)transform.position); // look������ �÷��̾� ��ġ���� ����� ���콺 ��ġ�� ����

    //    if (lookDirection.magnitude < .9f) // Vector.magnitude: ������ ũ��. ���ǹ�: ������ ũ�Ⱑ �ʹ� �۴ٸ�, 0���� �ϰ�, �ƴ϶�� ����ȭ�Ͽ� ������ ����.
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
        //lookDirection = (worldPos - (Vector2)transform.position); // look������ �÷��̾� ��ġ���� ����� ���콺 ��ġ��
        Vector2 worldPos = camera.ScreenToWorldPoint(inputValue.Get<Vector2>());
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < .9f) // Vector.magnitude: ������ ũ��. ���ǹ�: ������ ũ�Ⱑ �ʹ� �۴ٸ�, 0���� �ϰ�, �ƴ϶�� ����ȭ�Ͽ� ������ ����.
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
