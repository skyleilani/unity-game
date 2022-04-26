using UnityEngine;
using System.IO.Ports; 

public class Explorer : MonoBehaviour
{
    static SerialPort sp = new SerialPort(); // Create new SerialPort Object 

    public Material mat;

    public Vector2 pos;
    private Vector2 smoothPos;

    public float scale, angle;
    private float smoothScale, smoothAngle;

    private void Start()
    {    
            sp.Open(); // open connection 
            sp.ReadTimeout = 1;  
    }
    // to handle user input 
    private void UserInput()
    {
        if (sp.IsOpen)
        {
            // creating rotation for dir 
            float s = Mathf.Sin(angle);
            float c = Mathf.Cos(angle);

            // read bytes sent from arduino
            try
            {
                if (sp.ReadByte() == 1)
                {
                    //reduce scale by 1% each time
                    scale *= .99f;
                    Debug.Log(sp.ReadByte());
                }

                else if (sp.ReadByte() == 2)
                {
                    scale *= 1.01f;

                    Debug.Log(sp.ReadByte());
                } 
            }

            // throw an error  
            catch (System.Exception) { }
        }
    }
    private void UpdateShader()
    {

        // Lerp( start_val - when t=0, end_val - when t=1, t - interpolant value) ) 
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
    
    void OnDisable()
    {
        sp.Close();
    }

    void FixedUpdate()
    {
        UserInput(); 
        UpdateShader();
    }
    
}
