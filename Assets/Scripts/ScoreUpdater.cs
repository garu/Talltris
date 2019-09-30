using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUpdater : MonoBehaviour
{
    private GameObject gameOverTextObject;
    private TextMeshProUGUI scoreText;
    private int score;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameOverTextObject = FindObjectOfType<GameOver>().gameObject;
        NewGame();
    }

    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void UpdateScore()
    {
        score += 100;
    }

    public void BonusScore()
    {
        score += 1000;
    }

    public void GameOver()
    {
        gameOverTextObject.SetActive(true);
    }

    public void NewGame()
    {
        score = 0;
        gameOverTextObject.SetActive(false);
    }
}
