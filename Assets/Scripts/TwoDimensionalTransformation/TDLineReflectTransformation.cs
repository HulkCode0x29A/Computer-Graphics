using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDLineReflectTransformation : MonoBehaviour
{
    public enum TransType
    {
        RelfectYEqualX,
        RelfectYEqualMinusX,
    }

    public TransType Transformation = TransType.RelfectYEqualX;

    public Vector2 P1 = new Vector2(0.5f, 1);

    public Vector2 P2 = new Vector2(1.5f, 1);

    public Vector2 P3 = new Vector2(1.75f, 2);

    public Matrix4x4 GetReflectYEqualXMatrix()
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = 0;
        matrix[0, 1] = 1;
        matrix[1, 0] = 1;
        matrix[1, 1] = 0;
        return matrix;
    }

    public Matrix4x4 GetReflectYEqualMinusXMatrix()
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = 0;
        matrix[0, 1] = -1;
        matrix[1, 0] = -1;
        matrix[1, 1] = 0;
        return matrix;
    }

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);
        Gizmos.color = Color.green;
        GizmosExtension.DrawWireTriangle(P1, P2, P3);

        Matrix4x4 matrix = Matrix4x4.identity;
        switch (Transformation)
        {
            case TransType.RelfectYEqualX:
                matrix = GetReflectYEqualXMatrix();
                break;
            case TransType.RelfectYEqualMinusX:
                matrix = GetReflectYEqualMinusXMatrix();
                break;
            
                
        }

        Vector2 t1 = matrix.MultiplyPoint(P1);
        Vector2 t2 = matrix.MultiplyPoint(P2);
        Vector2 t3 = matrix.MultiplyPoint(P3);
        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1, t2, t3);
    }
}
