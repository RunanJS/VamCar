using UnityEngine;

public class BasicGun : WeaponBase
{
    [SerializeField] private float bulletSpeed = 7;
    [SerializeField] private float rapidSpeed = 3;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePos;

    public override float GetTime => rapidSpeed;

    public override void SkillEffect()
    {
        Instantiate(bullet, firePos.position, transform.rotation)
            .GetComponent<Bullet>().SetBullet(
            bulletSpeed, power);
    }
}
