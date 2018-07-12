using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public Text scoreText;

    int _shapePointValue = 10;    


    void Start()
    {
        //scoreText = GetComponent<Text>();
        score = 0;
        UpdateScore();
    }


    void Update()
    {
        UpdateScore();
    }


    public void AddToScore()
    {
        score += _shapePointValue;
    }


    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
