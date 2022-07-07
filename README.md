# fractals
Application algorithm:

A Windows Presentation Foundation (.NET 5) window application allows to:
### 1. Draw five types of fractals. Descriptions of fractals are presented below.
### 2. Provide the user with a choice of current fractals for drawing.
### 3. Set the number of recursion steps (its depth - the number of recursive calls). When the recursion depth is changed, the fractal should be automatically redrawn. Watch out for stack overflow.
### 4. Report incorrect data entry, conflicting or invalid data values and other abnormal situations in pop-up windows of the MessageBox type.
### 5. Automatically redraw the fractal when resizing the window. The window must be scalable. You can set the minimum and maximum window size. The maximum window size is the size of the screen, and the minimum window size is half of the screen size (both in length and width).
### 6. Allow the user to select two colors: startColor and endColor. The startColor is used to draw the elements of the first iteration, the endColor is needed to draw the elements of the last iteration. Colors for intermediate iterations should be calculated using a linear gradient.

![image](https://user-images.githubusercontent.com/95444064/177737012-a39df15a-a8dc-490b-b3bd-d67d9376f8b7.png)

### 7. Change the scale of the fractal for its detailed view. Magnification by two, three and five times.
### 8. Moving images, including those with an enlarged image.
### 9. Save the fractal as a picture.

In this work you will be presented with 5 types of fractals:

1. Windblown fractal tree.
2. Curve Koch.
3. Sierpinski carpet.
4. Sierpinski triangle.
5. Cantor set.

Now to the rules: About the depth of recursion: in order to work correctly, a restriction is put on it, for each fractal its own:
 - Pythagorean tree - 8 
 - Koch curve - 6
 - Sierpinski carpet - 4 
 - Sierpinski triangle - 7 
 - Cantor set - 7

Please observe it. With each change in depth, the fractal is redrawn. My program does not allow zooming in before drawing, so this button is initially disabled. The minimum value of all parameters is 0. The maximum: angles - 180, coefficient 75, distance between segments - 60. To save the image or reset the image movement you need to call the context menu by pressing the right mouse button on the canvas. The maximum zoom increase is 5. The default depth value is 0.

٩(｡•́‿•̀｡)۶
