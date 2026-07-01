
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Collision : MonoBehaviour
{
    
    [SerializeField] private float delayTime = 2.0f;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip coinCollectSound;
    [SerializeField] private ParticleSystem successParticle;
    [SerializeField] private ParticleSystem crashParticle;
    [SerializeField] private TMP_Text coinText;

    private int coinCount = 0;
    private int coins;
    
    private bool isControllable = true;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        // if rocket is not in control then return 
        if(!isControllable) { return;}
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
            
            case "Coin" :
                Debug.Log("You have hit coin");
                CoinCollector(other.gameObject);
                break;

            default:
                Debug.Log("You don't hit anything");
                CrashLevelSequence();
                break;

        }
    }

    void StartLevelSequence()
    { 
        // Disable all the controls
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        // To add delay in loading the next level
        Invoke("LoadNextLevel", delayTime);
    }

    void CrashLevelSequence()
    {
        // Disable all the controls
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
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

    void CoinCollector(GameObject coin)
    {
        // To increase the coin count by 1
        coinCount++;
        // To play the coin collect sound 
        audioSource.PlayOneShot(coinCollectSound);
        // To add the text in the viewport
        coinText.text = "Coins: " + coinCount;
        // To destroy the coin after collection 
        Destroy(coin);
    }
}
