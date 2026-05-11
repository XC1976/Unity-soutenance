using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5f;

    // === This will store the Rigidbody2D of the player. === //
    private Rigidbody2D _playerRigidBody;
    
    // ==> Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // ==> pdate is called once per frame
    void Update()
    {
        // Input.GetAxis("Horizontal") is a built-in Unity method
        // If a "left" key is pressed (q or left arrow), -1 is assigned
        // If a "right" key is pressed (d or right arrow), 1 is assigned
        float moveInput = Input.GetAxis("Horizontal");

        // For the first argument, moveInput is either going to be -1 (left) or 1 (right) * moveSpeed
        // _playerRigidBody.linearVelocity.y represents the current acceleration up and down (-3.5, 0, 1.8)
        // This second arguments allow to not affect other physics
        _playerRigidBody.linearVelocity = new Vector2(moveInput * moveSpeed, _playerRigidBody.linearVelocity.y);
    }
}
