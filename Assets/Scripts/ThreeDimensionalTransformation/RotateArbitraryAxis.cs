using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArbitraryAxis : MonoBehaviour
{
    Matrix4x4 composeMatrix = Matrix4x4.identity;

    public Vector3 P1 = new Vector3(-1, 1, 0);

    public Vector3 P2 = new Vector3(1, 1, 0);

    public Vector3 P3 = new Vector3(0, 2, 0);

    public float Angle = 30f;

    public Vector3 AxisP1 = new Vector3(1, 1, 0);

    public Vector3 AxisP2 = new Vector3(2, 3, 0);

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        GizmosExtension.DrawWireTriangle(P1, P2, P3);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(AxisP1, AxisP2);

        //get rotate normalize direction
        Vector3 u = Vector3.Normalize(AxisP2 - AxisP1);
        //move AxisP1 to (0,0)
        composeMatrix = Matrix4x4.identity;
        Matrix4x4 transMatrix = MatrixUtil.GetTranslationMatrix(new Vector3(-AxisP1.x, -AxisP1.y, -AxisP1.z));//formula (4.3.14)
        composeMatrix = transMatrix * composeMatrix;

        //debug see normalize direction u
        //Gizmos.color = Color.black;
        //Gizmos.DrawLine(Vector3.zero, u);

        //u projection on to yz
        Vector3 u1 = new Vector3(0, u.y, u.z);

        //debug see u projection  on to yz plane
        //Gizmos.color = Color.black;
        //Gizmos.DrawLine(Vector3.zero, u1);

        //a = u.x   b = u1.y     c = u1.z
        float d = u1.magnitude;
        float cOverd = u1.z / d;//cosA value formula (4.3.16)
        float bOverd = u1.y / d;//sinA value formula (4.3.20)

        Matrix4x4 xRotateMatrix = Matrix4x4.identity; // formula (4.3.21)
        xRotateMatrix[1, 1] = cOverd;
        xRotateMatrix[1, 2] = -bOverd;
        xRotateMatrix[2, 1] = bOverd;
        xRotateMatrix[2, 2] = cOverd;

        //debug see line rotate on xz plane
        //Gizmos.color = Color.black;
        //Vector3 u2 = new Vector3(u.x, 0, d);
        //Gizmos.DrawLine(Vector3.zero, u2);

        Matrix4x4 yRotateMatrix = Matrix4x4.identity;// formula (4.3.28)
        yRotateMatrix[0, 0] = d;//cosB value
        yRotateMatrix[0, 2] = -u.x;//sinB value
        yRotateMatrix[2, 0] = u.x;//-sinB value
        yRotateMatrix[2, 2] = d;//cosB value

        //now rotate axis toward z axis we only need roateZ Matrix
        Matrix4x4 zRotateMatrix = MatrixUtil.GetRotateZMatrix(Angle);

        Matrix4x4 inverseMatrix = transMatrix.inverse * xRotateMatrix.inverse * yRotateMatrix.inverse;

        composeMatrix = inverseMatrix * zRotateMatrix * yRotateMatrix * xRotateMatrix * composeMatrix; // formula (4.3.30)

        //debug see how axis change 
        //Vector3 t1 = composeMatrix.MultiplyPoint(AxisP1);
        //Vector3 t2 = composeMatrix.MultiplyPoint(AxisP2);
        //Gizmos.color = Color.black;
        //Gizmos.DrawLine(t1, t2);

        Vector3 p1 = composeMatrix.MultiplyPoint(P1);
        Vector3 p2 = composeMatrix.MultiplyPoint(P2);
        Vector3 p3 = composeMatrix.MultiplyPoint(P3);
        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(p1, p2, p3);
    }

}
