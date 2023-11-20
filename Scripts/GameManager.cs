using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioManager am;
    public TMP_Text timerText;
    public TMP_Text playerOneNameText; //ime igraèa
    public TMP_Text playerTwoNameText; //ime igraèa
    string playerOneNameString;
    string playerTwoNameString;
    public TMP_InputField playerOneNameInput; //unos imena
    public TMP_InputField playerTwoNameInput; //unos imena
    public TMP_Text playerOneScoreText; //score u igri
    public TMP_Text playerTwoScoreText; //score u igri
    public TMP_Text playerOneScoreTotal; //ukupni broj pobjeda
    public TMP_Text playerTwoScoreTotal; //ukupni broj pobjeda
    public TMP_Text gameOverText;
    public GameObject gameOverScreen;
    public GameObject mainMenuScreen;
    public float time = 181;
    public int playerOneScore = 1100; //jedan value na houseu je 100
    public int playerTwoScore = 1100;
    int playerOneWin;
    int playerTwoWin;
    public bool gameOverTime;
    public bool gameOverNoHouses;
    public bool bossDefeated;
    int played;
    private void Start()
    {
        played = PlayerPrefs.GetInt("Played");
        if (played == 0)
        {
            PlayerPrefs.SetInt("Played", 1);
            am.PlayMenuMusic();
            Time.timeScale = 0;
            mainMenuScreen.SetActive(true);
            ShowScore();
            if (playerOneNameString == null)
            {
                playerOneNameString = "Mellow";
                PlayerPrefs.SetString("PlayerOneName", playerOneNameString);
            }
            if (playerTwoNameString == null)
            {
                playerTwoNameString = "Yellow";
                PlayerPrefs.SetString("PlayerTwoName", playerTwoNameString);
            }
        }
        else
        {
            am.PlayGameMusic();
            Time.timeScale = 1;
            mainMenuScreen.SetActive(false);
            ShowScore();
            playerOneNameString = PlayerPrefs.GetString("PlayerOneName");
            playerTwoNameString = PlayerPrefs.GetString("PlayerTwoName");
            ShowNames();
        }
    }
    private void Update()
    {
        if (time >= 0)
        {
            time -= Time.deltaTime;
            int minutes = (int)(time / 60);
            int seconds = (int)(time % 60);
            if (minutes < 10 && seconds < 10)
                timerText.text = "0" + minutes + ":" + "0" + seconds;
            else if (minutes < 10 && seconds >= 10)
                timerText.text = "0" + minutes + ":" + seconds;
            else if (minutes >= 10 && seconds < 10)
                timerText.text = minutes + ":0" + seconds;
            else
                timerText.text = minutes + ":" + seconds;
        }
        if (time <= 0)
        {
            gameOverTime = true;
            GameOver();
        }
        if (playerOneScore <= 0 || playerTwoScore <= 0)
        {
            gameOverNoHouses = true;
            GameOver();
        }
        if(bossDefeated)
        {
            GameOver();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        
        if (gameOverTime)
            gameOverText.text = "Time's up! You need to rub the Magic Monolith. You both lose.";
        if (gameOverNoHouses)
            gameOverText.text = "You did not cooperate! Socialize! You both lose.";
        if (bossDefeated)
        {
            if (playerOneScore > playerTwoScore)
            {
                playerOneWin = PlayerPrefs.GetInt("POneWin");
                playerOneWin++;
                PlayerPrefs.SetInt("POneWin", playerOneWin);
                ShowTotalScore();
                gameOverText.text = "Well played " + playerOneNameString + ", you magnificent greedy bastard!";
            }
            else if (playerTwoScore > playerOneScore)
            {
                playerTwoWin = PlayerPrefs.GetInt("PTwoWin");
                playerTwoWin++;
                PlayerPrefs.SetInt("PTwoWin", playerTwoWin);
                ShowTotalScore();
                gameOverText.text = "Well played " + playerTwoNameString + ", you magnificent greedy bastard!";
            }
            else if (playerTwoScore == playerOneScore)
            {
                gameOverText.text = "You have beaten the game. How?!?";
            }
        }
    }
    public void ShowScore()
    {
        playerOneScoreText.text = playerOneScore.ToString();
        playerTwoScoreText.text = playerTwoScore.ToString();
    }
    public void StartGame()
    {
        am.PlayGameMusic();
        Time.timeScale = 1;
        ShowNames();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
    public void EnterPlayerOneName()
    {
        playerOneNameString = playerOneNameInput.text;
        PlayerPrefs.SetString("PlayerOneName", playerOneNameString);
    }
    public void EnterPlayerTwoName()
    {
        playerTwoNameString = playerTwoNameInput.text;
        PlayerPrefs.SetString("PlayerTwoName", playerTwoNameString);
    }
    public void ShowNames()
    {
        playerOneNameText.text = playerOneNameString;
        playerTwoNameText.text = playerTwoNameString;
    }
    public void ShowTotalScore()
    {
        playerOneScoreTotal.text = playerOneWin.ToString();
        playerTwoScoreTotal.text = playerTwoWin.ToString();
    }
}

