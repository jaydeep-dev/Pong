using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    public Border border;

    public void BorderHit()
    {
        switch (border)
        {
            case Border.Left:
                Debug.Log("Left Border Hit");
                break;
            case Border.Top:
                Debug.Log("Top Border Hit");
                break;
            case Border.Right:
                Debug.Log("Right Border Hit");
                break;
            case Border.Bottom:
                Debug.Log("Bottom Border Hit");
                break;
            default:
                break;
        }
        
        bool isGameover = ScoreManager.Instance.UpdateScore(border);
        if (isGameover)
        {
            Debug.Log("Game Is Over!!!");
        }
    }
}

public enum Border
{
    None, Left, Top, Right, Bottom
}
