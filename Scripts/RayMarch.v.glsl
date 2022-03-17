Shader "Unlit/RayMarch"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            

            #include "UnityCG.cginc"

            #define SURF_DIST 1e-3
            #define MAX_DIST 100
            #define MAX_STEPS 100 

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
        

              // vertex to fragment 
             // data structure that gets sent from vertex shader to fragment shader
            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float GetDist(float3 p) {

                // distance to ORIGIN of a sphere (origin of sphere) 
                float d = length(p) - .5;

                // Torus 
                d = length(float2(length(p.xy) - .5, p.z)) - .1;

                return d;
            }

            // return view depth (distance to surface) 
            float RayMarch(float3 ro, float3 rd) {

                // current distance from origin 
                float dO = 0; 
                float dS; 

                // marching 
                for (int i = 0; i < MAX_STEPS; i++) {
                    // calculate ray marching position
                    float3 p = ro + dO * rd; 

                    // calculate distance to surface
                    dS = GetDist(p); 

                    dO += dS; 

                    // check if you've hit an object or if youve surpassed object 
                    if (dS<SURF_DIST || dO > MAX_DIST) break;

                }
                return dO;
            }

            // take in the point that we hit the surface of obj at 
            float3 GetNormal(float3 p) {

                // epsilon
                float2 e = float2(1e-2, 0);

                // normal vector 
                float3 n = GetDist(p) - float3(
                    GetDist(p-e.xyy),
                    GetDist(p-e.yxy),
                    GetDist(p-e.yyx)
                    );
              
                return normalize(n);
            }

            // main fragment shader 

            fixed4 frag(v2f i) : SV_Target
            {

                // set origin to the middle of cube
                float2 uv = i.uv - .5;
                //camera looking at origin 
                float3 ro = float3(0,0,-3);
                float3 rd = normalize(float3(uv.x, uv.y, 1));

                float d = RayMarch(ro, rd);
                // sample the texture
                fixed4 col = 0;

                // if distance is smaller than max distance, you hit your surface
                if (d < MAX_DIST) {

                    // calculate point where surface was hit by ray 
                    float3 p = ro + rd * d; 

                    
                    float3 n = GetNormal(p);

                    // debug: test to see if n is working 
                    col.rgb = n;
                    
                }
               
                // debugging RayMarch function) 
               // col.rgb = RayMarch(ro, rd); 

                return col;
            }
            ENDCG
        }
    }
}
