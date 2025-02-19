using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpawnHandler : MonoBehaviour
{
    [SerializeField] public GameObject CameraPrefab;
    // 씬 변경시에 호출해서 플레이어를 스폰할 수 있는 메서드를 여기에 작성.
    public void SpawnCamera()
    {
        Destroy(Camera.main.gameObject);
        Instantiate(CameraPrefab, new Vector3(0,0,-10), Quaternion.identity);
    }
}
