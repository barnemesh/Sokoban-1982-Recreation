using System;
using UnityEngine;

/**
 * Manager to allow resetting all objects to initial position.
 * Register functions to call when reset.
 * Press F1 to invoke call to all registered functions.
 */
public class ResetController : MonoBehaviour
{
    private static ResetController _shared;

    private event Action ResetLevel;

    private void Awake ()
    {
        _shared = this;
    }

    public static void RegisterReset (Action call)
    {
        _shared.ResetLevel += call;
    }

    private void Update ()
    {
        if ( Input.GetKeyDown(KeyCode.F1) )
        {
            ResetLevel?.Invoke();
        }
    }
}