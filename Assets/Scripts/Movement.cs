using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How many updates should a single movement take")]
    private float updatesCountInMovement = 4.0f;

    [SerializeField]
    [Tooltip("Up, Down, Left, Right")]
    private Sprite[] sprites;

    [SerializeField]
    private SpriteRenderer mySpriteRenderer;

    [SerializeField]
    private Rigidbody2D myRigidbody;


    private Vector2 _targetDirection;
    private Vector2 _lastPosition;
    private bool _moving;
    private float _distancePercentage;

    private void Start()
    {
        _lastPosition = myRigidbody.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_moving)
        {
            return;
        }

        _targetDirection = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            mySpriteRenderer.sprite = sprites[2];
            _targetDirection = Vector2.left;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mySpriteRenderer.sprite = sprites[0];
            _targetDirection = Vector2.up;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            mySpriteRenderer.sprite = sprites[3];
            _targetDirection = Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mySpriteRenderer.sprite = sprites[1];
            _targetDirection = Vector2.down;
        }

        if (_targetDirection == Vector2.zero)
        {
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(myRigidbody.position, _targetDirection, 1.0f);
        if (hit.collider != null && hit.collider.CompareTag("Box"))
        {
            BoxControl boxControl = hit.collider.GetComponent<BoxControl>();
            if (!boxControl.TryToMoveInDirection(_targetDirection))
            {
                return;
            }

            _moving = true;
        }

        if (hit.collider == null)
        {
            _moving = true;
        }
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
}