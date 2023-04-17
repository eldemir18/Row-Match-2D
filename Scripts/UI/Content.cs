using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content : MonoBehaviour
{
    [SerializeField] RectTransform content;
    
    void Start()
    {  
        int unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);

        if (unlockedLevel >= 5)
        {
            Vector2 newAnchoredPosition = content.anchoredPosition;
            int shiftAmount = 490 + (220 * (unlockedLevel - 5)); 
            newAnchoredPosition.y += shiftAmount;
            content.anchoredPosition = newAnchoredPosition;
        }
    }

}

