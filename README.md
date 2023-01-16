# Fractal Explorer

Fractal Explorer based on Mandelbrot set. Rather than navigating using WASD, this is connected to my arduino circuit and controlled with buttons that are currently a work in progress. 

Base setup currently has two basic buttons. If possible, I'll use conductive paint in the future but seeing what I can do with what I have here instead. 

# About the Shader 
The traditional Mandelbrot set equation is z = z^2 + c, where z and c are complex numbers.
I modified the equation to z = rotate(z, 0, _Time.y) + c, where the value of z is  passed through a function "rotate" first. This function applies a rotation to the point, and then the resulting value of z is added to c. This modification causes the Mandelbrot set to rotate over time, as the rotation angle is controlled by the _Time.y variable.
