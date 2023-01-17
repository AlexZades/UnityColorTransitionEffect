using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class ColorTransitionEffect : MonoBehaviour
{
    [Tooltip("Material to apply effect to")]
    public Material objectMaterial;

    [Tooltip("Colors to tween between")]
    public UnityEngine.Color[] colors;

    [Tooltip("Speed of color transitions")]
    public float tweenSpeed = 1f;

    [Tooltip("Name of the shader's color property")]
    public string colorPropertyName = "_Color";

    [Tooltip("Start the effect automatically")]
    public bool autoStart = true;
    //The last color to tween from
    private UnityEngine.Color lastColor;

    //Index of next color in color array;
    private int colorIndex = 0;

    //Color change routine
    private Coroutine colorRoutine;

    void Start()
    {
        //Set the material if it's not set in the inspector.
        if(objectMaterial == null)
            objectMaterial = gameObject.GetComponent<Renderer>().material; //This will only get the first assigned material
        if(colors.Length < 2)
        {
            throw new ArgumentException($"Color Array contains less than two colors (Legth = {colors.Length})");
        }
        if(autoStart)
             StartEffect();
    }

    /// <summary>
    /// Starts the color transition effect
    /// </summary>
    /// <returns>Color effect coroutine</returns>
    public void StartEffect()
    {
        lastColor = colors[colorIndex];
        colorIndex++;
        
        colorRoutine = StartCoroutine(TweenColor(colors[colorIndex]));
    }

    /// <summary>
    /// Starts a new coroutine to change the color
    /// </summary>
    public void NextColor()
    {
        lastColor = colors[colorIndex];
        if (colorIndex < colors.Length-1)
            colorIndex++;
        else
            colorIndex = 0;

        colorRoutine = StartCoroutine(TweenColor(colors[colorIndex]));
    }

    /// <summary>
    /// Stops the color transition effect
    /// </summary>
    public void StopEffect()
    {
        StopCoroutine(colorRoutine);
    }

    /// <summary>
    /// Tweens the objects material from the last color to the next color
    /// </summary>
    /// <param name="targetColor">Color to tween to</param>
    /// <returns></returns>
    IEnumerator TweenColor(UnityEngine.Color targetColor)
    {
        //set up tween
        var startTime = Time.time;
        var t = (Time.time - startTime) * tweenSpeed;
        var lerpColor = UnityEngine.Color.Lerp(lastColor, targetColor, t);
        //Tween between colors
        while (lerpColor != targetColor)
        {
            t = (Time.time - startTime) * tweenSpeed;
            lerpColor = UnityEngine.Color.Lerp(lastColor, targetColor, t);
            objectMaterial.SetColor(colorPropertyName, lerpColor);
            yield return null;
        }
        //When done go to the next color
        NextColor();
    }
}
