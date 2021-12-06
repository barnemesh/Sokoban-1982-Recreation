using System;
using UnityEngine;

/**
 * Player avatar controller.
 */
public class Movement : MonoBehaviour
{
    #region Properties

    /**
     * Is movement Paused?
     */
    public bool Pause { get; set; }

    #endregion


    #region Inspector

    [SerializeField]
    [Tooltip("How many updates should a single movement take")]
    private float updatesCountInMovement = 4.0f;

    // [SerializeField]
    // [Tooltip("Up, Down, Left, Right")]
    // private Sprite[] sprites;

    [System.Serializable]
    public struct Sprites
    {
        public Sprite up;
        public Sprite down;
        public Sprite left;
        public Sprite right;
    }

    [SerializeField]
    private Sprites _sprites;

    [SerializeField]
    private SpriteRenderer mySpriteRenderer;

    [SerializeField]
    private Rigidbody2D myRigidbody;

    #endregion


    #region Private Fields

    private Vector2 _targetDirection;
    private Vector2 _lastPosition;
    private bool _moving;
    private float _distancePercentage;

    #endregion


    #region Monobehaviour

    private void Start ()
    {
        _lastPosition = myRigidbody.position;
        LevelGameManager.Player = this;
    }

    // Update is called once per frame
    private void Update ()
    {
        if ( Pause || _moving ) return;

        // Choose movement direction based on input, i not already moving.
        _targetDirection = Vector2.zero;
        if ( Input.GetKeyDown(KeyCode.LeftArrow) )
        {
            mySpriteRenderer.sprite = _sprites.left;
            _targetDirection = Vector2.left;
        }

        if ( Input.GetKeyDown(KeyCode.UpArrow) )
        {
            mySpriteRenderer.sprite = _sprites.up;
            _targetDirection = Vector2.up;
        }

        if ( Input.GetKeyDown(KeyCode.RightArrow) )
        {
            mySpriteRenderer.sprite = _sprites.right;
            _targetDirection = Vector2.right;
        }

        if ( Input.GetKeyDown(KeyCode.DownArrow) )
        {
            mySpriteRenderer.sprite = _sprites.down;
            _targetDirection = Vector2.down;
        }

        if ( _targetDirection == Vector2.zero ) return;

        // if we need to move - check if we can move in the desired direction.

        var hit = Physics2D.Raycast(myRigidbody.position, _targetDirection, 1.0f);

        // if there is a box, check if it can be moved before moving.
        if ( hit.collider != null && hit.collider.CompareTag("Box") )
        {
            var boxControl = hit.collider.GetComponent<BoxControl>();
            if ( !boxControl.TryToMoveInDirection(_targetDirection) ) return;

            _moving = true;
        }

        if ( hit.collider == null ) _moving = true;
    }

    private void FixedUpdate ()
    {
        if ( Pause || !_moving )
            return;

        // If we need to move, use exactly updatesCountInMovement to finish the entire movement.
        _distancePercentage += 1 / updatesCountInMovement;
        _distancePercentage = _distancePercentage >= 1 ? 1 : _distancePercentage;

        myRigidbody.MovePosition(_lastPosition + _distancePercentage * _targetDirection);

        if ( !(_distancePercentage >= 1) )
            return;

        _distancePercentage = 0;
        _lastPosition += _targetDirection;
        _moving = false;
    }

    #endregion
}