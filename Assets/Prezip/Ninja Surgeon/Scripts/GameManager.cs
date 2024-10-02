using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int score;
    int lives;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public Image gameOverImage;
    public Image gameVictoryImage;
    public Button restartButton;
    public bool gameIsOver = false;
    public bool gameVictory = false;

    void Start()
    {
        score = 0;
        lives = 3;
    }

    private void Update()
    {
        if (score >= 300)
        {
            Victory();
        }
    }

    public void UpdateTheScore(int scorePointsToAdd)
    {
        score += scorePointsToAdd;
        scoreText.text = score.ToString();
    }

    public void UpdateLives()
    {
        if(gameIsOver == false)
        {
            lives--;
            livesText.text = "Lives: " + lives;

            if (lives == 0)
            {
                GameOver();
            }
        }
        
    }

    public void GameOver()
    {
        gameIsOver = true;
        gameOverImage.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void Victory()
    {
        gameVictory = true;
        gameVictoryImage.gameObject.SetActive(true);
    }

    public void RestartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
