using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D myRigidbody;

    public bool TryToMoveInDirection (Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(myRigidbody.position, direction, 1.0f);
        if ( hit.collider == null )
        {
            print("Box moving in direction: " + direction);
            myRigidbody.position += direction;
            return true;
        }

        print("Cant move box in direction: " + direction);
        return false;
    }
}