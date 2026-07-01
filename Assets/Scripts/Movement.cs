using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Declaration of all the variables
    [SerializeField] private InputAction thrustMovement;
    [SerializeField] private InputAction rotationMovement;
    [SerializeField] private float thrustStrength = 100.0f;
    [SerializeField] private float rotationStrength = 100.0f;
    [SerializeField] private AudioClip thrustEngine;
    [SerializeField] private ParticleSystem engineParticle;
    [SerializeField] private ParticleSystem leftSideParticle;
    [SerializeField] private ParticleSystem rightSideParticle;
   
    
    // Declaration of Rigid Body 
     Rigidbody myRigidbody;
     AudioSource audioSource;
    

    private void Start()
    {
        // Assigning of the Rigid Body
        myRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // Enabling the Thrust Movement 
        thrustMovement.Enable();
        // Enabling the Rotation Movement 
        rotationMovement.Enable();
    }

    private void FixedUpdate()
    {
        // Calling the movements
        Thrusting();
        Rotation();
    }

    void Thrusting()
    {
        // if pressed the rocket will move in upward directions
        if (thrustMovement.IsPressed())
        {
            myRigidbody.AddRelativeForce(Vector3.up * (thrustStrength * Time.fixedDeltaTime));

            // if audio is not playing then play the sound 
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thrustEngine);
            }

            if (!engineParticle.isPlaying)
            {
                engineParticle.Play();
            }
        }
        else
        {
            audioSource.Stop();
            engineParticle.Stop();
        }
    }

    void Rotation()
    {
        // To get the value of the key pressed 
         float rotationInput = rotationMovement.ReadValue<float>();
         
         
         if (rotationInput < 0)
         {
             RightRotation();
         }
         else if (rotationInput > 0)
         {
             LeftRotation();
         }
         else
         {
             StopRotation();
         }
    }
    
    void RightRotation()
    {
        ApplyRotation(rotationStrength);
        if (!rightSideParticle.isPlaying)
        {
            rightSideParticle.Play();
        }
    }

    void LeftRotation()
    {
       ApplyRotation(-rotationStrength);
       if (!leftSideParticle.isPlaying)
       {
           leftSideParticle.Play();
       }
    }

    void StopRotation()
    {
        rightSideParticle.Stop();
        leftSideParticle.Stop();
    }

    void ApplyRotation(float rotationPerFrame)
    {
        myRigidbody.freezeRotation = false;
        transform.Rotate(Vector3.forward *(rotationPerFrame * Time.fixedDeltaTime));
        myRigidbody.freezeRotation = true;
    }
}