using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Manager to count the moves made by the user.
 */
public class MoveCounter : MonoBehaviour
{
    private static MoveCounter _shared;

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
}