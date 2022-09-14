using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthogonalProjections : MonoBehaviour
{
    public PixelScreen Screen;

    Matrix4x4 composeMatrix = Matrix4x4.identity;

    Matrix4x4 viewportMatrix;

    public Vector3 P1 = new Vector3(0, 0, 0);

    public Vector3 P2 = new Vector3(0, 0, 0);

    public Vector3 LightPos = new Vector3(5, 5, 5);

    public Vector3 T1;

    public Vector3 T2;

    public Vector4 OrthoArg = new Vector4(-4, 4, -2, 2);

    public float zNear = 1f;

    public float zFar = 10;

    void Start()
    {
        viewportMatrix = ThreeDimensionalMatrix.GetViewPortMatrix(0, Screen.WidthCount - 1, 0, Screen.HeightCount - 1);
        Screen.SetClearColor(Color.blue);
    }

    void Update()
    {
       
        Vector3 startPos = viewportMatrix.MultiplyPoint(T1);
        Vector3 endPos = viewportMatrix.MultiplyPoint(T2);
        Color startColor = new Color(startPos.z, startPos.z, startPos.z);
        Color endColor = new Color(endPos.z, endPos.z, endPos.z);
        Screen.Clear();
        Screen.SetLine(startPos.x, startPos.y, endPos.x, endPos.y, startColor, endColor);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        GizmosExtension.DrawNormalizedCube();

        Gizmos.color = Color.white;
        GizmosExtension.DrawOrthoCube(OrthoArg.x, OrthoArg.y, OrthoArg.z, OrthoArg.w, zNear, zFar);

        Gizmos.color = Color.green;
        GizmosExtension.DrawLineWithSphere(P1, P2, 0.1f);

        composeMatrix = Matrix4x4.identity;

        Matrix4x4 orthoMatrix = ThreeDimensionalMatrix.GetOrthoMatrix(OrthoArg.x, OrthoArg.y, OrthoArg.z, OrthoArg.w, zNear, zFar);
        composeMatrix = orthoMatrix * composeMatrix;
        T1 = composeMatrix.MultiplyPoint(P1);
        T2 = composeMatrix.MultiplyPoint(P2);
        Gizmos.color = Color.red;
        GizmosExtension.DrawLineWithSphere(T1, T2, 0.1f);
    }
}
