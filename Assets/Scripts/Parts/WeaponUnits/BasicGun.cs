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
            bulletSpeed, Power);
    }
    public override UpgradeText Upgrade()
    {
        UpgradeText res = new UpgradeText();
        switch (grade)
        {
            case 1:
                res = new UpgradeText("",
                    "");
                break;
            case 2:
                res = new UpgradeText("",
                    "");
                break;
            case 3:
                res = new UpgradeText("",
                    "");
                break;
            case 4:
                res = new UpgradeText("",
                    "");
                break;
            case 5:
                res = new UpgradeText("",
                    "");
                break;
            default:
                break;
        }

        return res;
    }
}
