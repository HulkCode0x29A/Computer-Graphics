using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDPointReflectTransformation : MonoBehaviour
{
  public   enum TransType
    {
        RelfectX,
        ReflectY,
        ReflectXAndY,
    }

    public TransType Transformation = TransType.RelfectX;

    public Vector2 P1 = new Vector2(2, 1);

    public Vector2 P2 = new Vector2(1, 1);

    public Vector2 P3 = new Vector2(1, 2);

    public Matrix4x4 GetReflectXMatrix()
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[1, 1] = -1;
        return matrix;
    }

    public Matrix4x4 GetReflectYMatrix()
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = -1;
        return matrix;
    }
    public Matrix4x4 GetReflectXAndYMatrix()
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = -1;
        matrix[1, 1] = -1;
        return matrix;
    }
    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);
        Gizmos.color = Color.green;
        GizmosExtension.DrawWireTriangle(P1,P2,P3);

        Matrix4x4 matrix = Matrix4x4.identity;
        switch (Transformation)
        {
            case TransType.RelfectX:
                matrix = GetReflectXMatrix();
                break;
            case TransType.ReflectY:
                matrix = GetReflectYMatrix();
                break;
            case TransType.ReflectXAndY:
                matrix = GetReflectXAndYMatrix();
                break;
        }

        Vector2 t1 = matrix.MultiplyPoint(P1);
        Vector2 t2 = matrix.MultiplyPoint(P2);
        Vector2 t3 = matrix.MultiplyPoint(P3);
        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1, t2, t3);

    }
}
