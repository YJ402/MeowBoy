using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;

    [SerializeField] private int bulletIndex;
    public int BulletIndex { get { return bulletIndex; } }
    [SerializeField] private float bulletSize = 1f;
    public float BulletSize { get { return bulletSize; } }
    [SerializeField] private float duration;
    public float Duration { get { return duration; } }
    [SerializeField] private float spread;
    public float Spread { get { return spread; } }
    [SerializeField] private int numberofProjectilePerShot;
    public int NumberofProjectilePerShot { get { return numberofProjectilePerShot; } }
    [SerializeField] private float multipleProjectileAngle;
    public float MultipleProjectileAngle { get { return multipleProjectileAngle; } }
    [SerializeField] private Color projectileColor;
    public Color ProjectileColor { get { return projectileColor; } }

    private ProjectileManager projectileManager;

    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
    }

    public override void Attack()
    {
        base.Attack();
        float projectilesAngleSpace = MultipleProjectileAngle; // 발사체 하나가 차지할 각도.(발사체당 각도)
        int numberofProjectilesPerShot = numberofProjectilePerShot;

        float minAngle = -(numberofProjectilePerShot * projectilesAngleSpace) / 2f; // 발사체 갯수 * 발사체당 각도 = 발사체들의 각도 총합
                                                                                    // 발사체는 양옆으로 퍼지기 때문에 /2를 하고
                                                                                    // - 부호를 붙여주면 발사선을 기준으로 가장 왼쪽의 발사체의 각도를 구할 수 있음.
                                                                                    // 발사체당 각도를 여기에 더해주면 됨.
        for (int i = 0; i < numberofProjectilePerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateProjectile(Controller.LookDirection, angle); // 베이스컨트롤러의 시야 방향으로 쏘니까.
        }
    }

    // 시야방향으로 투사체를 쏠건데, angle만큼 살짝 변환시킬거임.
    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        projectileManager.ShootBullet(
            this,
            projectileSpawnPosition.position,
            RotateVector2(_lookDirection, angle)
            );
    }

    //주어진 벡터 v를 (0,0,degree)만큼 회전시키는 메서드
    //교환법칙이 성립하지 않음.
    //쿼터니언 * 벡터2 연산 과정: 쿼터니언(4차원 벡터)는 3*3 회전 행렬로 변환, 벡터2(x,y 벡터)는 벡터3로 변환(x,y,0) (유니티에서 벡터2는 기본적으로 벡터3로 변환돼 처리됨. 
    //3*3행렬과 3*1 행렬의 곱으로 연산됨.
    //교환시 3*1 행렬과 3*3 행렬의 곱이기 때문에 불가능함.
    //cf)회전행렬: 벡터를 회전시키는 변환을 나타내는 행렬
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
