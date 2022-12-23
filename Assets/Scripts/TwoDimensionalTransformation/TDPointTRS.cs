using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDPointTRS : MonoBehaviour
{
    public Vector4 P1 = new Vector4(0, 0, 0, 1);

    public Vector4 P2 = new Vector4(0, 0, 0, 1);

    public Vector4 P3 = new Vector4(0, 0, 0, 1);

    public Vector4 P4 = new Vector4(0, 0, 0, 1);

    public Vector2 Translation;

    public float Angle;

    public Vector2 Scale;

    public Vector2 Point;



    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        GizmosExtension.DrawQuad(P1, P2, P3,P4);

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(Point, 0.1f);

        Matrix4x4 trsMatrix = MatrixUtil.GetTDPointTRSMatrix(Scale,Angle,Translation, Point);

        Vector2 t1 = trsMatrix * P1;
        Vector2 t2 = trsMatrix * P2;
        Vector2 t3 = trsMatrix * P3;
        Vector2 t4 = trsMatrix * P4;

        Gizmos.color = Color.red;
        GizmosExtension.DrawQuad(t1, t2, t3, t4);

    }
}
