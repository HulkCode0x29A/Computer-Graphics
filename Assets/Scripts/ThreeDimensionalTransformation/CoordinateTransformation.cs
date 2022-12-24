using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateTransformation : MonoBehaviour
{
    public Vector3 Point;

    public Vector3 CameraPosition;

    public bool DrawWolrdCoordinate;

    public bool DrawCameraCoordinate;

    public bool DrawConversionPoint;
    private void OnDrawGizmos()
    {
        if (DrawWolrdCoordinate)
            GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Vector3 uz = (CameraPosition - Point).normalized;
        Vector3 uy = Vector3.up;
        Vector3 ux = Vector3.Cross(uy, uz);
        uy = Vector3.Cross(uz, ux);

        if (DrawCameraCoordinate)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(Point, 0.1f);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(CameraPosition, CameraPosition + uz);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(CameraPosition, CameraPosition + ux);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(CameraPosition, CameraPosition + uy);
        }


        Matrix4x4 moveMatrix = MatrixUtil.GetTranslationMatrix(-CameraPosition);
        Matrix4x4 rotateMatrix = MatrixUtil.GetLookAtMatrix(CameraPosition, Point, Vector3.up);

        Matrix4x4 composeMatrix = rotateMatrix * moveMatrix;

        if(DrawConversionPoint)
        {
            Vector3 newPoint = composeMatrix.MultiplyPoint(Point);
      
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(newPoint, 0.1f);
        }

    }
}
