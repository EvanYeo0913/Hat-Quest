using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    public int damageAmount = 30; // Damage amount to deal to the player

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the player
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();

            // Deal damage to the player using the PlayerHealth script
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }

}
