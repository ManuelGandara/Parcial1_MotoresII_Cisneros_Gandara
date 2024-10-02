using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int score;
    int lives;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public Image gameOverImage;
    public Image gameVictoryImage;
    public Image restartButton;
    public Image menuButton;
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
        if (gameIsOver == false)
        {
            lives--;
            livesText.text = "Lives: " + lives;

            if (lives == 0)
            {
                GameOver();
            }
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
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

            if (gameVictoryImage != null)
                gameVictoryImage.gameObject.SetActive(true);

            if (menuButton != null)
                menuButton.gameObject.SetActive(true);
     }

    public void RestartTheGame()
    {
        score = 0;
        lives = 3;
        gameIsOver = false;
        gameVictory = false;

        scoreText.text = score.ToString();
        livesText.text = "Lives: " + lives;

        gameOverImage.gameObject.SetActive(false);
        gameVictoryImage.gameObject.SetActive(false);

        restartButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void StopTime(float duration)
    {
        StartCoroutine(StopTimeCoroutine(duration));
    }

    private IEnumerator StopTimeCoroutine(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}


