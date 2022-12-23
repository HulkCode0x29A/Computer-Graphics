using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDConstructRotate : MonoBehaviour
{
    public Vector4 P1 = new Vector4(0, 0, 0, 1);

    public Vector4 P2 = new Vector4(0, 0, 0, 1);

    public Vector4 P3 = new Vector4(0, 0, 0, 1);

    public float Angle;

    public bool drawOriginalAxis;

    public bool drawRoateAxis;

    public bool drawRotateTriangle;
    private void OnDrawGizmos()
    {
        // to demonstrate that we rotate the triangle
        Matrix4x4 rotatezMatrix = MatrixUtil.GetRotateZMatrix(Angle);
        Vector2 t1 = rotatezMatrix * P1;
        Vector2 t2 = rotatezMatrix * P2;
        Vector2 t3 = rotatezMatrix * P3;

        Vector2 u, v;
        u = t1 + (t2 - t1) * 1.5f;
        v = (t3 + (t1 + (t2 - t1) * 0.5f)) * 1.5f;
        if (drawOriginalAxis)
        {
            //draw u axis
            Gizmos.color = Color.red;
            Gizmos.DrawLine(t1, u);

            //draw y axis
            Gizmos.color = Color.green;
            Gizmos.DrawLine(t1 + (t2 - t1) * 0.5f, v);

            Gizmos.color = Color.green;
            GizmosExtension.DrawWireTriangle(t1, t2, t3);
        }

        Matrix4x4 lookatMatrix = MatrixUtil.GetTDLookAtMatrix(u,Vector2.zero,v);

        if(drawRoateAxis)
        {
            //draw rotate u
            Gizmos.color = Color.red;
            Vector2 u1 = lookatMatrix * u;
            Gizmos.DrawLine(Vector2.zero, u1);

            //draw roate v
            Gizmos.color = Color.green;
            Vector2 v1 = lookatMatrix * v;
            Gizmos.DrawLine(Vector2.zero, v1);

        
        }

        if(drawRotateTriangle)
        {
            Vector2 r1 = lookatMatrix * t1;
            Vector2 r2 = lookatMatrix * t2;
            Vector2 r3 = lookatMatrix * t3;

            Gizmos.color = Color.cyan;
            GizmosExtension.DrawWireTriangle(r1, r2, r3);

        }

    }
}
