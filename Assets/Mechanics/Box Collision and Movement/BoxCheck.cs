using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Check if there is a box ahead.
 * Move with arrow keys.
 * If there is a box - try to push it.
 * If there is a box - move only if the box moved.
 */
public class BoxCheck : MonoBehaviour
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

        RaycastHit2D hit = Physics2D.Raycast(myRigidbody.position, direction, 1.0f);
        if ( hit.collider != null && hit.collider.CompareTag("Box") )
        {
            print("Box in direction: " + direction);
            BoxController boxController = hit.collider.GetComponent<BoxController>();
            if ( !boxController.TryToMoveInDirection(direction) )
            {
                return;
            }

            myRigidbody.position += direction;
        }

        if ( hit.collider == null )
        {
            myRigidbody.position += direction;
        }
    }
}