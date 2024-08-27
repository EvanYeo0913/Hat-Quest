using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerController : MonoBehaviour
{
    public float attackRange = 10f;
    public Transform player;
    private Animator animator;
    private bool isFacingRight = false;
    static public Vector3 lastKnownPlayerPosition;
    private bool isAttacking = false;
    private float attackCooldown = 0f;
    public Transform ThrowPoint;
    public GameObject grenade;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Mathf.Abs(transform.position.x - player.position.x);
        Vector3 direction = (player.position - transform.position).normalized;
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        if (isAttacking && attackCooldown <= 0)
        {
            isAttacking = false;
        }

        if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
        {
            Flip();
        }

        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(Vector3.up, 180f);
    }

    void Attack()
    {
        if (!isAttacking && attackCooldown <= 0)
        {
            lastKnownPlayerPosition = player.position;
            isAttacking = true;
            animator.SetTrigger("Attack"); // Play attack animation
            // Perform attack logic here
            // Set attack cooldown
            Invoke("ThrowGrenade", 1.8f);
            attackCooldown = 4f;
        }
    }

    void ThrowGrenade()
    {
        Instantiate(grenade, ThrowPoint.position, Quaternion.identity);
    }
}
