using UnityEngine;

public class BasicGun : WeaponBase
{
    [SerializeField] private float bulletSpeed = 7;
    [SerializeField] private float rapidSpeed = 3;
    [SerializeField] private int rapidCount = 1;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePos;

    public override float GetTime => rapidSpeed;

    public override void SkillEffect()
    {
        for (int i = 0; i < rapidCount; i++)
        {
            Instantiate(bullet, firePos.position, transform.rotation)
                .GetComponent<Bullet>().SetBullet(
                bulletSpeed, Power);
        }
    }

    public override void Upgrade()
    {
        switch (grade)
        {
            case 1:
                rapidSpeed *= 1.3f;
                break;
            case 2:
                power = 130;
                break;
            case 3:
                rapidCount += 1;
                break;
            case 4:
                power = 160;
                break;
            case 5:
                rapidCount += 1;
                rapidSpeed *= 1.3f;
                break;
            default:
                break;
        }
    }

    public override UpgradeText GetExplain()
    {
        UpgradeText res = new UpgradeText();
        switch (grade)
        {
            case 0:
                res = new UpgradeText("공격 속도 상승",
                    "공격을 가하는 속도가 1.3배 상승합니다.");
                break;
            case 1:
                res = new UpgradeText("위력 증가",
                    "공격의 위력이 130%로 증가합니다.");
                break;
            case 2:
                res = new UpgradeText("탄 수 증가",
                    "나가는 탄의 수가 1 증가합니다.");
                break;
            case 3:
                res = new UpgradeText("위력 증가",
                    "공격의 위력이 160%로 증가합니다.");
                break;
            case 4:
                res = new UpgradeText("탄 수 증가 / 공격 속도 상승",
                    "나가는 탄의 수가 1 증가하고, 공격을 가하는 속도도 1.3배 더 상승합니다.");
                break;
            default:
                break;
        }

        return res;
    }
}
