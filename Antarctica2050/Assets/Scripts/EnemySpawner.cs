using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float minX = -8.5f, maxX = 8.5f;

    public GameObject[] enemyPrefabs;
    public GameObject boss;
    public float timer = 3f;
    private bool bossExist;
    
    void Start()
    {
        Invoke("SpawnEnemies", timer);
    }

    private void Update()
    {
        if(Timer.timeRemaining <= 0 && Score.scoreValue >= 8000 && bossExist == false)
        {
            SpawnBoss();
            bossExist = true;
        }
    }

    void SpawnEnemies()
    {
        float posX = Random.Range(minX, maxX);
        Vector3 currentPosition = transform.position;
        currentPosition.x = posX;

        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], currentPosition, Quaternion.Euler(0f, 0f, 0f));
        
        Invoke("SpawnEnemies", timer);
    }

    void SpawnBoss()
    {
        Instantiate(boss, new Vector3(-1.6f, 3.1f), Quaternion.Euler(0f,0f,0f));
    }
}
