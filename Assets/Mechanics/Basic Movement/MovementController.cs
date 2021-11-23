using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Movement mechanic: move the avatar in the direction of the pressed arrow key, and change to the correct sprite.
 */
public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float updatesCountInMovement = 4.0f;

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private SpriteRenderer mySpriteRenderer;

    [SerializeField]
    private Rigidbody2D myRigidBody;


    private Vector2 _targetDirection;
    private Vector2 _lastPosition;
    private bool _moving;
    private float _distancePercentage;

    private void Start ()
    {
        _lastPosition = myRigidBody.position;
    }

    // Update is called once per frame
    void Update ()
    {
        if ( _moving )
        {
            return;
        }

        if ( Input.GetKeyDown(KeyCode.LeftArrow) )
        {
            mySpriteRenderer.sprite = sprites[2];
            _targetDirection = Vector2.left;
            _moving = true;
            // myRigidBody.MovePosition(myRigidBody.position + Vector2.left);
        }

        if ( Input.GetKeyDown(KeyCode.UpArrow) )
        {
            mySpriteRenderer.sprite = sprites[0];
            _targetDirection = Vector2.up;
            _moving = true;

            // myRigidBody.MovePosition(myRigidBody.position + Vector2.up);
        }

        if ( Input.GetKeyDown(KeyCode.RightArrow) )
        {
            mySpriteRenderer.sprite = sprites[3];
            _targetDirection = Vector2.right;
            _moving = true;

            // myRigidBody.MovePosition(myRigidBody.position + Vector2.right);
        }

        if ( Input.GetKeyDown(KeyCode.DownArrow) )
        {
            mySpriteRenderer.sprite = sprites[1];
            _targetDirection = Vector2.down;
            _moving = true;

            // myRigidBody.MovePosition(myRigidBody.position + Vector2.down);
        }
    }

    private void FixedUpdate ()
    {
        if ( !_moving )
            return;

        _distancePercentage += (1 / updatesCountInMovement);
        _distancePercentage = _distancePercentage >= 1 ? 1 : _distancePercentage;

        myRigidBody.MovePosition(_lastPosition + _distancePercentage * _targetDirection);

        if ( !(_distancePercentage >= 1) )
            return;

        _distancePercentage = 0;
        _lastPosition += _targetDirection;
        _moving = false;
    }
}