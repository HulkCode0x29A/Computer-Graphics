using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDimensionalViewingTest : MonoBehaviour
{
    public Camera camera;
    public PixelScreen Screen;

    Matrix4x4 composeMatrix = Matrix4x4.identity;

    public Vector4 P1 = new Vector4(0, 0, 0, 1);

    public Vector4 P2 = new Vector4(0, 0, 0, 1);

    public Vector3 LightPos = new Vector3(5, 5, 5);

    public Vector4 T1;

    public Vector4 T2;

    public Vector3 RealT1;

    public Vector3 RealT2;

    public Vector4 OrthoArg = new Vector4(-4, 4, -2, 2);

    public float zNear = 1f;

    public float zFar = 10;

    public bool drawFrustum;

    private void Start()
    {
      

    }

    private void Update()
    {

    }

    private void OnDrawGizmos()
    {
        if(drawFrustum)
        {
 

           GizmosExtension.DrawFrustum(OrthoArg.x, OrthoArg.y, OrthoArg.z, OrthoArg.w, zNear, zFar);
        }
        
        
  
    }


}
