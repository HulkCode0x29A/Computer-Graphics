using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteViewingPipeline : MonoBehaviour
{
    public PixelScreen Screen;

    Matrix4x4 composeMatrix = Matrix4x4.identity;

    public Vector3 CameraPosition = new Vector3();

    //because  perspective division we must use vector4 and w component is 1
    public Vector4 P1 = new Vector4(1, 1, 1, 1);

    public Vector4 P2 = new Vector4(1, 1, 1, 1);

    public Vector4 P3 = new Vector4(1, 1, 1, 1);

    //homogeneous space coordinates
    public Vector4 T1;

    public Vector4 T2;

    public Vector4 T3;

    //coordinates after perspective division
    public Vector3 RealT1;

    public Vector3 RealT2;

    public Vector3 RealT3;

    public Vector4 PerspectiveArg = new Vector4(-1, 1, -1, 1);

    public float zNear = 1f;

    public float zFar = 10;

    public float Fov = 60;

    public float Aspect = 1f;


    void Start()
    {

    }


    void Update()
    {
        Matrix4x4 viewportMatrix = ThreeDimensionalMatrix.GetViewPortMatrix(0, Screen.WidthCount - 1, 0, Screen.HeightCount - 1);
        Vector2 screenP1 =  viewportMatrix.MultiplyPoint(RealT1);
        Vector2 screenP2 = viewportMatrix.MultiplyPoint(RealT2);
        Vector2 screenP3 = viewportMatrix.MultiplyPoint(RealT3);
        Screen.Clear();
        Screen.SetTriangle(new Vector2[] { screenP1, screenP2, screenP3}, new Color[]{ Color.red, Color.green, Color.blue});
    }

    private void OnDrawGizmos()
    {
        //draw triangle
        GizmosExtension.DrawWireTriangle(P1, P2, P3);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(P1, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(P2, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(P3, 0.1f);

        Vector3 triangleCenter = new Vector3((P1.x + P2.x + P3.x) / 3, (P1.y + P2.y + P3.y) / 3, (P1.z + P2.z + P3.z) / 3);
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(triangleCenter, 0.1f);

        //draw camera axis
        Vector3 uz = (CameraPosition - triangleCenter).normalized;
        Vector3 uy = Vector3.up;
        Vector3 ux = Vector3.Cross(uy, uz).normalized;
        uy = Vector3.Cross(uz, ux).normalized;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(CameraPosition, CameraPosition + uz);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(CameraPosition, CameraPosition + ux);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(CameraPosition, CameraPosition + uy);

        composeMatrix = Matrix4x4.identity;
        Matrix4x4 cameraMoveMatrix = ThreeDimensionalMatrix.GetTranslationMatrix(CameraPosition);
        Matrix4x4 cameraRotateMatrix = Matrix4x4.identity;
        cameraRotateMatrix[0, 0] = ux.x;
        cameraRotateMatrix[0, 1] = ux.y;
        cameraRotateMatrix[0, 2] = ux.z;
        cameraRotateMatrix[1, 0] = uy.x;
        cameraRotateMatrix[1, 1] = uy.y;
        cameraRotateMatrix[1, 2] = uy.z;
        cameraRotateMatrix[2, 0] = uz.x;
        cameraRotateMatrix[2, 1] = uz.y;
        cameraRotateMatrix[2, 2] = uz.z;
        //rotate from world to camera so need cameraRotateMatrix.inverse
        composeMatrix = cameraMoveMatrix * cameraRotateMatrix.inverse * composeMatrix;

        FrustumData frustumData = GizmosExtension.GetFrustumData(PerspectiveArg.x, PerspectiveArg.y, PerspectiveArg.z, PerspectiveArg.w, -zNear, -zFar);
        frustumData.Transformation(composeMatrix);
        GizmosExtension.DrawFrustum(frustumData);

        //debug for understand how does the observer rotate from the origin to the camera

        //Gizmos.color = Color.blue;
        //Vector3 temp1 = Vector3.zero;
        //Vector3 temp2 = new Vector3(0,0, -10);

        //Gizmos.DrawLine(temp1,temp2);

        //Vector3 tempt1 = composeMatrix.MultiplyPoint(temp1);
        //Vector3 tempt2 = composeMatrix.MultiplyPoint(temp2);

        //Gizmos.DrawLine(tempt1, tempt2);

        //construct the observation matrix
        composeMatrix = Matrix4x4.identity;
        Matrix4x4 axisMoveMatrix = ThreeDimensionalMatrix.GetTranslationMatrix(-CameraPosition);
        Matrix4x4 axisRotateMatrix = cameraRotateMatrix;
        composeMatrix = axisRotateMatrix * axisMoveMatrix * composeMatrix;

        T1 = composeMatrix * P1;
        T2 = composeMatrix * P2;
        T3 = composeMatrix * P3;

        //draw viewing space triangle
        Gizmos.color = Color.white;
        GizmosExtension.DrawWireTriangle(T1, T2, T3);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(T1, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(T2, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(T3, 0.1f);

        triangleCenter = new Vector3((T1.x + T2.x + T3.x) / 3, (T1.y + T2.y + T3.y) / 3, (T1.z + T2.z + T3.z) / 3);
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(triangleCenter, 0.1f);

        Gizmos.color = Color.green;
        GizmosExtension.DrawNormalizedCube();

        Gizmos.color = Color.white;
        GizmosExtension.DrawFrustum(PerspectiveArg.x, PerspectiveArg.y, PerspectiveArg.z, PerspectiveArg.w, zNear, zFar);

        composeMatrix = Matrix4x4.identity;
        Matrix4x4 projectionMatrix = ThreeDimensionalMatrix.GetProjectionMatrix(PerspectiveArg.x, PerspectiveArg.y, PerspectiveArg.z, PerspectiveArg.w, zNear, zFar);
        composeMatrix = composeMatrix * projectionMatrix;

        T1 = composeMatrix * T1;
        T2 = composeMatrix * T2;
        T3 = composeMatrix * T3;
        //Do perspective division
        RealT1 = new Vector3(T1.x / T1.w, T1.y / T1.w, T1.z / T1.w);
        RealT2 = new Vector3(T2.x / T2.w, T2.y / T2.w, T2.z / T2.w);
        RealT3 = new Vector3(T3.x / T3.w, T3.y / T3.w, T3.z / T3.w);

        // draw normalized cube triangle
        Gizmos.color = Color.white;
        GizmosExtension.DrawWireTriangle(RealT1, RealT2, RealT3);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(RealT1, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(RealT2, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(RealT3, 0.1f);


    }


}
