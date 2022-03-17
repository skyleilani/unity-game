#define SURF_DIST.01
#define MAX_DIST 100.
#define MAX_STEPS 100 


float GetDist(vec3 p) 
{
    // sphere
    vec4 s = vec4(0, 1, 6 , 1);
    
    // .xyz = pos ; .w = radius 
    float sDist = length(p - s.xyz)-s.w; 
    
    // get distance from plane 
    float pDist = p.y; 
    
    // total distance
    float d = min(sDist, pDist); 
    return d; 
}

float RayMarch(vec3 ro, vec3 rd) 
{
    // keep track of distance marched from origin
    float dO=0.; 
    
    // marching loop 
    for(int i=0; i < MAX_STEPS; i++) {
    
        // current marching location
        vec3 p = ro + rd*dO;
        
        // distance to scene
        float dS = GetDist(p);
        
        dO += dS; 
        
        // prevent infininte marching
        if(dO>MAX_DIST || dS<SURF_DIST) break;
    }
    
    return dO;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    // normalize 
    vec2 uv = (fragCoord - 0.5*iResolution.xy)/iResolution.y;

    vec3 col = vec3(0);
    
    // camera model 
    
    // ro - ray origin (camera location) 
    vec3 ro = vec3(0, 1, 0); 
    
    // rd = ray direction
    // sets rd to a unit vector disregarding mag
    vec3 rd = normalize(vec3(uv.x,uv.y,1));
    
    // 
    // Output to screen
    fragColor = vec4(col,1.0);
}
