using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _shared;

    private int _currentLevel = 1;
    private String _currentScene;
    private bool _wonLevel;

    public static void SetWon ()
    {
        _shared._wonLevel = true;
    }

    private void Awake ()
    {
        _shared = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update ()
    {
        if ( Input.GetKeyDown(KeyCode.F1) )
        {
            // TODO: add prompt
            if ( _wonLevel )
            {
                _wonLevel = false;
                _currentLevel++;
                _currentLevel %= SceneManager.sceneCountInBuildSettings;
            }

            SceneManager.LoadScene(_currentLevel);
        }
    }
}