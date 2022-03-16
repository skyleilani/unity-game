using UnityEngine;

//  FollowPlayer.cs - Attached to camera object, keeps the cameras focus on the player's game object. 

public class FollowPlayer : MonoBehaviour
{
    // remember transform is used whenever we are getting info about position, rotation, or scale
    public Transform player;
    public Vector3 distance_from_camera;

    //higher value = faster camera will lock onto player, slower value = more time it will spend smoothing 
    public float smoothSpeed = 0.125f; 
     

   // camera will follow player objects position per frame
    
    void FixedUpdate()
    { 
        // every frame we use targetposition to get the position we want to snap to 
        Vector3 targetPosition = player.position + distance_from_camera;

        // Lerp - linear interprelation ? process of smoothly going from point A to point B 
        // Lerp (start position, end position, float T(time) ) 
        // T is any value between 0 - 1, 
        // when it's 0... Lerp gives us start position, when it's 1 it will give us end position, and if it's anywhere between it will give a mix of the two 
       
        // we use smooth position to get a little bit closer to targetPosition every frame,
        // how much closer we get depends on our smoothspeed (whose value will be between 0-1) 
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothPosition;

        transform.LookAt(player);
    }
}
