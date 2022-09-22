using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDCoordinatesTransformation : MonoBehaviour
{
    public Vector3 LightPosition = new Vector3(1, 1, 0);

    public Vector3 P1;

    public Vector3 P2;

    public int LightRotate;

    public bool DrawStandardBasis;

    public bool DrawLightSpaceBasis;

    public bool DrawLightLine;

    public bool DrawWorldLine;

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

    public Matrix4x4 ConstructLightToWorldMatrix(Vector3 u, Vector3 v)
    {
        
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = u.x;
        matrix[0, 1] = u.y;
        matrix[1, 0] = v.x;
        matrix[1, 1] = v.y;
        return matrix;

    }

    private void OnDrawGizmos()
    {
        if (DrawStandardBasis)
            GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Vector3 right = Vector3.right;
        Matrix4x4 composeMatrix = Matrix4x4.identity;
        Matrix4x4 lightRotate = GetRotateMatrix(LightRotate);
        Matrix4x4 lightMove = GetTranslationMatrix(LightPosition);
        composeMatrix = lightMove *lightRotate* composeMatrix;
        Vector3 transRight = composeMatrix.MultiplyPoint(right);
        Vector3 up = Vector3.up;
        Vector3 transUp = composeMatrix.MultiplyPoint(up);

        if (DrawLightSpaceBasis)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(LightPosition, transRight);
            Gizmos.color = Color.black;
            Gizmos.DrawLine(LightPosition,  transUp);
        }

        if (DrawLightLine)
        {
            Gizmos.color = Color.blue;
            GizmosExtension.DrawLineWithSphere(P1, P2, 0.1f);
        }

        composeMatrix = Matrix4x4.identity;
        Matrix4x4 lightBasisMove = GetTranslationMatrix(-LightPosition);
        Vector3 originalLightUp = lightBasisMove.MultiplyPoint(transUp);
        Vector3 v = originalLightUp.normalized;
        Matrix4x4 rotateV = GetRotateMatrix(-90);
        Vector3 u = rotateV.MultiplyPoint(v);
        Matrix4x4 lightToWorld = ConstructLightToWorldMatrix(u,v);
        composeMatrix = lightToWorld * lightBasisMove * composeMatrix; 
        Vector3 t1 = composeMatrix.MultiplyPoint(P1); 
        Vector3 t2 = composeMatrix.MultiplyPoint(P2) ;

        if (DrawWorldLine)
        {
            Gizmos.color = Color.blue;
            GizmosExtension.DrawLineWithSphere(t1, t2, 0.1f);
        }
            
    }
}
