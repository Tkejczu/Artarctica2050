using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    Text timeText;
    private float initialTime = 120;
    public static float timeRemaining;
    public bool timerIsRunning = false;

    void Start()
    {
        timeRemaining = initialTime;
        timeText = GetComponent<Text>();
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }

        DisplayTime();

        if(timeRemaining <= 0 && Score.scoreValue < 8000)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        timeText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
