using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public const int CountDown = 3;
    [SerializeField] private TextMeshProUGUI countDownText;

    [SerializeField] private List<PaddleController> playerPaddleList;
    [SerializeField] private List<PaddleController> aiPaddleList;
    private void Start()
    {
        SetupPlayerPaddles();
        StartCoroutine(SetCountdown());
    }

    private IEnumerator SetCountdown()
    {
        for (int i = CountDown; i > 0; i--)
        {
            countDownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countDownText.gameObject.SetActive(false);
    }

    private void SetupPlayerPaddles()
    {
        Debug.Log(GameManager.NoOfPlayers + " : " + GameManager.MaxPlayers);
        for (int i = 0; i < GameManager.NoOfPlayers; i++)
        {
            playerPaddleList[i].gameObject.SetActive(true);
        }
        for (int i = GameManager.NoOfPlayers; i < GameManager.MaxPlayers; i++)
        {
            aiPaddleList[i].gameObject.SetActive(true);
        }
    }
}
