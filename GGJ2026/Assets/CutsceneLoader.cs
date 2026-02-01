using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneLoader : MonoBehaviour
{

    public float cutsceneDuration;
    public string sceneName;

    private void Update()
    {
        cutsceneDuration -= Time.deltaTime;

        if (cutsceneDuration <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
