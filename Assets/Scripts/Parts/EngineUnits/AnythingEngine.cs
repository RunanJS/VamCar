using System.Collections.Generic;
using UnityEngine;

public class AnythingEngine : EngineBase, IConnecter
{
    public Inventory storage = new Inventory(1);

    public Inventory ConnectedInventory => storage;

    protected override bool GetRequired
    {
        get
        {
            // 아무템 하나 없애고 발전량 전송
            if (storage.RemoveAnyItem(1))
            {
                Debug.Log("발전");
                return true;
            }
            return false;
        }
    }

    public override void Upgrade()
    {
        switch (grade)
        {
            case 1:
                generateAmount *= 1.5f;
                break;
            case 2:
                generateTime /= 2;
                break;
            case 3:
                storage.SizeUp(2);
                break;
            case 4:
                enginePower += 0.2f;
                break;
            case 5:
                generateAmount = 4;
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
