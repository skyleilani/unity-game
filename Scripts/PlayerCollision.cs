
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement; 

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
        }
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            Debug.Log("in contact w collider ");
            
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
