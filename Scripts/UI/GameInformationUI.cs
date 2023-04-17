using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameInformationUI : MonoBehaviour
{
    [SerializeField] GameInformation gameInformation;

    [Header("TMP")]
    [SerializeField] TextMeshProUGUI levelTMP;
    [SerializeField] TextMeshProUGUI moveTMP;
    [SerializeField] TextMeshProUGUI scoreTMP;

    [Header("Button")]
    [SerializeField] Button backButton;
    [SerializeField] Button restartButton;

    void Start()
    {
        scoreTMP.text = $"Score\n{gameInformation.Score}";
        levelTMP.text = $"Level {gameInformation.Level}";
        moveTMP.text  = $"Move Left\n{gameInformation.MoveCount}";
    }

    void OnEnable()
    {
        gameInformation.onPieceMove += UpdateMoveTMP;
        gameInformation.onPieceClear += UpdateScoreTMP;
        gameInformation.onGameOver += DisableButtons;
    }

    void OnDisable()
    {
        gameInformation.onPieceMove -= UpdateMoveTMP;
        gameInformation.onPieceClear -= UpdateScoreTMP;
        gameInformation.onGameOver -= DisableButtons;
    }

    public void UpdateMoveTMP()
    {
        moveTMP.text  = $"Move Left\n{gameInformation.MoveCount}";
    }

    public void UpdateScoreTMP()
    {
        scoreTMP.text = $"Score\n{gameInformation.Score}";
    }

    public void DisableButtons()
    {
        backButton.interactable = false;
        restartButton.interactable = false;
    }

}
/*


    */

