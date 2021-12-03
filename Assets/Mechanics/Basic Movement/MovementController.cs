using UnityEngine;

/**
 * Movement mechanic.
 * Move the avatar using the arrow keys.
 * Change the sprite to the correct sprite based on last movement direction.
 * Do not teleport - gradual movement.
 * While moving - do not take new movement input.
 */
public class MovementController : MonoBehaviour
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
    private Rigidbody2D myRigidBody;

    private float _distancePercentage;
    private Vector2 _lastPosition;
    private bool _moving;


    private Vector2 _targetDirection;

    private void Start()
    {
        _lastPosition = myRigidBody.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_moving) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            mySpriteRenderer.sprite = sprites[2];
            _targetDirection = Vector2.left;
            _moving = true;
            // myRigidBody.MovePosition(myRigidBody.position + Vector2.left);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mySpriteRenderer.sprite = sprites[0];
            _targetDirection = Vector2.up;
            _moving = true;

            // myRigidBody.MovePosition(myRigidBody.position + Vector2.up);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            mySpriteRenderer.sprite = sprites[3];
            _targetDirection = Vector2.right;
            _moving = true;

            // myRigidBody.MovePosition(myRigidBody.position + Vector2.right);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mySpriteRenderer.sprite = sprites[1];
            _targetDirection = Vector2.down;
            _moving = true;

            // myRigidBody.MovePosition(myRigidBody.position + Vector2.down);
        }
    }

    private void FixedUpdate()
    {
        if (!_moving)
            return;

        _distancePercentage += 1 / updatesCountInMovement;
        _distancePercentage = _distancePercentage >= 1 ? 1 : _distancePercentage;

        myRigidBody.MovePosition(_lastPosition + _distancePercentage * _targetDirection);

        if (!(_distancePercentage >= 1))
            return;

        _distancePercentage = 0;
        _lastPosition += _targetDirection;
        _moving = false;
    }
}