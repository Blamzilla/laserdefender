using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    GameSession gameSession;
    public void LoadGameOver()
    {

        StartCoroutine(GameOverSequence());
        
    }

    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameOver");
    }

    public void LoadGameScene()
    {
        FindObjectOfType<GameSession>().ResetGame();
            SceneManager.LoadScene("Main Game");
        

    }
    public void LoadStartMenu()
    {
        
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {

        Application.Quit();
    }

}
