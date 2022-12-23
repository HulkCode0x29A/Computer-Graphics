using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateParallelAxis : MonoBehaviour
{
    Matrix4x4 composeMatrix = Matrix4x4.identity;

    public Vector3 P1 = new Vector3(-1, 1, 0);

    public Vector3 P2 = new Vector3(1, 1, 0);

    public Vector3 P3 = new Vector3(0, 2, 0);

    public float Angle = 30f;

    //Because  rotate axis  parallel to coordinate axis
    //We're assuming that this is parallel to the X-axis
    public float AxisMove = -2;

    

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
        Matrix4x4 moveMatrix = MatrixUtil.GetTranslationMatrix(new Vector3(0,-AxisMove,0));
        //rotate triangle
        Matrix4x4 rotateMatrix = MatrixUtil.GetRotateXMatrix(Angle);
        //move back to it's original
        Matrix4x4 backMatrix = MatrixUtil.GetTranslationMatrix(new Vector3(0,AxisMove,0)) ;

        composeMatrix = backMatrix * rotateMatrix * moveMatrix;

        Vector3 t1 = composeMatrix.MultiplyPoint(P1);
        Vector3 t2 = composeMatrix.MultiplyPoint(P2);
        Vector3 t3 = composeMatrix.MultiplyPoint(P3);
        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1, t2, t3);
    }
}
