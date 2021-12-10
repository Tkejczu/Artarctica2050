using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public static int healthValue;
    Text healthText;

    void Start()
    {
        healthText = GetComponent<Text>();
    }

    void Update()
    {
        healthValue = Player.currentHealth;
        healthText.text = "Health: " + healthValue;
    }
}
