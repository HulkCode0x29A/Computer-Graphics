using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarycentricCoordinates : MonoBehaviour
{
    public PixelScreen Screen;

    public Vector2[] Points = new Vector2[3];

    public Color[] Colors = new Color[3];

    private void Update()
    {
        Screen.Clear();
        Screen.SetTriangle(Points,Colors);
    }

}
