using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] LevelInformation levelInformation;


    public void InitGame()
    {
        StateInformation.xDim = levelInformation.xDim;
        StateInformation.yDim = levelInformation.yDim;
        StateInformation.level = levelInformation.level;
        StateInformation.moveCount = levelInformation.moveCount;
        StateInformation.gridVals = ConvertStringTo2DArray(levelInformation.gridVals, levelInformation.xDim, levelInformation.yDim);
    }

    private int[,] ConvertStringTo2DArray(string input, int xDim, int yDim)
    {
        Dictionary<string, int> valueMap = new Dictionary<string, int>
        {
            { "r", 0 },
            { "g", 1 },
            { "b", 2 },
            { "y", 3 }
        };

        string[] flattenedArray = input.Split(',');
        int[,] result = new int[xDim, yDim];
        for (int i = 0; i < xDim; i++)
        {
            for (int j = 0; j < yDim; j++)
            {
                string value = flattenedArray[i * yDim + j];
                result[i, j] = valueMap[value];
            }
        }

        return result;
    }
}

