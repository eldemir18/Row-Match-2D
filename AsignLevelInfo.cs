using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsignLevelInfo : MonoBehaviour
{
    public LevelInfo[] levelInfos = new LevelInfo[10];
    public FileReader fileReader;

    void Start()
    {
        int tempScore = 1;
        for (int i = 0; i < levelInfos.Length; i++)
        {
            levelInfos[i].SetLevelAndCountText(fileReader.Levels[i], fileReader.MoveCounts[i]);

            if(tempScore > 0)
            {
                tempScore = PlayerPrefs.GetInt("Level" + (i+1).ToString(), 0);
                levelInfos[i].SetHighestScoreText(tempScore);
            }
            else
            {
                levelInfos[i].SetHighestScoreText(-1);
            }
        }
    }
}
