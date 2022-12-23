using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDCoordinatesTransformation : MonoBehaviour
{
    public Vector4 CameraPosition = new Vector4(0, 0, 0, 1);

    public Vector4 ModelPosition = new Vector4(0, 0, 0, 1);

    public Vector3 P1;

    public Vector3 P2;

    public int LightRotate;

    public bool ConstructY;

    public bool DrawStandardBasis;

    public bool DrawCameraSpaceBasis;

    public bool DrawTransSpaceBasis;

    private void OnDrawGizmos()
    {
        if (ConstructY)
            ConstructFromY();
        else
            ConstructFromX();
    }

    private void ConstructFromY()
    {
        if (DrawStandardBasis)
            GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Vector4 v = (CameraPosition - ModelPosition).normalized;
        Matrix4x4 rotatev = MatrixUtil.GetRotateZMatrix(-90);
        Vector4 u = rotatev * v;
        if (DrawCameraSpaceBasis)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(CameraPosition, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(ModelPosition, 0.1f);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(CameraPosition, CameraPosition + v);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(CameraPosition, CameraPosition + u);
        }

        Matrix4x4 translationMatrix = MatrixUtil.GetTDTranslationMatrix(-CameraPosition);

        //formula (1.13.6)
        Matrix4x4 rotateAxisMatrix = Matrix4x4.identity;
        rotateAxisMatrix[0, 0] = u.x;
        rotateAxisMatrix[0, 1] = u.y;
        rotateAxisMatrix[1, 0] = v.x;
        rotateAxisMatrix[1, 1] = v.y;

        Matrix4x4 composeMatrix = rotateAxisMatrix * translationMatrix;
        Vector4 u1 = composeMatrix * u;
        Vector4 v1 = composeMatrix * v;
        Vector4 transCamera = composeMatrix * CameraPosition;
        Vector4 transModel = composeMatrix * ModelPosition;

        if (DrawTransSpaceBasis)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transCamera, v1);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transCamera, u1);
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(transCamera, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transModel, 0.1f);
        }

    }

    private void ConstructFromX()
    {
        if (DrawStandardBasis)
            GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Vector4 u = (CameraPosition - ModelPosition).normalized;
        Matrix4x4 rotateu = MatrixUtil.GetRotateZMatrix(90);
        Vector4 v = rotateu * u;

        if (DrawCameraSpaceBasis)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(CameraPosition, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(ModelPosition, 0.1f);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(CameraPosition, CameraPosition + v);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(CameraPosition, CameraPosition + u);
        }

        Matrix4x4 translationMatrix = MatrixUtil.GetTDTranslationMatrix(-CameraPosition);
        Matrix4x4 rotateAxisMatrix = MatrixUtil.GetTDLookAtMatrix(CameraPosition, ModelPosition, v);
        Matrix4x4 composeMatrix = rotateAxisMatrix * translationMatrix;

        Vector4 u1 = composeMatrix * u;
        Vector4 v1 = composeMatrix * v;
        Vector4 transCamera = composeMatrix * CameraPosition;
        Vector4 transModel = composeMatrix * ModelPosition;

        if (DrawTransSpaceBasis)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transCamera, v1);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transCamera, u1);
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(transCamera, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transModel, 0.1f);
        }
    }
}
