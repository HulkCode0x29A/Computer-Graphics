using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveProjections : MonoBehaviour
{
    public enum PerspectiveMethod
    {
        LRBT,//Left Right Bottom Top
        FA,//Fov and Aspect
    };
    public PixelScreen Screen;

    public PerspectiveMethod Method = PerspectiveMethod.LRBT;

    Matrix4x4 composeMatrix = Matrix4x4.identity;

    //because  perspective division we must use vector4 and w component is 1
    public Vector4 P1 = new Vector4(5, 5, -10, 1);

    public Vector4 P2 = new Vector4(-5, -5, -10, 1);

    //Homogeneous space coordinates
    public Vector4 T1;

    public Vector4 T2;

    //coordinates after perspective division
    public Vector3 RealT1;

    public Vector3 RealT2;

    public Vector4 PerspectiveArg = new Vector4(-1, 1, -1, 1);

    public float zNear = 1f;

    public float zFar = 10;

    public float Fov = 60;

    public float Aspect = 1f;
    void Start()
    {
        Screen.SetClearColor(Color.blue);
    }

    
    void Update()
    {
        Matrix4x4 viewportMatrix = MatrixUtil.GetViewPortMatrix(0, Screen.WidthCount - 1, 0, Screen.HeightCount - 1);
        Vector3 startPos = viewportMatrix.MultiplyPoint(RealT1);
        Vector3 endPos = viewportMatrix.MultiplyPoint(RealT2);
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
        if(Method == PerspectiveMethod.LRBT)
        {
            GizmosExtension.DrawFrustum(PerspectiveArg.x, PerspectiveArg.y, PerspectiveArg.z, PerspectiveArg.w, zNear, zFar);
        }
        else
        {
            GizmosExtension.DrawFrustum(Fov,Aspect,zNear, zFar);
        }
        

        Gizmos.color = Color.green;
        GizmosExtension.DrawLineWithSphere(P1, P2, 0.1f);

        composeMatrix = Matrix4x4.identity;
        Matrix4x4 projectionMatrix;

        if(Method == PerspectiveMethod.LRBT)
        {
            projectionMatrix = MatrixUtil.GetProjectionMatrix(PerspectiveArg.x, PerspectiveArg.y, PerspectiveArg.z, PerspectiveArg.w, zNear, zFar);
        }
        else
        {
            projectionMatrix = MatrixUtil.GetProjectionMatrix(Fov, Aspect, zNear, zFar);
        }
  
        composeMatrix = composeMatrix * projectionMatrix;

        T1 = composeMatrix * P1;
        T2 = composeMatrix * P2;
        //Do perspective division
        RealT1 = new Vector3(T1.x / T1.w, T1.y / T1.w, T1.z / T1.w);
        RealT2 = new Vector3(T2.x / T2.w, T2.y / T2.w, T2.z / T2.w);
        Gizmos.color = Color.red;
        GizmosExtension.DrawLineWithSphere(RealT1, RealT2, 0.1f);
    }
}
