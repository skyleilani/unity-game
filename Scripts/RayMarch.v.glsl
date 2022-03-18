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
                // datatype var_name : WHICH_REGISTER_FOR_GPU
                // tell gpu which register to use to store each var & its contained data
                // make sure you don't assign a register twice 

                float2 uv : TEXCOORD0;       
                float4 vertex : SV_POSITION;
                float3 ro : TEXCOORD1;
                float3 hitPos : TEXCOORD2;   // hit position
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            // vertex shader 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // vertex
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); // uv coord

                // ray origin 
                // _WorldSpaceCameraPos is float3
                o.ro = _WorldSpaceCameraPos;

                o.hitPos = v.vertex;
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

            // fragment shader to SV_Target register
            fixed4 frag(v2f i) : SV_Target
            { 
                float2 uv = i.uv-.5; // set origin to the middle of cube

                //camera looking at origin 
                float3 ro = i.ro;

                float3 rd = normalize(i.hitPos-ro);
                float d = RayMarch(ro, rd);

                // sample the texture
                fixed4 col = 0;

                // if distance is smaller than max distance, you hit your surface
                if (d < MAX_DIST) {  
                    float3 p = ro + rd * d; // calculate point where surface was hit by ray 
                    float3 n = GetNormal(p); // normal vector
                    
                    col.rgb = n; // debug: test to see if n is working
                    
                }
               
                // debugging RayMarch function) 
               // col.rgb = RayMarch(ro, rd); 

                return col;
            }
            ENDCG
        }
    }
}
