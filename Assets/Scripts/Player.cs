using UnityEngine;
using System.Collections;   // Used for IEnumerator / Coroutine
using UnityEngine.UI;

public class Player : MonoBehaviour
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

    public int extraJumpsValue = 1;

    private int _extraJumpsLeft;

    // === Properties linked to player taking damage === //
    
    public int health = 100;
    // We store the sprite renderer to make the player blink red when taking damage or other effects in the future 
    private SpriteRenderer _spriteRenderer;

    // === Properties linked to the health bar in the UI === //

    public Image healthImage;

    // === Properties linked to coins === //

    public int coins;
    
    void Start()
    {
        // Get the Rigidbody2D of the player
        _playerRigidBody = GetComponent<Rigidbody2D>();

        // Get the Animator component
        _animator = GetComponent<Animator>();

        // Initialize the number of extraJumpsValue left
        _extraJumpsLeft = extraJumpsValue;

        // To make the player blink red when taking damage
        _spriteRenderer = GetComponent<SpriteRenderer>();
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

        healthImage.fillAmount = health / 100f;
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

    // Built-in Unity method that will trigger when an incoming collider makes contact
    // with this object collider
    // Link : https://docs.unity3d.com/6000.0/Documentation/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player touches an object with the tag "Damageq"
        if (collision.gameObject.tag == "Damage")
        {
            // Reduce 25 health
            health -= 25;
            // Create a knockback effect, it is basically the exact same thing as a player jump
            _playerRigidBody.linearVelocity = new Vector2(_playerRigidBody.linearVelocity.x, jumpForce);

            // Makes the player blink red
            StartCoroutine(BlinkRed());

            if (health <= 0)
            {
                Die();
            }
        }
    }

    // Makes the player blink red (takes damage)
    // IEnumerator because it is used in a Coroutine, since it waits for 0.1 second
    private IEnumerator BlinkRed()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        // The default sprite color is white
        _spriteRenderer.color = Color.white;
    }

    // Reload the scene, basically restart the game
    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
}
