using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelGameManager : MonoBehaviour
{
    private static LevelGameManager _shared;
    private int _targetCounter;

    public static int TargetCounter
    {
        get => _shared._targetCounter;
        set
        {
            _shared._targetCounter = value;
            if ( _shared._targetCounter == 0 )
            {
                Debug.Log("Won");
            }
        }
    }

    private void Awake ()
    {
        _shared = this;
    }

    private void Update ()
    {
        if ( Input.GetKeyDown(KeyCode.F1) )
        {
            SceneManager.LoadScene(1);
        }
    }
}