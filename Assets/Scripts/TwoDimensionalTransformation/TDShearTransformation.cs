using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDShearTransformation : MonoBehaviour
{

    public enum TransType
    {
        ShearByOriginal,
        ShearByYref,
        ShearByXref,
    }

    public TransType Transformation = TransType.ShearByOriginal;

    public Vector2 P1 = new Vector2(0.5f, 0);

    public Vector2 P2 = new Vector2(0.5f, 0.5f);

    public Vector2 P3 = new Vector2(0, 0.5f);

    public Vector2 P4 = new Vector2(0, 0);

    public float ShearValue;

    public Vector2 ShearRef = Vector2.zero;

  

    


    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);
        Gizmos.color = Color.green;
        GizmosExtension.DrawQuad(P1, P2, P3,P4);

        Matrix4x4 matrix = Matrix4x4.identity;
        switch (Transformation)
        {
            case TransType.ShearByOriginal:
                matrix = MatrixUtil. GetShearByOriginalMatrix(ShearValue);
                break;
            case TransType.ShearByYref:
                matrix = MatrixUtil.GetShearByYref(ShearValue, ShearRef.y);
                break;
            case TransType.ShearByXref:
                matrix = MatrixUtil.GetShearByXref(ShearValue, ShearRef.x);
                break;
            default:
                break;
        }

        Vector2 t1 = matrix.MultiplyPoint(P1);
        Vector2 t2 = matrix.MultiplyPoint(P2);
        Vector2 t3 = matrix.MultiplyPoint(P3);
        Vector2 t4 = matrix.MultiplyPoint(P4);
        Gizmos.color = Color.red;
        GizmosExtension.DrawQuad(t1, t2, t3, t4);
    }
}
