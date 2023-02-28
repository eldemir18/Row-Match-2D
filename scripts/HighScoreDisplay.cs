using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshPro textMeshPro;


    void Awake()
    {
        textMeshPro.text = "Highest Score\n" + StateInformation.highestScore.ToString();
    }
}
