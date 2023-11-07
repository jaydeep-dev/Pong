using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Gameplay UI")]
    [SerializeField] private TextMeshProUGUI leftWinText;
    [SerializeField] private TextMeshProUGUI rightWinText;
    [SerializeField] private TextMeshProUGUI topWinText;
    [SerializeField] private TextMeshProUGUI bottomWinText;

    [Header("Win UI")]
    [SerializeField] private GameObject winPopup;
    [SerializeField] private TextMeshProUGUI winnerText;

    private byte rightScore, leftScore, topScore, bottomScore;

    private void Awake()
    {
        Instance = this;
        if (GameManager.Is2PGame)
        {
            leftScore = rightScore = 0;
            leftWinText.text = rightWinText.text = "0";
        }
        else
        {
            rightScore = leftScore = topScore = bottomScore = 3;

            topWinText.text = "Top: " + topScore.ToString();
            bottomWinText.text = "Bottom: " + bottomScore.ToString();
            leftWinText.text = "Left: " + leftScore.ToString();
            rightWinText.text = "Right: " + rightScore.ToString();
        }
    }

    public bool UpdateScore(Border border)
    {
        bool gameOverStatus;
        if (GameManager.Is2PGame)
            gameOverStatus = CalculateTwoPlayerScore(border);
        else
            gameOverStatus = CalculateFourPlayerScore(border);

        return gameOverStatus;
    }

    private bool CalculateFourPlayerScore(Border border)
    {
        switch (border)
        {
            case Border.Left:
                leftWinText.text = "Left: " + (--leftScore).ToString();
                break;

            case Border.Right:
                rightWinText.text = "Right: " + (--rightScore).ToString();
                break;

            case Border.Top:
                topWinText.text = "Top: " + (--topScore).ToString();
                break;

            case Border.Bottom:
                bottomWinText.text = "Bottom: " + (--bottomScore).ToString();
                break;
        }

        const string OUT_STRING = "Out";

        if (leftScore <= 0)
        {
            leftWinText.text = OUT_STRING;
            winnerText.text = "Left Player Lost!!!";
        }
        else if (rightScore <= 0)
        {
            rightWinText.text = OUT_STRING;
            winnerText.text = "Right Player Lost!!!";
        }
        else if (topScore <= 0)
        {
            topWinText.text = OUT_STRING;
            winnerText.text = "Top Player Lost!!!";
        }
        else if (bottomScore <= 0)
        {
            bottomWinText.text = OUT_STRING;
            winnerText.text = "Bottom Player Lost!!!";
        }

        bool isGameOver = leftScore <= 0 || rightScore <= 0 || topScore <= 0 || bottomScore <= 0;

        if (isGameOver)
        {
            Time.timeScale = 0;
            winPopup.SetActive(true);
            Debug.Log(winnerText.text);
        }

        return isGameOver;
    }

    private bool CalculateTwoPlayerScore(Border border)
    {
        switch (border)
        {
            case Border.Left:
                rightWinText.text = (++rightScore).ToString();
                break;
            case Border.Right:
                leftWinText.text = (++leftScore).ToString();
                break;
            case Border.Top:
                topWinText.text = (++topScore).ToString();
                break;
            case Border.Bottom:
                bottomWinText.text = (++bottomScore).ToString();
                break;
        }

        if (leftScore >= 3)
        {
            winnerText.text = "Left Player Won!!!";
            Time.timeScale = 0;
            winPopup.SetActive(true);
            Debug.Log("Left Player Won!!!");
        }
        else if (rightScore >= 3)
        {
            winnerText.text = "Right Player Won!!!";
            Time.timeScale = 0;
            winPopup.SetActive(true);
            Debug.Log("Right Player Won!!!");
        }

        bool isGameOver = leftScore >= 3 || rightScore >= 3;
        return isGameOver;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        GameManager.ResetData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainmenu()
    {
        Time.timeScale = 1;
        GameManager.ResetData();
        SceneManager.LoadScene(GameManager.Scenes.MainMenu.ToString());
    }
}
