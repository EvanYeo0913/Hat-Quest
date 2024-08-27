using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Brutehealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private bool canTakeDamage = true;
    public float damageCooldown = 0.7f; // Cooldown duration after taking damage
    public Material defaultMaterial; // Reference to the default material of the enemy
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentHealth = maxHealth; // Set current health to max health when the game starts
        animator = GetComponent<Animator>();

        // Get the default material of the enemy renderer
        defaultMaterial = GetComponent<Renderer>().material;
    }

    // Method to decrease enemy health
    public void TakeDamage(int damageAmount)
    {
        if (canTakeDamage)
        {
            currentHealth -= damageAmount;

            // Check if the enemy is dead (health reaches zero)
            if (currentHealth <= 0)
            {
                Die(); // Implement your logic when the enemy dies
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                StartCoroutine(DamageCooldown());
                StartCoroutine(FlashRed());
            }
        }
    }

    // Method to handle enemy death
    private void Die()
    {
        // Implement your logic for enemy death (e.g., play death animation, drop items, etc.)
        animator.SetTrigger("Death");
        StartCoroutine(DestroyAfterAnimation());
    }

    // Coroutine to destroy the enemy after the death animation
    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(2.5f); // Wait for 2.5 seconds (or your specified duration)

        // Destroy the enemy GameObject after the delay
        Destroy(gameObject);
    }

    // Coroutine for damage cooldown duration
    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false; // Set to false to prevent further damage
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true; // Set back to true after cooldown duration
    }

    // Coroutine to flash red when damaged
    private IEnumerator FlashRed()
    {
        // Change the enemy's material color to red temporarily
        GetComponent<Renderer>().material.color = Color.red;

        // Wait for a short duration
        yield return new WaitForSeconds(0.1f); // Adjust the duration as needed

        // Revert back to the default material color
        GetComponent<Renderer>().material = defaultMaterial;

    }

    // Getter for current health value
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}