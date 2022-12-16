using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDComposeScale : MonoBehaviour
{
    Matrix4x4 composeMatrix = Matrix4x4.identity;

    public Vector4 P1 = new Vector4(0, 0, 0, 1);

    public Vector4 P2 = new Vector4(0, 0, 0, 1);

    public Vector4 P3 = new Vector4(0, 0, 0, 1);

    public Vector2 Scale1 = new Vector2();

    public Vector2 Scale2 = new Vector2();
     Matrix4x4 GetScaleMatrix(Vector2 scale)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = scale.x;
        matrix[1, 1] = scale.y;
        return matrix;
    }

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        GizmosExtension.DrawWireTriangle(P1, P2, P3);

        //formula (1.10.8)
        Matrix4x4 scaleMatrix1 = GetScaleMatrix(Scale1);
        Matrix4x4 scaleMatrix2 = GetScaleMatrix(Scale2);

        composeMatrix = scaleMatrix2 * scaleMatrix1;

        Vector2 t1 = composeMatrix * P1;
        Vector2 t2 = composeMatrix * P2;
        Vector2 t3 = composeMatrix * P3;

        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1, t2, t3);
    }
}
