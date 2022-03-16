

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour
{

    public Material mat;

    public Vector2 pos;
    private Vector2 smoothPos;

    public float scale;
    private float smoothScale; 

    // to handle user WASD input 
    private void UserInput()
    {
        if (Input.GetKey("w"))
        {
            //reduce scale by 1% each time
            scale *= .99f;
        }

        if (Input.GetKey("a"))
        {
            pos.x -= .001f * scale;
        }

        if (Input.GetKey("s"))
        {
            //increase scale by 1% each time
            scale *= 1.01f;
        }
       
        if (Input.GetKey("d"))
        {
            pos.x += .001f * scale;
        }
    }
    private void UpdateShader()
    {
        // position will interpolate between 
        smoothPos = Vector2.Lerp(smoothPos, pos, 0.03f);

        smoothScale = Mathf.Lerp(smoothScale, scale, 0.03f);
        // fix resizing of fractal to aspect ratio of the screen
        float aspect = (float)Screen.width / (float)Screen.height;
        float scaleX = smoothScale;
        float scaleY = smoothScale;

        // if the aspect is larger than 1 then Screen width must be a larger # than Screen height  
        if (aspect > 1f)

            scaleY /= aspect;

        else
            scaleX *= aspect;
        // sets _Area vector4(x, y , z, w ) 
        // pos.x - x component of Vector2 ; pos.y - y component of Vector2 
        mat.SetVector("_Area", new Vector4(pos.x, pos.y, scaleX, scaleY)); // setting _Area vector4 (x, y, z, w) 
    }

    void FixedUpdate()
    {
        UserInput(); 
        UpdateShader();
    }
}
