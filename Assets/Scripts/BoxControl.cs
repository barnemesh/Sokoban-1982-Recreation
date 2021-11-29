using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Box movement controls.
 * If pushed - try and move in the direction pushed.
 * If there is nothing there - move.
 * If there is anything in the way - dont move.
 */
public class BoxControl : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How many updates should a single movement take")]
    private float updatesCountInMovement = 4.0f;

    [SerializeField]
    private Rigidbody2D myRigidbody;

    private Vector2 _targetDirection;
    private bool _moving;
    private Vector2 _lastPosition;
    private float _distancePercentage;

    private void Start()
    {
        _lastPosition = myRigidbody.position;
    }

    public bool TryToMoveInDirection(Vector2 direction)
    {
        if (_moving)
        {
            return false;
        }

        RaycastHit2D hit = Physics2D.Raycast(myRigidbody.position, direction, 1.0f);
        if (hit.collider == null)
        {
            _targetDirection = direction;
            _moving = true;
            return true;
        }

        return false;
    }

    private void FixedUpdate()
    {
        if (!_moving)
            return;

        _distancePercentage += (1 / updatesCountInMovement);
        _distancePercentage = _distancePercentage >= 1 ? 1 : _distancePercentage;

        myRigidbody.MovePosition(_lastPosition + _distancePercentage * _targetDirection);

        if (!(_distancePercentage >= 1))
            return;

        _distancePercentage = 0;
        _lastPosition += _targetDirection;
        _moving = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            GameManager.TargetCounter--;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            GameManager.TargetCounter++;
        }
    }
}