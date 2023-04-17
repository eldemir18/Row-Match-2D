using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelInformationUI : MonoBehaviour
{
    [SerializeField] LevelInformation levelInformation;
    [SerializeField] TextMeshProUGUI levelAndMoveTMP;
    [SerializeField] TextMeshProUGUI highestScoreTMP;
    [SerializeField] Button playButton;

    void Start()
    {
        SetLevelAndCountTMP();
        SetHighestScoreTMP();
    }

    public void SetLevelAndCountTMP()
    {
        levelAndMoveTMP.text = "Level " + levelInformation.level.ToString() + " - " + levelInformation.moveCount.ToString() + " Moves"; 
    }

    public void SetHighestScoreTMP()
    {
        int unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);

        if(unlockedLevel < levelInformation.level)
        {
            highestScoreTMP.text = "Locked Level";
            playButton.interactable = false;
        } 
        else if(unlockedLevel == levelInformation.level)
        {
            highestScoreTMP.text = "No Score";
        }
        else
        {
            highestScoreTMP.text = "Highest Score: " + PlayerPrefs.GetInt($"Level {levelInformation.level}", 0);
        }
    }
}
