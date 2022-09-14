using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateParallelAxis : MonoBehaviour
{
    Matrix4x4 composeMatrix = Matrix4x4.identity;

    public Vector3 P1 = new Vector3(-1, 1, 0);

    public Vector3 P2 = new Vector3(1, 1, 0);

    public Vector3 P3 = new Vector3(0, 2, 0);

    public Vector3 Translate = new Vector3(5, 3, 2);

    public float Angle = 30f;

    //Because  rotate axis  parallel to coordinate axis
    //We're assuming that this is parallel to the X-axis
    public float AxisMove = -2;

    Matrix4x4 Translation( Vector3 move)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 3] = move.x;
        matrix[1, 3] = move.y;
        matrix[2, 3] = move.z;
        return matrix;
    }

    Matrix4x4 RotateX( float theta)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        float radians = theta * Mathf.Deg2Rad;
        matrix[1, 1] = Mathf.Cos(radians);
        matrix[1, 2] = -Mathf.Sin(radians);
        matrix[2, 1] = Mathf.Sin(radians);
        matrix[2, 2] = Mathf.Cos(radians);
        return matrix;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.cyan;
        //parallel to the x axis
        Gizmos.DrawLine(new Vector3(0, AxisMove, 0), new Vector3(1,AxisMove,0));

        Gizmos.color = Color.green;
        GizmosExtension.DrawWireTriangle(P1,P2,P3);
        composeMatrix = Matrix4x4.identity;
        //move rotate axis to xaxis
        composeMatrix= Translation(new Vector3(0,-AxisMove,0));
        //rotate triangle
        composeMatrix = RotateX(Angle) * composeMatrix;
        //move back to it's original
        composeMatrix = Translation(new Vector3(0,AxisMove,0)) * composeMatrix;

        Vector3 t1 = composeMatrix.MultiplyPoint(P1);
        Vector3 t2 = composeMatrix.MultiplyPoint(P2);
        Vector3 t3 = composeMatrix.MultiplyPoint(P3);
        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1, t2, t3);
    }
}
