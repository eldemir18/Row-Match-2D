using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] GameObject winAnimation;
    [SerializeField] GameObject winParticles;
    [SerializeField] Animator levelsPopup;


    public enum MainSceneStates
    {
        START,
        WIN,
        LOOSE,
        COUNT
    }

    void Awake()
    {
        switch (StateInformation.mainSceneState)
        {
            case MainSceneStates.START:
                levelsPopup.SetTrigger("Closed");
                break;
            case MainSceneStates.WIN:
                winAnimation.SetActive(true);
                winParticles.SetActive(true);
                levelsPopup.SetTrigger("Opened");
                break;
            case MainSceneStates.LOOSE:
               levelsPopup.SetTrigger("Opened");
                break;
            default:
                // Code to handle any other state not explicitly defined
                break;
        }

    }
}
