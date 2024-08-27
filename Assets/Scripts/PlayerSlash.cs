using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    public int damageAmount = 35; // Damage amount to deal to enemies

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is an enemy
        if (collision.collider.CompareTag("Brute"))
        {
            Debug.Log("Enemy Hit");
            Brutehealth enemyHealth = collision.collider.GetComponent<Brutehealth>();

            // Check if the enemy has a health script (similar to Brutehealth)
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log("Dealt Damage");
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
           
            }
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            Brutehealth enemyHealth = collision.collider.GetComponent<Brutehealth>();

            // Check if the enemy has a health script (similar to Brutehealth)
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log("Dealt Damage");
              
            }
        }
    }
}
