
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


}
