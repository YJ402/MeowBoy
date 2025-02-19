using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnHandler : MonoBehaviour
{
    [SerializeField] public GameObject PlayerPrefab;
    // �� ����ÿ� ȣ���ؼ� �÷��̾ ������ �� �ִ� �޼��带 ���⿡ �ۼ�.
    public void SpawnPlayer(Vector2 spawnLocation)
    {
        Instantiate(PlayerPrefab, spawnLocation, Quaternion.identity);
    }
}
