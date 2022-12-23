using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDPointScale : MonoBehaviour
{
    public Vector4 P1 = new Vector4(0, 0, 0, 1);

    public Vector4 P2 = new Vector4(0, 0, 0, 1);

    public Vector4 P3 = new Vector4(0, 0, 0, 1);


    public Vector2 FixedPoint;

    public Vector2 Scale;

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        GizmosExtension.DrawWireTriangle(P1, P2, P3);

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(FixedPoint, 0.1f);

        Matrix4x4 scaleMatrix = MatrixUtil.GetTDPointScaleMatrix(FixedPoint, Scale);

        Vector2 t1 = scaleMatrix * P1;
        Vector2 t2 = scaleMatrix * P2;
        Vector2 t3 = scaleMatrix * P3;

        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1, t2, t3);
    }
}
