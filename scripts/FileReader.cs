using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class FileReader : MonoBehaviour
{
    public string[] fileNames;

    private int[] levels;
    private int[] moveCounts;
    private int[] xDims;
    private int[] yDims;
    private int[,] gridVals;

    
    public int[] Levels
    {
        get{return levels;}
    }

    public int[] MoveCounts
    {
        get{return moveCounts;}
    }

    public int[] XDims
    {
        get{return xDims;}
    }

    public int[] YDims
    {
        get{return yDims;}
    }

    void Awake()
    {
        levels = new int[fileNames.Length];
        xDims = new int[fileNames.Length];
        yDims = new int[fileNames.Length];
        moveCounts = new int[fileNames.Length];

        for (int i = 0; i < fileNames.Length; i++)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, fileNames[i]);
            if (!File.Exists(filePath))
            {
                Debug.LogError("File not found: " + filePath);
            }

            string[] lines = File.ReadAllLines(filePath);
            int[] values = new int[4];
            values = lines.Take(4).Select(line => int.Parse(line.Split(':')[1].Trim())).ToArray();
            levels[i] = values[0];
            xDims[i] = values[1];
            yDims[i] = values[2];
            moveCounts[i] = values[3];
        }
    }

    public void FillStateInformation(int index)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileNames[index]);
        if (!File.Exists(filePath))
        {
            Debug.LogError("File not found: " + filePath);
        }

        string[] lines = File.ReadAllLines(filePath);
        string[] sGridVals = lines[4].Split(':')[1].Trim().Split(',');

        // Set grid values
        GetGridValues(sGridVals, xDims[index], yDims[index]);

        // Set state information
        StateInformation.xDim = xDims[index];
        StateInformation.yDim = yDims[index];
        StateInformation.levelNum = levels[index];
        StateInformation.highestScore = PlayerPrefs.GetInt("Level" + levels[index].ToString(), 0);
        StateInformation.moveCount = moveCounts[index];
    }

    private void GetGridValues(string[] sGridVals, int xDim, int yDim)
    {
        gridVals = new int[xDim, yDim];
        int index = 0;
        for (int y = 0; y < yDim; y++)
        {
            for (int x = 0; x < xDim; x++)
            {
                string color = sGridVals[index++].Trim();
                switch (color)
                {
                    case "r":
                        gridVals[x, y] = 0;
                        break;
                    case "g":
                        gridVals[x, y] = 1;
                        break;
                    case "b":
                        gridVals[x, y] = 2;
                        break;
                    case "y":
                        gridVals[x, y] = 3;
                        break;
                    default:
                        Debug.LogError("Invalid color value: " + color);
                        break;
                }
            }
        }

        StateInformation.gridVals = gridVals;
    }    
}
