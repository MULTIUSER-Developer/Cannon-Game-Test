using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int HighScore;
    public PlayerData (ScoreScript HighScore_)
    {
        HighScore = HighScore_.HighScore;
    }

}
