using UnityEngine;

public class BoostAddOn : AddOnBase, ISpeed, IUpdated
{
    [SerializeField] private float driftGage = 0;
    public float driftActiveValue = 3;
    public float driftPower;

    public float Power { get { return driftPower; } }

    public override float GetTime => throw new System.NotImplementedException();

    public void OnUpdate()
    {
        if (InputManager.Brake && InputManager.Move != Vector2.zero)
        {
            driftGage += Time.deltaTime;
        }
        else
        {
            driftGage = Mathf.Max(0, driftGage - Time.deltaTime);
        }

        if(driftGage > driftActiveValue)
        {

        }
    }

    public override void SkillEffect()
    {
        throw new System.NotImplementedException();
    }
    public override void Upgrade()
    {
        switch (++grade)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
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
                res = new UpgradeText("효율 상승",
                    "생성하는 에너지의 양이 1.5배로 증가합니다.");
                break;
            case 1:
                res = new UpgradeText("시간 단축",
                    "한번의 작동에 걸리는 시간이 1/2로 줄어듭니다.");
                break;
            case 2:
                res = new UpgradeText("저장량 증가",
                    "소재를 저장해둘 수 있는 슬롯이 2칸 늘어납니다.");
                break;
            case 3:
                res = new UpgradeText("속도 상승",
                    "작동중 이동속도가 0.2배만큼 상승합니다.");
                break;
            case 4:
                res = new UpgradeText("효율 상승",
                    "생성하는 에너지의 양이 2배로 증가합니다.");
                break;
            default:
                break;
        }

        return res;
    }
}
