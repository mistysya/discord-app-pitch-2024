using UnityEngine;

public class MovableObjectController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float acceleration = 10f;
    public float maxSpeed = 10f;
    public float linearDrag = 5f;
    public float boost = 0f;
    private Vector2 movement;
    void Update()
    {
        // Update object movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        // Debug.Log(movement);
        // If space is pressed, boost
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(movement.normalized * acceleration * boost);
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
