using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedPack : MonoBehaviour
{
    public int healthAmount = 50; // Amount of health the pickup restores

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming the "Player" has a script called "PlayerHealth" to handle health
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.Addhealth(healthAmount); // Restore player health
                Destroy(gameObject);// Disable the medpack after pickup
            }
        }
    }
}
