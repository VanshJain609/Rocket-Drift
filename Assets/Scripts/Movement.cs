using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputAction thrustMovement;
    [SerializeField] private InputAction rotationMovement;
    [SerializeField] private float thrustStrength = 100.0f;
    [SerializeField] private float rotationStrength = 100.0f;
    
    public Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        thrustMovement.Enable();
        rotationMovement.Enable();
    }

    private void FixedUpdate()
    {
        Thrusting();
        Rotation();
    }

    void Thrusting()
    {
        if (thrustMovement.IsPressed())
        {
            myRigidbody.AddRelativeForce(Vector3.up * (thrustStrength * Time.fixedDeltaTime));
        }
    }

    void Rotation()
    {
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
