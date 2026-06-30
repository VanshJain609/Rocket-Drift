
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        // To make the working of objects by using tags 
        switch(other.gameObject.tag)
        {
            case "Friendly" :
                Debug.Log("You have hit friendly");
                break;
            
            case "Finish" :
                Debug.Log("You have hit finish");
                LoadNextLevel();
                break;
            
            default:
                Debug.Log("You don't hit anything");
                RestartLevel();
                break;
            
        }

        void LoadNextLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            int nextScene = currentScene + 1;

            if (nextScene == SceneManager.sceneCountInBuildSettings)
            {
                nextScene = 0;
            }
            SceneManager.LoadScene(nextScene);
        }
        
        void RestartLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
    }
}
