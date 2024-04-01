using System.Timers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float acceleration = 10f;
    public float maxSpeed = 10f;
    public float linearDrag = 5f;
    public float boost = 0f;
    private Vector2 movement;
    private ConnectionManager _connectionManager;
    private Vector2 _targetPosition;
    private Vector2 _previousPosition;
    private float elapsed = 0f;
    private bool _randomizeMovement = true;
    private float _randomizeElapsed = 0f;

    public void Start()
    {
        _previousPosition = rb.position;
        // Initialize game room.
        _connectionManager = gameObject.AddComponent<ConnectionManager>();
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        _randomizeElapsed += Time.deltaTime;
        // Update object movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        // Disable random movement by pressing R
        if (Input.GetKeyDown(KeyCode.R))
        {
            _randomizeMovement = !_randomizeMovement;
        }
        // Randomize the movement every second
        if (_randomizeMovement && _randomizeElapsed <= 0.8f)
        {
            movement.x = Random.Range(-1f, 1f);
            movement.y = Random.Range(-1f, 1f);
        }
        if (_randomizeMovement && _randomizeElapsed >= 1.0f)
        {
            movement.x = 0;
            movement.y = 0;
        }
        if (_randomizeMovement && _randomizeElapsed >= 1.5f)
        {
            _randomizeElapsed = 0f;
        }
        // If space is pressed, boost
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(movement.normalized * acceleration * boost);
        }
        // Send the player's position to the server when the player moves or every second
        if (_previousPosition != rb.position || elapsed >= 1f)
        {
            elapsed = 0f;
            _previousPosition = rb.position;
            _connectionManager.PlayerPosition(rb.position);
        }
    }

    void FixedUpdate()
    {
        // Apply force to move the character
        rb.AddForce(movement.normalized * acceleration);

        // limit the speed of the character
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

        // slow down the character when not pressing anything
        if (movement == Vector2.zero)
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
}
