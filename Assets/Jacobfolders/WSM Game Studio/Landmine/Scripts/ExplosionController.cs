using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSMGameStudio.Bombs
{
    public class ExplosionController : MonoBehaviour
    {
        public float radius = 5f;
        public int damage = 50;

        void Start()
        {
            Explode();
            Destroy(gameObject, 1.5f);
        }

        void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider hit in colliders)
            {
                if (hit.CompareTag("Player"))
                {
                    // Apply damage only to the player
                    PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();

                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damage);
                    }
                }
            }
        }
    }
}
