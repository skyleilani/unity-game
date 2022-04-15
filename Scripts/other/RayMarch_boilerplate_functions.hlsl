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

// returns Normal Vector based on slope
vec3 GetNormal(vec3 p) { 

    // distance at point p 
    float d = GetDist(p); 
    
    // evaluate distance to the surface at points around p 
    vec2 e = vec2( .01, 0); 
    
    vec3 n = d - vec3(
    // the distances very close to point p
        GetDist(p-e.xyy), 
        GetDist(p-e.yxy), 
        GetDist(p-e.yyx)); 
        
    return normalize(n);
}

float GetLight(vec3 p) { 

    vec3 lightPos = vec3(0, 5, 6);
    
    // make light move overhead 
    lightPos.xz += vec2(sin(iTime), cos(iTime))*2.; 
    
    // light vector (unit vector) 
    vec3 l = normalize(lightPos-p);
    
    // normal vector (unit vector) 
    vec3 n = GetNormal(p);
    
    // difuse  lighting by getting dot product of n and l 
    float dif = clamp(dot(n, l), 1., 1.); 
    float d = RayMarch(p, l);
    
    // finds shadow on if statement 
    // in shadow it's only 10% as bright as outside of the shadow. 
    if(d<length(lightPos-p)) dif *= .1; 
    
    
    return dif;
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
    
    
    // Output to screen
    float d = RayMarch(ro, rd);
    
    vec3 p = ro + rd * d;
    
    //difuse lighting 
    float dif = GetLight(p); 
    // use dif to shade 
    col = vec3(dif); 
    
    // check GetNormal function, should turn sphere red bc it turns the x - 1 and y z to 0 
    // col = GetNormal(p); 
    
    
    
    fragColor = vec4(col,1.0);
}
