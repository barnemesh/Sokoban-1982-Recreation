using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithReset : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D myRigidbody;

    private Vector3 _initialPosition;

    private void Start ()
    {
        _initialPosition = transform.position;
        ResetController.RegisterReset(ResetPosition);
    }

    private void ResetPosition ()
    {
        transform.position = _initialPosition;
    }

    // Update is called once per frame
    void Update ()
    {
        Vector2 direction = Vector2.zero;
        if ( Input.GetKeyDown(KeyCode.LeftArrow) )
        {
            direction = Vector2.left;
        }

        if ( Input.GetKeyDown(KeyCode.UpArrow) )
        {
            direction = Vector2.up;
        }

        if ( Input.GetKeyDown(KeyCode.RightArrow) )
        {
            direction = Vector2.right;
        }

        if ( Input.GetKeyDown(KeyCode.DownArrow) )
        {
            direction = Vector2.down;
        }

        if ( direction == Vector2.zero )
        {
            return;
        }
        
        myRigidbody.position += direction;
    }
}