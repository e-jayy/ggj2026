using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameplayScene()
    {
        SceneManager.LoadScene("GameplayScene");

        Time.timeScale = 1f;
    }

    public void LoadCutscene()
    {
        SceneManager.LoadScene("Cutscene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
