using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    public static int powerValue;
    Text powerText;

    void Start()
    {
        powerText = GetComponent<Text>();
    }

    void Update()
    {
        powerValue = Player.currentPowerUps;
        powerText.text = "Power: " + powerValue;
    }
}
