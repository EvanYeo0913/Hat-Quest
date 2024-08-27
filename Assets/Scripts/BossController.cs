
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 3f;
    public float followRange = 30f;
    public string[] attackAnimations = { "attack1", "attack2", "attack3", "attack4" };
    public Transform player;
    private Animator animator;
    private bool isFacingRight = false;

    private bool canMove = true; // Control whether the enemy can move
    private bool isWalking = false;
    private bool isAttacking = false;
  //  private bool candodamage = false;
    private float attackCooldown = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
        {
            Flip();
        }

        if (player == null)
            return;

       // bool playerIsGrounded = player.GetComponent<PlayerController>().isGrounded;
        float distanceToPlayer = Mathf.Abs(transform.position.x - player.position.x);

        if (distanceToPlayer <= followRange && canMove)
        {
            MoveTowardsPlayer();

            if (distanceToPlayer <= attackRange)
            {
                Attack();
            }
        }

        else
        {
            animator.SetBool("IsWalking", false);
        }

        // Check for attack cooldown
        if (attackCooldown > 0)
        {

            attackCooldown -= Time.deltaTime;
        }

        // Check if attacking and attack cooldown is finished
        if (isAttacking && attackCooldown <= 0)
        {
           // candodamage = false;
            isAttacking = false;
            SetCanMove(true); // Allow movement after attack
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        // Move only along the X-axis towards the player
        Vector3 movement = new Vector3(direction.x, 0, 0) * moveSpeed * Time.deltaTime;

        if (movement.magnitude > 0)
        {
            // Set isWalking to true when there is movement
            isWalking = true;

            // Update the animator's IsWalking parameter to trigger the walking animation
            animator.SetBool("IsWalking", false); // Stop the walking animation if moving
        }
        else
        {
            isWalking = false;
        }

        transform.Translate(movement, Space.World); // Use Space.World to move in world coordinates

    }

        public void Attack()
    {
        if (!isAttacking && attackCooldown <= 0)
        {
            isAttacking = true;

            // Randomly select an attack animation from the array
            int randomIndex = Random.Range(0, attackAnimations.Length);
            string selectedAnimation = attackAnimations[randomIndex];

            // Play the selected attack animation
            animator.SetTrigger(selectedAnimation);

            attackCooldown = 5f; // Set attack cooldown
            SetCanMove(false); // Prevent movement during attack
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(Vector3.up, 180f);
    }

    // Method to enable/disable enemy movement
    public void SetCanMove(bool move)
    {
        canMove = move;
        animator.SetBool("IsWalking", move); // Stop walking animation if cannot move
    }
}

