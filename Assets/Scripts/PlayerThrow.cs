using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public int damageAmount = 25;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brute"))
        {
            Brutehealth enemyHealth = collision.collider.GetComponent<Brutehealth>();

            // Check if the enemy has a health script (similar to Brutehealth)
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log("Dealt Damage");
                Destroy(gameObject); // Destroy the thrown object when hitting an enemy or Ground
            }
        }

        if (collision.gameObject.CompareTag("Thrower"))
        {
            Throwerhealth throwerHealth = collision.collider.GetComponent<Throwerhealth>();

            // Check if the enemy has a health script (similar to Brutehealth)
            if (throwerHealth != null)
            {
                throwerHealth.TakeDamage(damageAmount);
                Debug.Log("Dealt Damage");
                Destroy(gameObject); // Destroy the thrown object when hitting an enemy or Ground
            }
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            Bosshealth enemyHealth = collision.collider.GetComponent<Bosshealth>();

            // Check if the enemy has a health script (similar to Brutehealth)
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log("Dealt Damage");
                Destroy(gameObject); // Destroy the thrown object when hitting an enemy or Ground
            }
        }
        else if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Drone"))
        {
            Destroy(gameObject);
        }
    }

}