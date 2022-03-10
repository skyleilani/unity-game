using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement;
    public Rigidbody rb; 
    public int Timer;
    public float m_Thrust = 20f;

    private void OnCollisionEnter(Collision collisionInfo)
    {
       if (collisionInfo.collider.tag == "Obstacle")
        {
            movement.enabled = false;
        }
    }

    private void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            movement.enabled = true;
            Timer = 0;
        }
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            Debug.Log("in contact w collider ");
            Timer += 1;
            Debug.Log(Timer);

            // if the player's rigidbody has been in contact with the Obstacle object for
            // more than 3 seconds, I want there to be a force to stop it from staying in contact 
            // 
            if( Timer > 3)
            {
                rb.AddForce(transform.up * m_Thrust);
            }
            
            if (Input.GetKey("d"))
            {
                Debug.Log("collisionStay d");
                movement.enabled = true;               
            }

            if (Input.GetKey("a"))
            {
                Debug.Log("collisionStay a");
                movement.enabled = true;
            }
        }
    }


}
