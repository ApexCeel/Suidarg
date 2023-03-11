using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreUIText;
    [SerializeField] private Sprite[] lifeSprites;
    [SerializeField] private Image livesImage;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text restartText;

    private void Start()
    {
        scoreUIText.text = "Score: " + 0;
        gameOverText.gameObject.SetActive(false);
        livesImage.sprite = lifeSprites[3];
    }

    public void UpdateScore(int currentScore)
    {
        scoreUIText.text = "Score: " + currentScore;
    }

    public void UpdateLives(int currentLives)
    {
        livesImage.sprite = lifeSprites[currentLives];

        if (currentLives <= 0)
        {
            GameOverSequence();
        }

    }

    private void GameOverSequence()
    {
        FindObjectOfType<GameManager>().GameOver();
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        StartCoroutine(FlickerRoutine());
    }

    private IEnumerator FlickerRoutine()
    {
        while (true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
            gameOverText.text = "";
            yield return new WaitForSeconds(Random.Range(0.1f, 0.6f));
            
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
