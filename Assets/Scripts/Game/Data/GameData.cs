using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int LevelsCompleted;

    public GameData(int levelsCompleted)
    {
        LevelsCompleted = levelsCompleted;
    }
}
