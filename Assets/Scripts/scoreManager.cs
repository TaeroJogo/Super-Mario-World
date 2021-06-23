using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreManager : MonoBehaviour
{

    public static scoreManager instance;
    public TextMeshProUGUI text;
    int score;
    private Transform playerTransform;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void changeScore(int coinValue)
    {
        score += coinValue;
        text.text = "X" + score.ToString();
    }

    public void RestartPoints()
    {
        score = 0;
        changeScore(0);
    }
}
