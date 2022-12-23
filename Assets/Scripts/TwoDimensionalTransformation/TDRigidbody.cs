using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDRigidbody : MonoBehaviour
{
    public Vector4 P1 = new Vector4(0, 0, 0, 1);

    public Vector4 P2 = new Vector4(0, 0, 0, 1);

    public Vector4 P3 = new Vector4(0, 0, 0, 1);

    public Vector4 P4 = new Vector4(0, 0, 0, 1);

    public Vector2 Translation;

    public float Angle;

    public Vector2 Point;

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        GizmosExtension.DrawQuad(P1, P2, P3, P4);

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(Point, 0.1f);

        Matrix4x4 rigidbodyMatrix = MatrixUtil.GetTDRigidbodyMatrix(Angle,Translation,Point);

        Vector2 t1 = rigidbodyMatrix * P1;
        Vector2 t2 = rigidbodyMatrix * P2;
        Vector2 t3 = rigidbodyMatrix * P3;
        Vector2 t4 = rigidbodyMatrix * P4;

        Gizmos.color = Color.red;
        GizmosExtension.DrawQuad(t1, t2, t3, t4);

    }
}
