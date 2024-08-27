using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float accelerationTime = 2f;
    public float jumpForce = 8f;
    private float currentSpeed = 0f;
    private Rigidbody rb;
    private Animator animator;
    private bool canJump = true;
    private bool facingRight = true; // New variable to track the player's facing direction
    public GameObject hatPrefab;
    public Transform hatStartPoint; // Reference to the hat starting point object on the player
    public float throwDistance = 1f; // Adjust the throw distance as needed
    private bool canAttack = true; // New variable to control attack cooldown
    public bool isGrounded = true; // Track whether the player is grounded
    public int RoadDamage = 200;

    public GameObject objectToActivate; // Reference to the GameObject to be activated
    public GameObject deactivatehat; // Reference to the GameObject to be activated
    private bool canDeactivateObject = false; // Track whether the GameObject should be deactivated

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
       
    }

    void Update()
    {
        if (canJump && Input.GetButtonDown("Jump"))
        {
            Jump();
            PlayJumpAnimation(); // Trigger jump animation when jumping
        }

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();

            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
                canDeactivateObject = true;
                deactivatehat.SetActive(false);
                StartCoroutine(DeactivateAfterDelay());
            }
        }
        if (Input.GetMouseButtonDown(1) && canAttack)
        {
            ThrowHat();
        }

        if (!isGrounded)
        {
            // Play falling animation
            animator.SetBool("IsFalling", true);
        }
        // Add logic for other actions here if needed
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Move(horizontalInput);

        // Check if the player is not moving
        if (Mathf.Abs(horizontalInput) < 0.01f && canJump)
        {
            SetIdleAnimation();
        }
    }

    void Move(float input)
    {
        float targetSpeed = moveSpeed * input;
        currentSpeed = Mathf.SmoothStep(currentSpeed, targetSpeed, Time.time / accelerationTime);
        Vector3 velocity = new Vector3(currentSpeed, rb.velocity.y, 0);
        rb.velocity = velocity;

        if (currentSpeed < 0 && facingRight) // Player is moving left
        {
            Flip();
        }
        else if (currentSpeed > 0 && !facingRight) // Player is moving right
        {
            Flip();
        }

        animator.SetFloat("Speed", Mathf.Abs(currentSpeed)); // Set animation parameter based on movement speed
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180f);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        StartCoroutine(JumpCooldown());
        canJump = false;
    }

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(1f); // Adjust the delay duration here (1 second in this case)
        canJump = true;
    }
    void Attack()
    {
        StartCoroutine(AttackCooldown());

        // Trigger attack animation
        animator.SetTrigger("Attack");
    }

    IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(1.0f); // Adjust as needed for deactivation delay

        // Deactivate the object after the delay
        if (canDeactivateObject && objectToActivate != null)
        {
            deactivatehat.SetActive(true);
            objectToActivate.SetActive(false);
            canDeactivateObject = false;
        }
    }

    void ThrowHat()
    {
        // Ensure the hatStartPoint is not null before proceeding
        if (hatStartPoint != null)
        {
            // Calculate the hat's position relative to the hat starting point
          //  Vector3 throwOffset = hatStartPoint.forward * throwDistance;

            // Calculate the position in world space for the hat
            Vector3 throwPosition = hatStartPoint.position;

            // Instantiate the hat prefab at the calculated position
            GameObject hat = Instantiate(hatPrefab, throwPosition, Quaternion.identity);

            // Add force to the hat in the forward direction of the hat starting point
            float throwForce = 15f; // Adjust throw force as needed
            Rigidbody hatRb = hat.GetComponent<Rigidbody>();
            if (hatRb != null)
            {
                hatRb.AddForce(hatStartPoint.forward * throwForce, ForceMode.Impulse);
            }

            StartCoroutine(AttackCooldown()); // Start attack cooldown (assuming throwing the hat counts as an attack)
            animator.SetTrigger("Throw");
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        // Adjust the attack cooldown duration here (e.g., 1 second)
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }


    void SetIdleAnimation()
    {
        animator.SetFloat("Speed", 0f); // Set speed parameter to 0 for idle animation
    }

    void PlayJumpAnimation()
    {
        animator.SetTrigger("Jump"); // Trigger jump animation
    }

    void OnCollisionEnter(Collision other) 
    { 
        if (other.gameObject.CompareTag("Road"))
        {
            PlayerHealth playerHealth = other.collider.GetComponent<PlayerHealth>();

            // Deal damage to the player using the PlayerHealth script
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(RoadDamage);
            }
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("IsFalling", false); // Stop playing falling animation
            canJump = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Road"))
        {
            canJump = false;
            isGrounded = false;
        }
    }
}