using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 1.3f;
    public float followRange = 8f;

    public Transform player;
    private Animator animator;
    private bool isFacingRight = false;

    private bool canMove = true; // Control whether the enemy can move

    private bool isAttacking = false;
   // private bool candodamage = false;
    private float attackCooldown = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null)
            return;

        bool playerIsGrounded = player.GetComponent<PlayerController>().isGrounded;
        float distanceToPlayer = Mathf.Abs(transform.position.x - player.position.x);

        if (playerIsGrounded && distanceToPlayer <= followRange && canMove)
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
        animator.SetBool("IsWalking", true);

        Vector3 direction = (player.position - transform.position).normalized;

        // Move only along the X-axis towards the player
        Vector3 movement = new Vector3(direction.x, 0, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World); // Use Space.World to move in world coordinates

        if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
        {
            Flip();
        }
    }

    public void Attack()
    {
        if (!isAttacking && attackCooldown <= 0)
        {
            isAttacking = true;
          //  candodamage = true;
            animator.SetTrigger("Attack"); // Play attack animation
            // Perform attack logic here
            // Set attack cooldown
            attackCooldown = 2f;
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