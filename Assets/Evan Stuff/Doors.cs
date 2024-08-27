using UnityEngine;

public class DeactivateObject : MonoBehaviour
{
    public Transform playerTransform;
    public float deactivationDistance = 5f;

    private void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= deactivationDistance)
            {
                // Deactivate the object
                gameObject.SetActive(false);
            }
        }
    }
}
