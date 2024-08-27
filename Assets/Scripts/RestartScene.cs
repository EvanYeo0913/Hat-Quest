using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public void LoadLevel1()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex;
       
            SceneManager.LoadScene(nextSceneIndex);
        
    }
}
