using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Tilemap containing the target tiles")]
    private Tilemap targetsTilemap;

    private static GameManager _shared;
    private int _targetCounter;
    private int _level = 1;
    private bool _levelWon;

    public static int TargetCounter
    {
        get => _shared._targetCounter;
        set
        {
            _shared._targetCounter = value;
            if (_shared._targetCounter == 0)
            {
                _shared._levelWon = true;
                Debug.Log("Won");
            }
        }
    }

    private void Awake()
    {
        _shared = this;
    }

    private void Start()
    {
        _targetCounter = targetsTilemap.GetUsedTilesCount();
    }
}