using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("FirstLevel"); // Load the "Level1" scene
    }
}
