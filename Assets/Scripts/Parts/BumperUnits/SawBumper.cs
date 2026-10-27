using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SawBumper : WeaponBase
{
    [SerializeField] GameObject saw;
    private bool gravity = false;
    [SerializeField] float pullSpeed = 1.5f;

    public override float GetTime => Time.deltaTime;

    public override void SkillEffect()
    {
        float speed = 500 * Time.deltaTime;
        saw.transform.Rotate(0, speed, 0);

        foreach (var target in CatchAll(maxCatchCount))
        {
            target.OnDamaged(Power * Time.deltaTime);
        }

        if (gravity) Pull();
    }

    void Pull()
    {
        foreach (var c in catchers)
        {
            Vector3 center = c.Position(transform);

            foreach (var target in c.Catch(transform, 2f))
            {
                Vector3 pos = target.transform.position;
                Vector3 next = Vector3.MoveTowards(pos, center, pullSpeed * Time.deltaTime);
                next.y = pos.y;                      // 높이는 유지
                target.transform.position = next;
            }
        }
    }

    public override void Upgrade()
    {
        switch (grade)
        {
            case 1:
                Resize(1.3f);
                break;
            case 2:
                power = 130;
                break;
            case 3:
                maxCatchCount *= 2;
                break;
            case 4:
                power = 160;
                break;
            case 5:
                gravity = true;
                Resize(1.3f);
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
                res = new UpgradeText("범위 증가",
                    "공격 범위가 1.3배로 증가합니다.");
                break;
            case 1:
                res = new UpgradeText("위력 증가",
                    "공격의 위력이 130%로 증가합니다.");
                break;
            case 2:
                res = new UpgradeText("포착 대상 증가",
                    "한번에 공격하는 대상 수가 2배 증가합니다.");
                break;
            case 3:
                res = new UpgradeText("위력 증가",
                    "공격의 위력이 160%로 증가합니다.");
                break;
            case 4:
                res = new UpgradeText("범위 증가 / 인력",
                    "공격 범위가 1.7배로 증가하고 톱날이 적을 끌어당기게 됩니다.");
                break;
            default:
                break;
        }

        return res;
    }
}
