using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator animatorPlayer;
    public GameObject gameOver;
    public Text scoreText;

    public void Jogar()
    {
        animatorPlayer.SetInteger("Start", 1);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }
}
