using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public enum ScaleType
    {
        Origin,
        Point,
    }
    Matrix4x4 composeMatrix = Matrix4x4.identity;

    public Vector3 P1 = new Vector3(-1, 1, 0);

    public Vector3 P2 = new Vector3(1, 1, 0);

    public Vector3 P3 = new Vector3(0, 2, 0);

    public float Sx = 1f;

    public float Sy = 1f;

    public float Sz = 1f;

    public Vector3 ScalePoint = new Vector3(-1,1,0);

    public ScaleType ScaleEnum = ScaleType.Origin;

    private Matrix4x4 OriginScale(float sx, float sy, float sz)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = sx;
        matrix[1, 1] = sy;
        matrix[2, 2] = sz;
        return matrix;
    }

    private Matrix4x4 PointScale(float sx, float sy, float sz, Vector3 point)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = sx;
        matrix[0, 3] = (1 - sx) * point.x;
        matrix[1, 1] = sy;
        matrix[1, 3] = (1 - sy) * point.y;
        matrix[2, 2] = sz;
        matrix[2, 3] = (1 - sz) * point.z;
        return matrix;
    }

    private void OnDrawGizmos()
    {
       

        GizmosExtension.DrawLHCoordinate(Vector3.zero);
        Gizmos.color = Color.green;
        GizmosExtension.DrawWireTriangle(P1, P2, P3);

        composeMatrix = Matrix4x4.identity;
        switch (ScaleEnum)
        {
            case ScaleType.Origin:
                composeMatrix = composeMatrix * OriginScale(Sx, Sy, Sz);
                break;
            case ScaleType.Point:
                composeMatrix = composeMatrix * PointScale(Sx, Sy, Sz, ScalePoint);
                break;
            
        }

        Vector3 t1 = composeMatrix.MultiplyPoint(P1);
        Vector3 t2 = composeMatrix.MultiplyPoint(P2);
        Vector3 t3 = composeMatrix.MultiplyPoint(P3);
        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1, t2, t3);
    }
}
