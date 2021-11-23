using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetController : MonoBehaviour
{
    private static ResetController _shared;

    [SerializeField]
    private UnityEvent resetLevel;

    private void Awake ()
    {
        _shared = this;
    }

    public static void RegisterReset (UnityAction call)
    {
        _shared.resetLevel.AddListener(call);
    }

    private void Update ()
    {
        if ( Input.GetKeyDown(KeyCode.R) )
        {
            resetLevel.Invoke();
        }
    }
}