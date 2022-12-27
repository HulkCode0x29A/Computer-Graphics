using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDInverseTransformation : MonoBehaviour
{
    public Vector2 P1 = new Vector2(-1, 0);

    public Vector2 P2 = new Vector2(1, 0);

    public Vector2 P3 = new Vector2(0, 1);

    public Vector2 Translation = Vector2.zero;

    public bool DrawConversion;

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
        GizmosExtension.DrawWireTriangle(P1,P2,P3);

        Matrix4x4 transMatrix = GetTranslationMatrix(Translation);
        Vector2 t1 = transMatrix.MultiplyPoint(P1);
        Vector2 t2 = transMatrix.MultiplyPoint(P2);
        Vector2 t3= transMatrix.MultiplyPoint(P3);

        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1,t2,t3);

        Matrix4x4 inverseTrans = transMatrix.inverse;
        t1 = inverseTrans.MultiplyPoint(t1);
        t2 = inverseTrans.MultiplyPoint(t2);
        t3 = inverseTrans.MultiplyPoint(t3);

        if(DrawConversion)
        {
            Gizmos.color = Color.yellow;
            GizmosExtension.DrawWireTriangle(t1, t2, t3);
        }
       
    }
}
