

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour
{

    public Material mat;

    public Vector2 pos;
    private Vector2 smoothPos;

    public float scale, angle;
    private float smoothScale, smoothAngle; 

    // to handle user input 
    private void UserInput()
    {
        // wasd (in, out, L, R)
        if (Input.GetKey("w"))
            //reduce scale by 1% each time
            scale *= .99f;
        if (Input.GetKey("a"))
            pos.x -= .002f * scale;
        if (Input.GetKey("s"))
            //increase scale by 1% each time
            scale *= 1.01f;       
        if (Input.GetKey("d"))
            pos.x += .002f * scale;

        // qe rotation
        if (Input.GetKey("e"))
            //reduce scale by 1% each time
            angle += .01f;
        if (Input.GetKey("q"))
            //reduce scale by 1% each time
            angle -= .01f;
    }
    private void UpdateShader()
    {

        // Lerp( start_val - when t=0, end_val - when t=1, t - interpolation value) ) 
        // RETURNS start_val + (end_val - start_val) * t 
        // position will interpolate between #s
        smoothPos = Vector2.Lerp(smoothPos, pos, 0.03f);
        smoothScale = Mathf.Lerp(smoothScale, scale, 0.03f);
        smoothAngle = Mathf.Lerp(smoothAngle, angle, 0.03f);

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
       
        // sets the interp_Angle float 
        mat.SetFloat("_Angle", smoothAngle);
    }

    void FixedUpdate()
    {
        UserInput(); 
        UpdateShader();
    }
}
