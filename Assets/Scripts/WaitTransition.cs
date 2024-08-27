using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitTransition : MonoBehaviour
{
    public string targetSceneName = "TargetScene"; // Change this to the name of your target scene
    public float delayInSeconds = 10f;

    private void Start()
    {
        // Invoke the TransitionToScene method after the specified delay
        Invoke("TransitionToScene", delayInSeconds);
    }

    private void TransitionToScene()
    {
        // Load the target scene
        SceneManager.LoadScene(targetSceneName);
    }
}

