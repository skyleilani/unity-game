using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // reference to rigidbody component 
    public Rigidbody rb;

    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;


    // Update is called once per frame
    // FixedUpdate is based on unitys physics system - 
    // apparantely not very good for non-constant/random movements like jumping 
    void FixedUpdate()
    {
        // add constant movement 
        rb.AddForce(1, 0, forwardForce * Time.deltaTime);   


        // this will for sure change once you incorporate joysticks and remotes!! 
        // there are so many ways to get user input 
        if(Input.GetKey("d"))
        {
            Debug.Log("d pressed");
            // ForceMode - specifies the way in which you should add a force
            // directly edits velocity of the object, completely ignoring its mass 
            rb.AddForce(sidewaysForce  * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        
        if (Input.GetKey("a"))
        {
            Debug.Log("a pressed");

            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

       // if (Input.GetKey("w"))
        //{
         //   Debug.Log("w pressed");
          //  rb.AddForce(forwardForce* 2 * Time.deltaTime, 0, 0);
       // }

    }
}
