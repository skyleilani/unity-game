// shader based on https://www.youtube.com/watch?v=kY7liQVPQSc


Shader "Explorer/Mandelbrot"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    // holds area we will render (center, center, size, size) 
    _Area("Area", vector) = (0, 0, 4 , 4)

        // range of -pi to pi in radians
        _Angle("Angle", range(-3.1415, 3.1415)) = 0
    }
        SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // we will build our start value based on this area 
            float4 _Area;
            float _Angle;

            sampler2D _MainTex;

            // function rotate point in 2d space 
            // og_p - original point
            // piv_p -pivot point around w      // a - angle
            float2 rotate(float2 og_p, float piv_p, float a) {

                float s = sin(a);
                float c = cos(a);

                // set og_p to 00
                og_p -= piv_p;

                // rotate the original point 
                // new x = original point's X * cosine(angle) - original point's Y *sin(angle) 
                // new y = original point's X * sin(angle) + original point's Y * cosine(angle) 
                og_p = float2(og_p.x * c - og_p.y * s, og_p.x * s + og_p.y * c);

                // shift back to original pivot after rotation is done
                og_p += piv_p;

                return og_p;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // start with start position, initialize to uv coordinate. 
                float2 start = _Area.xy + (i.uv - 0.5) * _Area.zw; // .zw = last two coords (x, y, z, w) from _Area (4, 4) 
                start = rotate(start, _Area.xy, _Angle);
                // keep track of where pixel is jumping across the string
                float2 track;

                // mandelbrot fractal algorithm 
                for (float i = 0; i < 255; i++) {
                    // update track value based on previous track value
                    track = float2(track.x * track.x - track.y * track.y, 2 * track.x * track.y) + start;

                    // breakout of loop
                    if (length(track) > 2) break;
                }

                float4 rgba = sin(float4(0.15f, 0.08f, 25, 1) * i);
                return rgba;
            }
            ENDCG
        }
    }
}
