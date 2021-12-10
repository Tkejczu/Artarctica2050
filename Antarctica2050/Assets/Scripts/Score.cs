using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int scoreValue = 0;
    Text scoreText;
    int bonus;
    const int bonusInterval = 3000;
    private AudioSource bonusSound;


    void Start()
    {
        bonusSound = GetComponent<AudioSource>();
        bonus = bonusInterval;
        scoreText = GetComponent<Text>();
        scoreValue = 0;
    }

    void Update()
    {
        scoreText.text = "Score: " + scoreValue;
        ScoreRewards();
    }

    void ScoreRewards()
    {
        if (scoreValue >= bonus)
        {
            Player.currentPowerUps++;
            Player.currentHealth++;
            bonus += bonusInterval;
            bonusSound.Play();
        }
    }
}
