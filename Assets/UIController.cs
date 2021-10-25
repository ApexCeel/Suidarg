using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreUIText;

    private void Start()
    {
        scoreUIText.text = "Score: " + 0;
    }

    public void UpdateScore(int currentScore)
    {
        scoreUIText.text = "Score: " + currentScore;
    }
}
