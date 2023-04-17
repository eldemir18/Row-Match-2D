using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInformation : MonoBehaviour
{
    private int level;
    private int moveCount;
    private int score;

    public int Level
    {
        get{return level;}
    }

    public int MoveCount
    {
        get{return moveCount;}
    }

    public int Score
    {
        get{return score;}
    }

    public event Action onPieceMove;
    public event Action onPieceClear;
    public event Action onGameOver;

    void Awake()
    {
        level = StateInformation.level;
        moveCount = StateInformation.moveCount;

        StateInformation.mainSceneState = MainSceneManager.MainSceneStates.LOOSE;
    }

    public void OnMove()
    {
        moveCount--;
        onPieceMove();

        if(moveCount == 0) GameOver();
    }

    public void GameOver()
    {
        int highestScore = PlayerPrefs.GetInt($"Level {level}", 0);

        if(score > highestScore)
        {
            PlayerPrefs.SetInt($"Level {level}", score);
            StateInformation.mainSceneState = MainSceneManager.MainSceneStates.WIN;
            StateInformation.highestScore = score;
            
            int unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);
            if(level == unlockedLevel)
            {
                PlayerPrefs.SetInt("unlockedLevel", level+1);
            }
        }

        onGameOver();
    }

    public void OnPieceClear(GamePiece piece)
    {
        score += piece.ColorPieceRef.GetScore;
        onPieceClear();
    }
}
