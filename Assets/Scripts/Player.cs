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

    // === Propreties linked to animation === //

    private Animator _animator;

    // === Properties linked to double jump === //

    public int extraJumpsValue = 2;
    
    private int _extraJumpsLeft;
    void Start()
    {
        // Get the Rigidbody2D of the player
        _playerRigidBody = GetComponent<Rigidbody2D>();

        // Get the Animator component
        _animator = GetComponent<Animator>();

        // Initialize the number of extraJumpsValue left
        _extraJumpsLeft = extraJumpsValue;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(_isGrounded) {
                // Same logic as left and right, the first argument does not affect anything, the second jumps
                _playerRigidBody.linearVelocity = new Vector2(_playerRigidBody.linearVelocity.x, jumpForce);
            }
            // Allows another jump if extra jump are left
            else if (_extraJumpsLeft > 0) {
                _playerRigidBody.linearVelocity = new Vector2(_playerRigidBody.linearVelocity.x, jumpForce);
                // Substract one extra jump
                _extraJumpsLeft--;
            }
        }

        // Resets the number of extra jumps allowed if the player touches the ground
        if (_isGrounded)
        {
            _extraJumpsLeft = extraJumpsValue;
        }
        
        // Check which animation to play
        SetAnimation(moveInput);

    }

    private void FixedUpdate()
    {
        // We make a small invisible circle at the player's feet that will check :
        // If if has a collision with a GameObject Layer 6 : Ground
        // Will store true if yes, false if not (in the air, falling...)
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // Function to check which animation to play on which circumstances
    private void SetAnimation(float moveInput)
    {
        // All the scenarios where the player is on the ground
        if (_isGrounded)
        {
            // This means the player is on the ground and not walking (neither -1 or 1), play "Player_idle"
            if (moveInput == 0)
            {
                _animator.Play("Player_idle");
            }
            // This means the player is grounded and moving
            else
            {
                _animator.Play("Player_run");
            }
        }
        // The player is not on the ground
        else
        {
            // If y > 0, it means the player is going up (jump)
            if (_playerRigidBody.linearVelocityY > 0)
            {
                _animator.Play("Player_jump");
            }
            // That means y < 0, so the player is falling
            else
            {
                _animator.Play("Player_fall");
            }
        }
    }
}
