
using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Friendly" :
                Debug.Log("You have hit friendly");
                break;
            
            case "Finish" :
                Debug.Log("You have hit finish");
                break;
            
            default:
                Debug.Log("You don't hit anything");
                break;
            
        }
    }
}
