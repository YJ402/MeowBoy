using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnHandler : MonoBehaviour
{
    [SerializeField] public GameObject PlayerPrefab;
    // �� ����ÿ� ȣ���ؼ� �÷��̾ ������ �� �ִ� �޼��带 ���⿡ �ۼ�.
    public GameObject SpawnPlayer(Vector2 spawnLocation)
    {
        Debug.Log("ĳ���� ����!");
        GameObject player = Instantiate(PlayerPrefab, spawnLocation, Quaternion.identity);
        return player;
    }
}
