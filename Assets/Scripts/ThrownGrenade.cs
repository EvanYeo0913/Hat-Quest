using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ThrownGrenade : MonoBehaviour
{
    public GameObject explosionPrefab;
    private Transform playerTransform;
    public float throwForce = 2f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Calculate the direction to throw the grenade
        Vector3 throwDirection = (playerTransform.position - transform.position).normalized;

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Adjust the throw force based on the distance to ensure it reaches the player
        float adjustedForce = Mathf.Clamp(throwForce * distanceToPlayer, throwForce, throwForce * 2);

        // Apply force to the grenade in the calculated direction
        GetComponent<Rigidbody>().AddForce(throwDirection * adjustedForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the grenade hits the ground or any other collider
        Explode();
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

