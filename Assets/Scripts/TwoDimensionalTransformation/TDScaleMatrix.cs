using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDScaleMatrix : MonoBehaviour
{
    public Vector2 P1 = new Vector2(-1,0);

    public Vector2 P2 = new Vector2(1,0);

    public Vector2 P3 = new Vector2(0,1);

    public Vector2 ScaleValue = Vector2.zero;

    public Matrix4x4 GetScaleMatrix(Vector2 scale)
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
        GizmosExtension.DrawWireTriangle(P1,P2,P3);

        Matrix4x4 scaleMatrix = GetScaleMatrix(ScaleValue);
        Vector2 t1 = scaleMatrix.MultiplyPoint(P1);
        Vector2 t2 = scaleMatrix.MultiplyPoint(P2);
        Vector2 t3 = scaleMatrix.MultiplyPoint(P3);

        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1,t2,t3);

    }
}
