using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public static TitleManager Inst;
    public int gold = 0;

    bool isInGame = false;

    private void Awake()
    {
        if (Inst == null)
            Inst = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        InputManager.OnPressAnyKey += GameStart;
    }

    public void GameStart()
    {
        if (isInGame) return;
        isInGame = true;
        SceneManager.LoadScene("InGame");
    }

    public void GameEnd()
    {
        if (isInGame)
        {
            isInGame = false;
            SceneManager.LoadScene("Title");
        }
    }
}
