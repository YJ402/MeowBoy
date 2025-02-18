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
        instance = this; // this는 되고, new ProjectileManager()는 안되는 이유? this는 이미 컴포넌트 인스턴스로서 존재하는 것을 가져와 할당하는 거고,
                         // new는 인스턴스를 새로 생성하는 것인데, 유니티에서 컴포넌트 인스턴스를 직접 생성하는 것은 불가능함.
    }

    //무기가 가진 투사체인덱스 번호를 통해 투사체 종류를 가져오고, 투사체를 Instantiate함.
    //그 다음에 투사체 내의 컨트롤러 스크립트로 가서, Init함(투사체의 초기화 후에 투사체의 bool 변수인 isReady를 true로 변환.
    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPosition, Vector2 direction)
    {
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler);
    }
}
