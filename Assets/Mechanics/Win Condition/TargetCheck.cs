using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Add 1 to global win counter whenever there is no box above the target.
 */
public class TargetCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start ()
    {
        WinCounter.Counter++;
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if ( other.CompareTag("Box") )
        {
            WinCounter.Counter--;
        }
    }

    private void OnTriggerExit2D (Collider2D other)
    {
        if ( other.CompareTag("Box") )
        {
            WinCounter.Counter++;
        }
    }
}