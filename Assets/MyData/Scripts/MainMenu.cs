using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameManager.Scenes scene;

    private void Awake()
    {
        GameManager.ResetData();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void Set2PGame()
    {
        GameManager.MaxPlayers = 2;
        scene = GameManager.Scenes.GameScene_TwoPlayer;
    }

    public void Set4PGame() 
    {
        GameManager.MaxPlayers = 4;
        scene = GameManager.Scenes.GameScene_FourPlayer;
    }

    public void SetPlayersCount(int count)
    {
        GameManager.NoOfPlayers = count;
        StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
