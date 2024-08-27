using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float maxHeight = 5f;
    public float minHeight = 0f;

    private bool goingUp = true;

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        float step = moveSpeed * Time.deltaTime;

        if (goingUp)
        {
            transform.Translate(Vector3.up * step);

            if (transform.position.y >= maxHeight)
            {
                goingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * step);

            if (transform.position.y <= minHeight)
            {
                goingUp = true;
            }
        }
    }
}
