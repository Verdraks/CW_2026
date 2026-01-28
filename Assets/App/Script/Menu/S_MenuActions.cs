using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MenuActions : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
