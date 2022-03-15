using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour
{

    public Material mat;
    public Vector2 pos;
    public float scale; 

    // Update is called once per frame
    void Update()
    {

       // fix resizing of fractal to aspect ratio of the screen
       float aspect 

        // set _Area vector4(x, y , z, w ) 
        // pos.x - x component of Vector2 ; pos.y - y component of Vector2 
        mat.SetVector("_Area", new Vector4(pos.x, pos.y, scale, scale)); // sets _Area vector4 (x, y, z, w) 
    }
}
