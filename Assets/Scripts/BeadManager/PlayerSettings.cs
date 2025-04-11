using UnityEngine;

[System.Serializable]
public class PlayerSettings
{
    public Vector3 redBeadPosition;
    public Vector3 yellowBeadPosition;
    public Vector3 greenBeadPosition;
    public Color skyboxColor; // Store color as a float array  
    public bool isFloorActive;
    public float blinkSpeed;

    // Example method to convert color to a float array for storage  
    public float[] ColorToFloatArray(Color color)
    {
        return new float[] { color.r, color.g, color.b, color.a };
    }

    public Color FloatArrayToColor(float[] colorArray)
    {
        return new Color(colorArray[0], colorArray[1], colorArray[2], colorArray[3]);
    }
}