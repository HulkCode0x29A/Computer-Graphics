using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public enum RotateAxis
    {
        RotateX,
        RotateY,
        RotateZ
    }
    Matrix4x4 rotateMatrix = Matrix4x4.identity;

    public Vector3 P1 = new Vector3(-1, 0, 0);

    public Vector3 P2 = new Vector3(1, 0, 0);

    public Vector3 P3 = new Vector3(0, 1, 0);

    public RotateAxis Axis = RotateAxis.RotateX;

    public float Angle = 30f;


    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;

        GizmosExtension.DrawWireTriangle(P1,P2,P3);
        rotateMatrix = Matrix4x4.identity;

        switch (Axis)
        {
            case RotateAxis.RotateX:
                rotateMatrix = MatrixUtil.GetRotateXMatrix(Angle);
                break;
            case RotateAxis.RotateY:
                rotateMatrix =MatrixUtil.GetRotateYMatrix(Angle);
                break;
            case RotateAxis.RotateZ:
                rotateMatrix = MatrixUtil.GetRotateZMatrix(Angle);
                break;
        }
       
        Vector3 t1 = rotateMatrix.MultiplyPoint(P1);
        Vector3 t2 = rotateMatrix.MultiplyPoint(P2);
        Vector3 t3 = rotateMatrix.MultiplyPoint(P3);

        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1,t2,t3);

    }
}
