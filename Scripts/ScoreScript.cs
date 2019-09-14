using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//https://www.youtube.com/watch?time_continue=947&v=XOjd_qU2Ido

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;
    public int HighScore = 0;
    public TextMeshProUGUI HighScoreText;

    public void SaveScore()
    {
        SaveSystem.SaveData(this);
    }

    public void LoadData()
    {
        HighScore = SaveSystem.LoadData().HighScore;
    }

    void Start()
    {
        
    }

    void Update()
    {
        scoreText.text = score.ToString();
        
        if (score >= HighScore)
        {
            HighScore = score;
            HighScoreText.text = HighScore.ToString();
            SaveScore();
        }
    }

}
