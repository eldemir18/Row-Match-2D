using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameInformation gameInformation;

    void OnEnable()
    {
        if(gameInformation != null) 
        {
            gameInformation.onGameOver += () => LoadMainScene(1f);    
        }
    }

    void OnDisable()
    {
        if(gameInformation != null)
        {
            gameInformation.onGameOver -= () => LoadMainScene(1f); 
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void LoadMainScene(float loadDelay)
    {
        StartCoroutine(LevelLoadDelay("MainScene", loadDelay));
    }

    IEnumerator LevelLoadDelay(string sceneName, float loadDelay)
    {
        yield return new WaitForSecondsRealtime(loadDelay);

        SceneManager.LoadScene(sceneName);
    }
}
