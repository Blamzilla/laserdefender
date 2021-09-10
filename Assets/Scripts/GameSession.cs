using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;


    // Start is called before the first frame update
    private void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        int numberGameSession = FindObjectsOfType<GameSession>().Length;
        if(numberGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Update is called once per frame
    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }
    public void ResetGame()
    {
        Debug.Log("I Ran");
        Destroy(gameObject);
    }
}
