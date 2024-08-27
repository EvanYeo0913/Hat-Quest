using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 15f;
    public float moveSpeed = 5f;
    private bool isFacingRight = false;
    private bool isMoving = false;
    private Vector3 initialPosition;
    public GameObject explosionPrefab;

    void Start()
    {
        initialPosition = transform.position; // Store the initial position of the drone
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.position.x, player.position.y));
        Vector3 direction = (player.position - transform.position).normalized;

        if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
        {
            Flip();
        }


        if (distanceToPlayer <= detectionRange && !isMoving)
        {
            MoveTowardsPlayer();
        }

        void Flip()
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(Vector3.up, 180f);
        }

        if (isMoving)
        {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y + 0.5f, initialPosition.z);

            // Move the drone towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the drone reached the target position
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(targetPosition.x, targetPosition.y)) <= 0.1f)
            {
                isMoving = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the player or ground
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("ThrownHat"))
        {
            Explode();
        }
    }

    void MoveTowardsPlayer()
    {
        isMoving = true;
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}