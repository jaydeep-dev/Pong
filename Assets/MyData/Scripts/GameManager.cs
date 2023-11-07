using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool Is2PGame => NoOfPlayers <= 2 & MaxPlayers == 2;

    public static List<PaddleController> PaddlesList { get; private set; } = new List<PaddleController>();
    
    public enum Scenes
    {
        MainMenu,
        GameScene_TwoPlayer,
        GameScene_FourPlayer,
    }

    public static bool IsAiGame { get; } = NoOfPlayers != MaxPlayers;
    public static int NoOfPlayers { get; set; } = 2;

    public static int MaxPlayers { get; set; }

    public static void RegisterPaddle(PaddleController paddle) => PaddlesList.Add(paddle);

    public static List<PaddleController> GetOpponents(PaddleController paddleController)
    {
        var otherPaddles = new List<PaddleController>(PaddlesList);
        otherPaddles.Remove(paddleController);
        return otherPaddles;
    }

    public static void ResetData()
    {
        PaddlesList.Clear();
    }
}
