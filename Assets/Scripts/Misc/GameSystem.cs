using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    private static GameSystem ThisInstance;
    public static GameSystem Instance
    {
        get {
            if (ThisInstance == null) {
                ThisInstance = FindObjectOfType<GameSystem>();
                if (ThisInstance == null) {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    ThisInstance = obj.AddComponent<GameSystem>();
                }
            }
            return ThisInstance;
        }
    }

    public bool Paused;

    private void Start()
    {
        Paused = false;
        Time.timeScale = 1.0f;
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ThisInstance = this;
    }

    private void Update()
    {
        
    }
    private void OnDestroy()
    {
        ThisInstance = null;
    }
    
    // use this for a future pause menu function
    public void Pause()
    {
        Time.timeScale = 0;
    }
}
