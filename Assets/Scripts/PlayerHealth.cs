using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private bool canTakeDamage = true;
    public float damageCooldown = 0.7f;
    public GameObject objectToActivate;

    public Image healthBar; // Reference to the UI Slider component representing the health bar

    void Start()
    {
        currentHealth = maxHealth; // Set current health to max health when the game starts
        UpdateHealthBar(); 
    }

   

    // Method to decrease player health
    public void TakeDamage(int damageAmount)
    {
        if (canTakeDamage == true)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                Die(); // Implement your logic when the enemy dies
            }
            else
            {
                UpdateHealthBar();
                StartCoroutine(DamageCooldown());
            }

        }
    }

    public void Addhealth(int health)
    {
      health = maxHealth;

      currentHealth = health;

      currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
      UpdateHealthBar();

    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false; // Set to false to prevent further damage
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true; // Set back to true after cooldown duration
    }

    // Method to handle player death
    private void Die()
    {
        // Implement your logic for player death (e.g., game over, respawn logic, etc.)
        // Example: Reload the level, show game over screen, etc.
        objectToActivate.SetActive(true);
    }

    // Method to update the health bar UI
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            healthBar.fillAmount = fillAmount; // Update the fill amount of the health bar Image
        }
    }

    // Getter for current health value
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            TakeDamage(300);
        }
    }
}