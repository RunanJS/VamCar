using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] PopupUI upgradeUI;
    [SerializeField] Button[] btns = new Button[3];

    public void UpgradeStart()
    {
        upgradeUI.Open();
    }

    public void UpgradeComplete(int select)
    {

    }

    // 레벨이 올라감
    // 업그레이드창이 열림
    // 게임이 멈춤
    // 3개의 선택지버튼이 가능한 업글중 랜덤한 하나씩으로 업데이트됨
        // 현재 가진 부품의 업글, 새로운 부품, 스테이터스의 총 3종류
    // 버튼을 누르면 업글이 진행됨
    // 창이 닫힘
}