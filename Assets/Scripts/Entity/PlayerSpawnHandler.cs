using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnHandler : MonoBehaviour
{
    [SerializeField] public GameObject PlayerPrefab;
    // 씬 변경시에 호출해서 플레이어를 스폰할 수 있는 메서드를 여기에 작성.
    public GameObject SpawnPlayer(Vector2 spawnLocation)
    {
        Debug.Log("캐릭터 생성!");
        GameObject player = Instantiate(PlayerPrefab, spawnLocation, Quaternion.identity);
        return player;
    }
}
