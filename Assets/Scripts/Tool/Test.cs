using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private PixelScreen screen;

    private void Start()
    {
        screen = this.GetComponent<PixelScreen>();
        screen.SetPixel(20, 20, Color.green);
    }

    private void OnDrawGizmos()
    {
        
    }
}
