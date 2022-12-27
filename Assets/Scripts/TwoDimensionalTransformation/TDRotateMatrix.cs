using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDRotateMatrix : MonoBehaviour
{
    public Vector2 P1;

    public Vector2 P2;

    public float Angle;

    public Matrix4x4 GetRotateMatrix(float angle)
    {
        //formula (1.7.1)
       float theta = angle * Mathf.Deg2Rad;
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
        GizmosExtension.DrawLineWithSphere(P1, P2, 0.1f);

        Matrix4x4 rotateMatrix = GetRotateMatrix(Angle);
        Vector2 t1 = rotateMatrix.MultiplyPoint(P1);
        Vector2 t2 = rotateMatrix.MultiplyPoint(P2);

        Gizmos.color = Color.red;
        GizmosExtension.DrawLineWithSphere(t1,t2,0.1f);
    }
}
