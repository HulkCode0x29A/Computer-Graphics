using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDTranslationMatrix : MonoBehaviour
{
    public Vector2 P1;

    public Vector2 P2;

    public Vector2 Translation = Vector2.zero;

    Matrix4x4 GetTranslationMatrix(Vector2 trans)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 3] = trans.x;
        matrix[1, 3] = trans.y;
        return matrix;
    }

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        GizmosExtension.DrawLineWithSphere(P1, P2, 0.1f);

        Matrix4x4 transMatrix = GetTranslationMatrix(Translation);
        Vector2 t1 = transMatrix.MultiplyPoint(P1);
        Vector2 t2 = transMatrix.MultiplyPoint(P2);

        Gizmos.color = Color.red;
        GizmosExtension.DrawLineWithSphere(t1, t2, 0.1f);
    }
}
