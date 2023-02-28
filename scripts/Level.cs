using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Linq;
using System;

public class Level : MonoBehaviour
{
    public Grid grid;
    public TextMeshPro scoreText;
    public TextMeshPro levelText;
    public TextMeshPro moveText;
    public TextMeshPro highestScoreText;

    public SceneLoader sceneLoader;
    
    private int moveCount;
    public int MoveCount
    {
        get{return moveCount;}
    }

    private int currentScore;
    private int highestScore;
    private int xDim;
    private int yDim;
    private int levelNum;
    public string fileName;
    
    void Awake()
    {
        // Initilaze level and Move
        levelNum = StateInformation.levelNum;
        moveCount = StateInformation.moveCount;

        // Initilaze highestScore
        highestScore = StateInformation.highestScore;

        // Initilaze grid
        grid.XDim = StateInformation.xDim;
        grid.YDim = StateInformation.yDim;
        grid.GridVals = StateInformation.gridVals;

        // Initilaze level texts
        scoreText.text = currentScore.ToString();
        levelText.text = levelNum.ToString();
        moveText.text = moveCount.ToString();
        highestScoreText.text = highestScore.ToString();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        grid.GameOver();

        if(currentScore > highestScore)
        {
            highestScore = currentScore;
            PlayerPrefs.SetInt("Level" + levelNum.ToString(), highestScore);
            StateInformation.highestScore = highestScore;
            sceneLoader.LoadMainScene("win");
        }
        else
        {
            sceneLoader.LoadMainScene("loose");
        }
    }

    public void OnPieceClear(GamePiece piece)
    {
        currentScore += piece.ColorPieceRef.GetScore;
        scoreText.text = currentScore.ToString();
    }

    public void OnMove()
    {
        moveCount -= 1;
        moveText.text = moveCount.ToString();
        
        if(moveCount == 0) GameOver();
    }
}
