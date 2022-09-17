using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDRotateMatrixByPoint : MonoBehaviour
{
    Matrix4x4 composeMatrix = Matrix4x4.identity;
    public Vector2 P1 = new Vector2(-1, 0);

    public Vector2 P2 = new Vector2(1, 0);

    public Vector2 P3 = new Vector2(0, 1);

    public Vector2 ScaleValue = Vector2.zero;

    public float Angle;

    public Vector2 RotatePoint = Vector2.zero;

    Matrix4x4 GetTranslationMatrix(Vector2 trans)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 3] = trans.x;
        matrix[1, 3] = trans.y;
        return matrix;
    }

    public Matrix4x4 GetRotateMatrix(float theta)
    {
        theta = theta * Mathf.Deg2Rad;
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = Mathf.Cos(theta);
        matrix[0, 1] = -Mathf.Sin(theta);
        matrix[1, 0] = Mathf.Sin(theta);
        matrix[1, 1] = Mathf.Cos(theta);

        return matrix;
    }
    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);
        Gizmos.color = Color.green;
        GizmosExtension.DrawWireTriangle(P1,P2,P3);

        composeMatrix = Matrix4x4.identity;
        //first transformation object to origin attention here is -RotatePoint
        Matrix4x4 transMatrix  =GetTranslationMatrix(-RotatePoint);
        //rotate object
        Matrix4x4 rotateMatrix = GetRotateMatrix(Angle);
        //tranformation object to it's start position 
        Matrix4x4 backMatrix = GetTranslationMatrix(RotatePoint);
        //read it from right to left
        composeMatrix = backMatrix * rotateMatrix * transMatrix * composeMatrix;

        Vector2 t1 = composeMatrix.MultiplyPoint(P1);
        Vector2 t2 = composeMatrix.MultiplyPoint(P2);
        Vector2 t3 = composeMatrix.MultiplyPoint(P3);

        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1, t2, t3);

    }
}
