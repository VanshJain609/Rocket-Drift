
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    [SerializeField] private float delayTime = 2.0f;
    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        // To make the working of objects by using tags 
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You have hit friendly");
                break;

            case "Finish":
                Debug.Log("You have hit finish");
                StartLevelSequence();
                break;

            default:
                Debug.Log("You don't hit anything");
                CrashLevelSequence();
                break;

        }
    }

    void StartLevelSequence()
    {
        // To add delay in loading the next level
        Invoke("LoadNextLevel", delayTime);
    }

    void CrashLevelSequence()
    {
        // To add delay in restarting the level
        Invoke("RestartLevel",  delayTime);
    }
    
    void LoadNextLevel()
    {
        // To load the next level
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        // if all the levels are completed then restart the first level 
            if (nextScene == SceneManager.sceneCountInBuildSettings)
            {
                    nextScene = 0;
            }
        SceneManager.LoadScene(nextScene);
    }
            
    void RestartLevel()
    {
        // To restart the level
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
