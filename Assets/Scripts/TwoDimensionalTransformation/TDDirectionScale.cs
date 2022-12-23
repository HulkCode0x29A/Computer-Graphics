using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDDirectionScale : MonoBehaviour
{
    public Vector4 P1 = new Vector4(0, 0, 0, 1);

    public Vector4 P2 = new Vector4(0, 0, 0, 1);

    public Vector4 P3 = new Vector4(0, 0, 0, 1);

    public Vector4 P4 = new Vector4(0, 0, 0, 1);

    public Vector2 Direction;

    public Vector2 Scale;

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        GizmosExtension.DrawQuad(P1, P2, P3,P4);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(Vector2.zero, Direction);

        Matrix4x4 scaleMatrix = MatrixUtil.GetTDDirectionScaleMatrix(Direction, Scale);

        Vector2 t1 = scaleMatrix * P1;
        Vector2 t2 = scaleMatrix * P2;
        Vector2 t3 = scaleMatrix * P3;
        Vector2 t4 = scaleMatrix * P4;

        Gizmos.color = Color.red;
        GizmosExtension.DrawQuad(t1, t2, t3, t4);
    }
}
