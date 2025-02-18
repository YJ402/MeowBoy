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
        float projectilesAngleSpace = MultipleProjectileAngle; // �߻�ü �ϳ��� ������ ����.(�߻�ü�� ����)
        int numberofProjectilesPerShot = numberofProjectilePerShot;

        float minAngle = -(numberofProjectilePerShot * projectilesAngleSpace) / 2f; // �߻�ü ���� * �߻�ü�� ���� = �߻�ü���� ���� ����
                                                                                    // �߻�ü�� �翷���� ������ ������ /2�� �ϰ�
                                                                                    // - ��ȣ�� �ٿ��ָ� �߻缱�� �������� ���� ������ �߻�ü�� ������ ���� �� ����.
                                                                                    // �߻�ü�� ������ ���⿡ �����ָ� ��.
        for (int i = 0; i < numberofProjectilePerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateProjectile(Controller.LookDirection, angle); // ���̽���Ʈ�ѷ��� �þ� �������� ��ϱ�.
        }
    }

    // �þ߹������� ����ü�� ��ǵ�, angle��ŭ ��¦ ��ȯ��ų����.
    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        projectileManager.ShootBullet(
            this,
            projectileSpawnPosition.position,
            RotateVector2(_lookDirection, angle)
            );
    }

    //�־��� ���� v�� (0,0,degree)��ŭ ȸ����Ű�� �޼���
    //��ȯ��Ģ�� �������� ����.
    //���ʹϾ� * ����2 ���� ����: ���ʹϾ�(4���� ����)�� 3*3 ȸ�� ��ķ� ��ȯ, ����2(x,y ����)�� ����3�� ��ȯ(x,y,0) (����Ƽ���� ����2�� �⺻������ ����3�� ��ȯ�� ó����. 
    //3*3��İ� 3*1 ����� ������ �����.
    //��ȯ�� 3*1 ��İ� 3*3 ����� ���̱� ������ �Ұ�����.
    //cf)ȸ�����: ���͸� ȸ����Ű�� ��ȯ�� ��Ÿ���� ���
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
