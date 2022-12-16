using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDPointRotate : MonoBehaviour
{
    
    public Vector4 P1 = new Vector4(0, 0, 0, 1);

    public Vector4 P2 = new Vector4(0, 0, 0, 1);

    public Vector4 P3 = new Vector4(0, 0, 0, 1);

    public float Angle;

    public Vector2 RotatePoint;

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        GizmosExtension.DrawWireTriangle(P1, P2, P3);

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(RotatePoint, 0.1f);

        Matrix4x4 rotateMatrix = MatrixUtil.GetTDPointRotateMatrix(RotatePoint, Angle);

        Vector2 t1 = rotateMatrix * P1;
        Vector2 t2 = rotateMatrix * P2;
        Vector2 t3 = rotateMatrix * P3;

        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1, t2, t3);
    }
}
