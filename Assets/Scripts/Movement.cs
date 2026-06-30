using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Declaration of all the variables
    [SerializeField] private InputAction thrustMovement;
    [SerializeField] private InputAction rotationMovement;
    [SerializeField] private float thrustStrength = 100.0f;
    [SerializeField] private float rotationStrength = 100.0f;
    
    // Declaration of Rigid Body 
     Rigidbody myRigidbody;

    private void Start()
    {
        // Assigning of the Rigid Body
        myRigidbody = GetComponent<Rigidbody>();
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
    }

    void RightRotation()
    {
        ApplyRotation(rotationStrength);
    }

    void LeftRotation()
    {
       ApplyRotation(-rotationStrength);
    }

    void ApplyRotation(float rotationPerFrame)
    {
        transform.Rotate(Vector3.forward *(rotationPerFrame * Time.fixedDeltaTime));
    }
}
