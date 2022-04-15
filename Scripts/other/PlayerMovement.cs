using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // reference to rigidbody component 
    public Rigidbody rb;

    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;

    private void HandleInput()
    {
        if (Input.GetKey("d"))
        {
            // ForceMode - the way in which you should add a force
            // directly edits velocity of object, completely ignoring its mass 
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
    }
    
    void FixedUpdate()
    {
        // add constant movement 
        rb.AddRelativeForce(1, 0, forwardForce * Time.deltaTime);
        HandleInput();

    }

}
