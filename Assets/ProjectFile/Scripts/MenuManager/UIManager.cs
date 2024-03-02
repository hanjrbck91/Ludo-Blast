using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Method to advance to the next scene index
    public void LoadNextScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Advance to the next scene index
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if there's a scene at the next index
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // If there's no next scene, print a message
            Debug.LogWarning("There is no next scene available.");
        }
    }
}
