using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f; // Adjust the jump force as needed
    private bool isGrounded;

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        float verticalInput = 0f;
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }

        MovePlayer(verticalInput, horizontalInput);

        // Check for jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void MovePlayer(float verticalInput, float horizontalInput)
    {
        // Move in the desired direction
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime;
        transform.Translate(movement, Space.World); // Use Space.World to move relative to the world axes
    }

    void Jump()
    {
        // Add upward force for jumping
        GetComponent<Rigidbody>().velocity = new Vector3(0, jumpForce, 0);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
