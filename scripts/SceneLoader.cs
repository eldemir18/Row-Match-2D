using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public float loadDelay = 2.0f;

    void Awake()
    {
        Screen.SetResolution(Mathf.RoundToInt(Screen.height * (9.0f / 16.0f)), Screen.height, true);
    }
 
    public void LoadGameScene()
    {
        SceneManager.LoadScene("LevelScene");
    }


    public void LoadMainScene(string mode)
    {
        StateInformation.mode = mode;

        StartCoroutine(LevelLoadDelay("MainScene"));
    }

    IEnumerator LevelLoadDelay(string sceneName)
    {
        yield return new WaitForSecondsRealtime(loadDelay);

        SceneManager.LoadScene(sceneName);
    }
}
