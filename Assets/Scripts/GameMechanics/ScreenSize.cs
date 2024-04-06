using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSize : MonoBehaviour
{
    Camera cam;
    void Awake()
    {
        // Get the camera reference (assuming the camera is orthographic)
        cam = Camera.main;

        if (cam != null)
        {
            CheckScreenSize();
            
            // Print the results
            Debug.Log("Screen Size in Pixels: " + screenPixelWidth + " x " + screenPixelHeight);
            Debug.Log("Screen Size in Unity Units: " + screenUnitWidth + " x " + screenUnitHeight);
        }
        else
        {
            Debug.LogError("Main camera not found.");
        }
    }

    private void Update()
    {
        CheckScreenSize();
    }

    //get the size of the screen
    void CheckScreenSize()
    {
        // Get the screen size in pixels
        float screenHeightPixels = Screen.height;
        float screenWidthPixels = Screen.width;
        screenPixelHeight = screenHeightPixels;
        screenPixelWidth = screenWidthPixels;

        // Convert screen size to Unity units using the camera's orthographic size
        float screenHeightUnits = cam.orthographicSize * 2f;
        float screenWidthUnits = screenHeightUnits * screenWidthPixels / screenHeightPixels;

        screenUnitHeight = screenHeightUnits;
        screenUnitWidth = screenWidthUnits;
    }

    public static float screenPixelWidth { get; private set; }
    public static float screenPixelHeight { get; private set; }
    public static float screenUnitWidth { get; private set; }
    public static float screenUnitHeight { get; private set; }

}
