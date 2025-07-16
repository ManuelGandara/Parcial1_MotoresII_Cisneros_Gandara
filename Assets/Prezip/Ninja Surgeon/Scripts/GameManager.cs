using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public int score;
    public int lives;
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
    [SerializeField] public int WinPoints = 300;
    [SerializeField] private TextMeshProUGUI ScoreText;
    private bool _gameOverSequenceTriggered = false;

    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject spawner3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        ScoreText.text = "Objective: " + WinPoints.ToString();
    }

    void Start()
    {
        score = 0;

        lives = RemoteConfigManager.Instance.RemoteConfigValues.InitialLives;

        SetLivesText();
    }

    private void Update()
    {
        if (score >= WinPoints)
        {
            StartCoroutine(GameOverSequence());
        }

        if (lives < 0)
        {
            lives = 0;
        }
    }

    public void UpdateTheScore(int scorePointsToAdd)
    {
        score += scorePointsToAdd;
        scoreText.text = score.ToString();
    }

    public void UpdateLife(int LifeAdd)
    {
        lives += LifeAdd;
        SetLivesText();
    }

    public void UpdateLives()
    {
        if (gameIsOver == false)
        {
            lives--;
            SetLivesText();

            if (lives <= 0)
            {
                StartCoroutine(GameOverSequence());
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
        lives = RemoteConfigManager.Instance.RemoteConfigValues.InitialLives;
        gameIsOver = false;
        gameVictory = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        scoreText.text = score.ToString();
        SetLivesText();

        gameOverImage.gameObject.SetActive(false);
        gameVictoryImage.gameObject.SetActive(false);

        restartButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);

        StaminaManager.Instance.ConsumeStamina(staminaPerPlay);

    }

    private IEnumerator GameOverSequence()
    {
        if (_gameOverSequenceTriggered)
        {
            yield break;
        }
        _gameOverSequenceTriggered = true;
        spawner1.SetActive(false);
        spawner2.SetActive(false);
        spawner3.SetActive(false);
        yield return new WaitForSeconds(3f);
        if (GachaSystem.Instance != null)
        {
            GachaSystem.Instance.TryPickReward();
        }
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            Victory();
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives " + Mathf.Max(0, lives).ToString();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}



