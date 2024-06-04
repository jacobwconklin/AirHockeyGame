using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10.0F;
    public float rotateSpeed = 0.5F;

    private float currSpeed = 0f;

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        currSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * currSpeed);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Controller collider hit this: " + hit.transform.name);
        try
        {
            if (hit.collider.CompareTag("Puck")) {
                Rigidbody puckRb = hit.gameObject.GetComponent<Rigidbody>();
                Vector3 oldPuckVelocity = puckRb.velocity;
                // Vector3 newPuckVelocity = Vector3.Reflect(oldPuckVelocity, transform.TransformDirection(Vector3.forward) * currSpeed);
                Vector3 newPuckVelocity = transform.TransformDirection(Vector3.forward) * currSpeed;
                puckRb.velocity = newPuckVelocity;
            }   
        }
        catch (System.Exception)
        {
            Debug.LogError("Error in character collider I guess");
        }
    }
    
    void OnCollisionEnter( Collision collision)
    {
        Debug.Log("Collision detected in Paddle with: " + collision.transform.name);
    }
}
