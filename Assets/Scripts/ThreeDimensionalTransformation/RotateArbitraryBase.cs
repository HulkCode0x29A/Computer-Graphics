using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArbitraryBase : MonoBehaviour
{
    Matrix4x4 composeMatrix = Matrix4x4.identity;

    public Vector3 P1 = new Vector3(-1, 1, 0);

    public Vector3 P2 = new Vector3(1, 1, 0);

    public Vector3 P3 = new Vector3(0, 2, 0);

    public float Angle = 30f;

    public Vector3 AxisUz = new Vector3(1, 1, 0);

    Matrix4x4 RotateZ(float theta)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        float radians = theta * Mathf.Deg2Rad;
        matrix[0, 0] = Mathf.Cos(radians);
        matrix[0, 1] = -Mathf.Sin(radians);
        matrix[1, 0] = Mathf.Sin(radians);
        matrix[1, 1] = Mathf.Cos(radians);
        return matrix;
    }

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(Vector3.zero, AxisUz);

        Gizmos.color = Color.green;
        GizmosExtension.DrawWireTriangle(P1,P2,P3);

       //Gizmos.DrawSphere(P1,0.1f);

        Vector3 Uz = Vector3.Normalize(AxisUz);
        Vector3 Ux = new Vector3(1,0,0);
        Vector3 Uy = Vector3.Normalize(Vector3.Cross(Uz, Ux));
        Ux = Vector3.Normalize(Vector3.Cross(Uy, Uz));
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(Vector3.zero, Ux);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(Vector3.zero, Uy);

        Matrix4x4 baseMatrix = Matrix4x4.identity;
        composeMatrix[0, 0] = Ux.x;
        composeMatrix[0, 1] = Ux.y;
        composeMatrix[0, 2] = Ux.z;
        composeMatrix[1, 0] = Uy.x;
        composeMatrix[1, 1] = Uy.y;
        composeMatrix[1, 2] = Uy.z;
        composeMatrix[2, 0] = Uz.x;
        composeMatrix[2, 1] = Uz.y;
        composeMatrix[2, 2] = Uz.z;

        Matrix4x4 rotateZMatrix = RotateZ(Angle);

        Matrix4x4 inverseMatrix = baseMatrix.inverse;

        composeMatrix = inverseMatrix*  rotateZMatrix * baseMatrix * composeMatrix;


        //Vector3 t1 = composeMatrix.MultiplyPoint(P1);
        //Gizmos.DrawSphere(t1, 0.1f);

        Vector3 t1 = composeMatrix.MultiplyPoint(P1);
        Vector3 t2 = composeMatrix.MultiplyPoint(P2);
        Vector3 t3 = composeMatrix.MultiplyPoint(P3);
        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1, t2, t3);


    }
}
