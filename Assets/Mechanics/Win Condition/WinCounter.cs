using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Manager to count the number of targets that have no boxes.
 * Turns red if the counter is not 0 (not won yet).
 * Turns green when all targets have boxes (game won).
 */
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