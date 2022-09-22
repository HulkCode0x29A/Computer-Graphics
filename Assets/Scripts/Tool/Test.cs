using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public PixelScreen Screen;

    public Vector3 P1;

    public Vector3 P2;

    public Vector3 P3;


    private void Start()
    {
        Screen.SetPixel(10,10,Color.green);
    }
}
