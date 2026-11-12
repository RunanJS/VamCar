using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarStat : Unit
{
    [SerializeField] PopupUI gameoverUI;
    [SerializeField] Button restartBtn;

    public override void Dead()
    {
        gameoverUI.Open();
        Time.timeScale = 0;
        restartBtn.onClick.AddListener(() => SceneManager.LoadScene("InGame"));
        base.Dead();
    }
}
