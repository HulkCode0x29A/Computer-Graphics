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



    void RotateX(float theta)
    {
        float radians = theta * Mathf.Deg2Rad;
        rotateMatrix[1, 1] = Mathf.Cos(radians);
        rotateMatrix[1, 2] = -Mathf.Sin(radians);
        rotateMatrix[2, 1] = Mathf.Sin(radians);
        rotateMatrix[2, 2] = Mathf.Cos(radians);
    }

    void RotateY(float theta)
    {
        float radians = theta * Mathf.Deg2Rad;
        rotateMatrix[0, 0] = Mathf.Cos(radians);
        rotateMatrix[0, 2] = Mathf.Sin(radians);
        rotateMatrix[2, 0] = -Mathf.Sin(radians);
        rotateMatrix[2, 2] = Mathf.Cos(radians);
    }
    //00 01 02 03
    //10 11 12 13
    //20 21 22 23
    //30 31 32 33

    void RotateZ(float theta)
    {
        float radians = theta * Mathf.Deg2Rad;
        rotateMatrix[0, 0] = Mathf.Cos(radians);
        rotateMatrix[0, 1] = -Mathf.Sin(radians);
        rotateMatrix[1, 0] = Mathf.Sin(radians);
        rotateMatrix[1, 1] = Mathf.Cos(radians);
    }
    void Start()
    {
        rotateMatrix = Matrix4x4.identity;
        RotateZ(Angle);
        
        Debug.Log(rotateMatrix);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        GizmosExtension.DrawWireTriangle(P1,P2,P3);
        rotateMatrix = Matrix4x4.identity;

        switch (Axis)
        {
            case RotateAxis.RotateX:
                RotateX(Angle);
                break;
            case RotateAxis.RotateY:
                RotateY(Angle);
                break;
            case RotateAxis.RotateZ:
                RotateZ(Angle);
                break;
        }
       
        Vector3 t1 = rotateMatrix.MultiplyPoint(P1);
        Vector3 t2 = rotateMatrix.MultiplyPoint(P2);
        Vector3 t3 = rotateMatrix.MultiplyPoint(P3);

        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1,t2,t3);

    }
}
