using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text scoreText;
    GameSession gameSession;


    private void Start()
    {
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        if(gameSession == null)
        {
            gameSession = FindObjectOfType<GameSession>();
        }
        scoreText.text = gameSession.GetScore().ToString();
    }
}