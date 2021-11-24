using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Move with arrow keys.
 */
public class SimpleMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D myRigidbody;

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
