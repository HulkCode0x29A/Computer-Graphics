using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateTransformation : MonoBehaviour
{
    Matrix4x4 composeMatrix = Matrix4x4.identity;

    public Vector3 P1 = new Vector3(0, 0, 0);

    public Vector3 LightPos = new Vector3(5,5,5);

  

    Matrix4x4 Translation(Vector3 move)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 3] = move.x;
        matrix[1, 3] = move.y;
        matrix[2, 3] = move.z;
        return matrix;
    }

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(P1, 0.1f);

        
        Vector3 uz = (LightPos - P1).normalized;
        Vector3 uy = Vector3.up;
        Vector3 ux = Vector3.Cross(uy, uz).normalized;
        uy = Vector3.Cross(uz, ux).normalized;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(LightPos ,LightPos + uz);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(LightPos, LightPos +ux);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(LightPos, LightPos + uy);

        composeMatrix = Matrix4x4.identity;

        Matrix4x4 transMatrix = Translation(new Vector3(-LightPos.x, -LightPos.y, -LightPos.z));
        Matrix4x4 basisMatrix = Matrix4x4.identity;
        basisMatrix[0, 0] = ux.x;
        basisMatrix[0, 1] = ux.y;
        basisMatrix[0, 2] = ux.z;
        basisMatrix[1, 0] = uy.x;
        basisMatrix[1, 1] = uy.y;
        basisMatrix[1, 2] = uy.z;
        basisMatrix[2, 0] = uz.x;
        basisMatrix[2, 1] = uz.y;
        basisMatrix[2, 2] = uz.z;
        composeMatrix = basisMatrix * transMatrix * composeMatrix;

        Vector3 t1 = composeMatrix.MultiplyPoint(P1); ;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(t1, 0.1f);
    }
}
