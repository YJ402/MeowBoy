using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpawnHandler : MonoBehaviour
{
    [SerializeField] public GameObject CameraPrefab;
    // �� ����ÿ� ȣ���ؼ� �÷��̾ ������ �� �ִ� �޼��带 ���⿡ �ۼ�.
    public void SpawnCamera()
    {
        Destroy(Camera.main.gameObject);
        Instantiate(CameraPrefab, new Vector3(0,0,-10), Quaternion.identity);
    }
}
