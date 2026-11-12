using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public int gold = 0;

    bool isInGame = false;

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
