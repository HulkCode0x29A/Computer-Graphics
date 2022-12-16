using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDComposeRotate : MonoBehaviour
{

    Matrix4x4 composeMatrix = Matrix4x4.identity;

    public Vector4 P1 = new Vector4(0, 0, 0, 1);

    public Vector4 P2 = new Vector4(0, 0, 0, 1);

    public Vector4 P3 = new Vector4(0, 0, 0, 1);

    public float Angle1;

    public float Angle2;

    Matrix4x4 GetRotateMatrix(float theta)
    {
        theta = theta * Mathf.Deg2Rad;
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = Mathf.Cos(theta);
        matrix[0, 1] = -Mathf.Sin(theta);
        matrix[1, 0] = Mathf.Sin(theta);
        matrix[1, 1] = Mathf.Cos(theta);

        return matrix;
    }

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        GizmosExtension.DrawWireTriangle(P1, P2, P3);

        Matrix4x4 rotateMatrix1 = GetRotateMatrix(Angle1);
        Matrix4x4 rotateMatrix2 = GetRotateMatrix(Angle2);

        composeMatrix = rotateMatrix2 * rotateMatrix1;

        Vector2 t1 = composeMatrix * P1;
        Vector2 t2 = composeMatrix * P2;
        Vector2 t3 = composeMatrix * P3;

        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1, t2, t3);

    }
}
