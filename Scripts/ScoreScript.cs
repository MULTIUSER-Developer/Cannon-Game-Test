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

    void SaveHighScore()
    {
        Debug.Log("Saving the HighScore: " + HighScore.ToString());
        PlayerPrefs.SetInt("HighScore", HighScore);
    }

    void LoadHighScore()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreText.text = HighScore.ToString();
        Debug.Log("Loading the value: " + HighScore.ToString());
    }

    void Update()
    {
        scoreText.text = score.ToString();

        if (score > HighScore)
        {
            HighScore = score;
            HighScoreText.text = HighScore.ToString();
        }
    }

}
