using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelInfo : MonoBehaviour
{
    public TextMeshPro levelAndMoveText;
    public TextMeshPro highestScoreText;
    public Button playButton;

    public void SetLevelAndCountText(int level, int moveCount)
    {
        levelAndMoveText.text = "Level " + level.ToString() + " - " + moveCount.ToString() + " Moves"; 
    }

    public void SetHighestScoreText(int highScore)
    {
        if(highScore == -1)
        {
            highestScoreText.text = "Locked Level";
            playButton.LockButton();
        } 
        else if(highScore == 0)
        {
            highestScoreText.text = "No Score";
        }
        else
        {
            highestScoreText.text = "Highest Score: " + highScore.ToString();
        }
    }
}
