using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCounter : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer mySpriteRenderer;

    private static WinCounter _shared;

    private int _counter;

    public static int Counter
    {
        get => _shared._counter;
        set
        {
            _shared._counter = value;
            print(_shared._counter);
        }
    }

    private void Awake ()
    {
        _shared = this;
    }

    private void Update ()
    {
        if ( _counter == 0 )
        {
            mySpriteRenderer.color = Color.green;
        }
        else
        {
            mySpriteRenderer.color = Color.red;
        }
    }
}