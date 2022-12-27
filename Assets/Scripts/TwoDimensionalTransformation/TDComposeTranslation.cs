using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDComposeTranslation : MonoBehaviour
{
    Matrix4x4 composeMatrix = Matrix4x4.identity;

    public Vector4 P1 = new Vector4(0, 0, 0, 1);

    public Vector4 P2 = new Vector4(0, 0, 0, 1);

    public Vector4 P3 = new Vector4(0, 0, 0, 1);

    public Vector2 Translation1 = Vector2.zero;

    public Vector2 Translation2 = Vector2.zero;

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
        GizmosExtension.DrawWireTriangle(P1, P2, P3);

        //formula (1.10.3)
        Matrix4x4 transMatrix1 = GetTranslationMatrix(Translation1);
        Matrix4x4 transMatrix2 = GetTranslationMatrix(Translation2);

        //read it from right to left 
        composeMatrix = transMatrix2 * transMatrix1;

        Vector2 t1 = composeMatrix * P1;
        Vector2 t2 = composeMatrix * P2;
        Vector2 t3 = composeMatrix * P3;

        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1, t2, t3);
    }
}
