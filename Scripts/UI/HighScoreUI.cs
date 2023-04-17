using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;

    void Awake()
    {
        tmp.text = $"Highest Score\n{StateInformation.highestScore}";
    }
}
