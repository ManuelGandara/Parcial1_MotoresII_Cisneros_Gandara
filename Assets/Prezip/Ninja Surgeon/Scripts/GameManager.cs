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
    public bool currencyBool;
    public bool gameIsOver = false;
    public bool gameVictory = false;
    public int currencyPerWin = 10;
    public int staminaPerPlay = 10;

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

        TimeManager.instance.StopTime(5f);

        AdsManager.Instance.ShowAd();

    }

    public void Victory()
    {
        gameVictory = true;
        currencyBool = true;
        if (gameVictoryImage != null)
            gameVictoryImage.gameObject.SetActive(true);

        if (menuButton != null)
            menuButton.gameObject.SetActive(true);

        StoreManager.Instance.ObtainCurrency(currencyPerWin);

        TimeManager.instance.StopTime(5f);
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

        StaminaManager.Instance.ConsumeStamina(staminaPerPlay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}



