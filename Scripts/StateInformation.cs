using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInformation : MonoBehaviour
{ 
    public static MainSceneManager.MainSceneStates mainSceneState;
    public static int currentScore;
    public static int highestScore;
    public static int xDim;
    public static int yDim;
    public static int level;
    public static int moveCount;
    public static int[,] gridVals;
}
