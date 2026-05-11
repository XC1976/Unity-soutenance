using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5f;

    // === Properties === //
    private Rigidbody2D _playerRigidBody; // Rigidbody of the player
    public float jumpForce = 10f;   // The force used for jump

    // === These properties are used to stop the player from jumping infinitely in the air. === //
    // We created a small invisible GameObject (circle) at the player's feet (groundCheck).
    public Transform groundCheck;
    // Radius of the invisible circle
    public float groundCheckRadius = 0.2f;
    // We store which layer it will check
    public LayerMask groundLayer;
    // true if grounded, false if not
    private bool _isGrounded;
    
    void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input.GetAxis("Horizontal") is a built-in Unity method
        // If a "left" key is pressed (q or left arrow), -1 is assigned
        // If a "right" key is pressed (d or right arrow), 1 is assigned
        float moveInput = Input.GetAxis("Horizontal");

        // For the first argument, moveInput is either going to be -1 (left) or 1 (right) * moveSpeed
        // ==> The second arguments basically does not do anything
        // _playerRigidBody.linearVelocity.y represents the current acceleration up and down (-3.5, 0, 1.8)
        _playerRigidBody.linearVelocity = new Vector2(moveInput * moveSpeed, _playerRigidBody.linearVelocity.y);

        // If space key is pressed
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            // Same logic as left and right, the first argument does not affect anything, the second jumps
            _playerRigidBody.linearVelocity = new Vector2(_playerRigidBody.linearVelocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        // We make a small invisible circle at the player's feet that will check :
        // If if has a collision with a GameObject Layer 6 : Ground
        // Will store true if yes, false if not (in the air, falling...)
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
