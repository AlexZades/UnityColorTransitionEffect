# Unity Color Transition Effect
![example](https://user-images.githubusercontent.com/22029050/212966632-33c9fa69-45f9-4bd0-8729-53c35fb88398.gif)

This small script changes the color of an object's material in a looping manner. I've seen several youtube tutorials that don't implement this feature well so I wanted to create a script that's more performant and easy to drop into a project. You can use as many colors as you want for the effect and can stop and start the effect as needed. 

**Please note that this will affect the material asset.** If you want to have your asset return to its original color after the effect stops please cache the result and set the material color to that after calling the "StopEffect" method.
## Setup
1. Clone the GitHub repo and copy the script into your project's assets folder.
2. Apply the script to an object in a scene
3. Assign the material you want to change to the "objectMaterial" field in the inspector.
![image](https://user-images.githubusercontent.com/22029050/212966584-ea59f0ac-813d-46c5-80d9-ca1d1e2a37e7.png)
4. Add the colors you want to use to the "Colors" array.
![image](https://user-images.githubusercontent.com/22029050/212967016-a1ca7a02-49b6-4f3e-b007-cb0b546c13e2.png)
5. Set the "Color Property Name" field to match the color property of the material's shader. The name will depend on the material's shader. You can find this by clicking edit on the material's component. 
![image](https://user-images.githubusercontent.com/22029050/212967639-3f900eef-9e8f-4eba-ab94-6f35662548d6.png)
Then checking the shader properties for the name of the color property.
![image](https://user-images.githubusercontent.com/22029050/212967810-489e8afb-bf7a-4c14-acef-9f475674b9bf.png)
6. Set the "Auto Start" property to true in the inspector. You're done!

## Starting the effect programmatically
To start the effect from another script set the "Auto Start" property to false in the inspector. You can call `StartEffect` to start the effect. The effect runs in a coroutine which can be stopped using `StopEffect`. The currently active coroutine is stored in the 'colorRoutine' field. 
