using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance;
    public static ProjectileManager Instance {  get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;

    private void Awake()
    {
        instance = this; // this�� �ǰ�, new ProjectileManager()�� �ȵǴ� ����? this�� �̹� ������Ʈ �ν��Ͻ��μ� �����ϴ� ���� ������ �Ҵ��ϴ� �Ű�,
                         // new�� �ν��Ͻ��� ���� �����ϴ� ���ε�, ����Ƽ���� ������Ʈ �ν��Ͻ��� ���� �����ϴ� ���� �Ұ�����.
    }

    //���Ⱑ ���� ����ü�ε��� ��ȣ�� ���� ����ü ������ ��������, ����ü�� Instantiate��.
    //�� ������ ����ü ���� ��Ʈ�ѷ� ��ũ��Ʈ�� ����, Init��(����ü�� �ʱ�ȭ �Ŀ� ����ü�� bool ������ isReady�� true�� ��ȯ.
    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPosition, Vector2 direction)
    {
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler);
    }
}
