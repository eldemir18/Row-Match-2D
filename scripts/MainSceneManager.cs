using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{

    public GameObject winAnimation;
    public GameObject levelsPopup;
    public GameObject levelsButton;

    void Awake()
    {
        string mode = StateInformation.mode;

        if(mode == "win")
        {
            levelsButton.SetActive(false);
            levelsPopup.SetActive(false);
            winAnimation.SetActive(true);
        }
        else if(mode == "loose")
        {
            levelsButton.SetActive(false);
            levelsPopup.SetActive(true);
            winAnimation.SetActive(false);
        }
        else
        {
            levelsButton.SetActive(true);
            levelsPopup.SetActive(false);
            winAnimation.SetActive(false);
        }
    }
}
