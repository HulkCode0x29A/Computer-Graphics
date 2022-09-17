using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDScaleMatrixByPoint : MonoBehaviour
{
    Matrix4x4 composeMatrix = Matrix4x4.identity;
    public Vector2 P1 = new Vector2(-1, 0);

    public Vector2 P2 = new Vector2(1, 0);

    public Vector2 P3 = new Vector2(0, 1);

    public Vector2 ScaleValue = Vector2.zero;

    public Vector2 ScalePoint = Vector2.zero;

    Matrix4x4 GetTranslationMatrix(Vector2 trans)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 3] = trans.x;
        matrix[1, 3] = trans.y;
        return matrix;
    }

    public Matrix4x4 GetScaleMatrix(Vector2 scale)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = scale.x;
        matrix[1, 1] = scale.y;
        return matrix;
    }

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);
        Gizmos.color = Color.green;
        GizmosExtension.DrawWireTriangle(P1, P2, P3);

        composeMatrix = Matrix4x4.identity;
        Matrix4x4 transMatrix = GetTranslationMatrix(-ScalePoint);
        Matrix4x4 scaleMatrix = GetScaleMatrix(ScaleValue);
        Matrix4x4 backMatrix = GetTranslationMatrix(ScalePoint);
        composeMatrix = backMatrix * scaleMatrix * transMatrix * composeMatrix;

        Vector2 t1 = composeMatrix.MultiplyPoint(P1);
        Vector2 t2 = composeMatrix.MultiplyPoint(P2);
        Vector2 t3 = composeMatrix.MultiplyPoint(P3);
        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1, t2, t3);
    }
}
