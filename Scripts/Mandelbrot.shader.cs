Shader "Explorer/Mandelbrot"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag(v2f i) : SV_Target
            {
                // mandelbrot fractal algorithm 

                // start with start position, initialize to uv coordinate. 
                float2 start = i.uv; 

                // keep track of where pixel is jumping across the string
                float2 track; 

                for (float i = 0; i < 255; i++) {
                    // update track value based on previous track value
                    track = float2(track.x * track.x - track.y * track.y, 2 * track.x * track.y) + start; 

                    // breakout of loop
                    if (length(track) > 2) break; 
                }

                return i/255;
            }
            ENDCG
        }
    }
}