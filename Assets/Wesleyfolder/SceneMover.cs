using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMover : MonoBehaviour
{
    
    // Check if the player is grounded
    // Update to work with 3D collision
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sewer")){
            SceneManager.LoadScene("Transition 3-4");
        }
        if (collision.gameObject.CompareTag("Baseball")){
            SceneManager.LoadScene("Transition 1-2");
        }
    }

}
