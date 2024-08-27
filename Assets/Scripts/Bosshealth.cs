using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bosshealth : MonoBehaviour
{
    public int maxHealth = 300;
    private int currentHealth;
    private Animator animator;
    private bool canTakeDamage = true;
    public float damageCooldown = 0.7f; // Cooldown duration after taking damage
    private Rigidbody rb;
    public Image healthBar;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentHealth = maxHealth; // Set current health to max health when the game starts
        animator = GetComponent<Animator>();
        UpdateHealthBar();
        // Get the default material of the enemy renderer

    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            healthBar.fillAmount = fillAmount; // Update the fill amount of the health bar Image
        }
    }

    // Method to decrease enemy health
    public void TakeDamage(int damageAmount)
    {
        if (canTakeDamage)
        {
            currentHealth -= damageAmount;
            UpdateHealthBar();
            // Check if the enemy is dead (health reaches zero)
            if (currentHealth <= 0)
            {
                Die(); // Implement your logic when the enemy dies
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                StartCoroutine(DamageCooldown());
 
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
        yield return new WaitForSeconds(5.0f); // Wait for 2.5 seconds (or your specified duration)
        SceneManager.LoadScene("WinScene");
        // Destroy the enemy GameObject after the delay
    }

    // Coroutine for damage cooldown duration
    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false; // Set to false to prevent further damage
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true; // Set back to true after cooldown duration
    }

    // Getter for current health value
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
