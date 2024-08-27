using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjust the speed as needed
    public float endPointX = 10f; // Adjust the endpoint as needed
    private float originalX;
    private int direction = 1; // 1 for right, -1 for left
    private bool playerOnPlatform = false;

    private void Start()
    {
        originalX = transform.position.x;
    }

    private void FixedUpdate()
    {
        if (playerOnPlatform)
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        float newX = transform.position.x + direction * moveSpeed * Time.fixedDeltaTime;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Check if the platform has reached the endpoint
        if ((direction == 1 && newX >= originalX + endPointX) || (direction == -1 && newX <= originalX))
        {
            // Change direction
            direction *= -1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
            // Make the player a child of the platform to move with it
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
            // Unparent the player from the platform
            collision.transform.parent = null;
        }
    }
}
