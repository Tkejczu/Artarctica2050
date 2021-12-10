using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyAudio : MonoBehaviour
{
    private Scene currentScene;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
   
        if(currentScene.name == "Level1")
        {
            Destroy(gameObject);
        }
    }

}
