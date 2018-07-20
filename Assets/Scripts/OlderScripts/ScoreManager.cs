using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public Text scoreText;

    private int _shapePointValue = 10;    


    void Start()
    {
        score = 0;
        UpdateScore();
    }


    //  Adds points to score
    //  void -> void
    public void AddToScore()
    {
        score += _shapePointValue;
        UpdateScore();
    }


    //  Displays updated score
    //  void -> void
    void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
}
