using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDRigidbodyTransformation : MonoBehaviour
{
   
    public Vector2 AxisX = new Vector2(1,0);

    public float AxisXRotate = 0f;

    public Vector2 P1 = new Vector2(-1, 0);

    public Vector2 P2 = new Vector2(1, 0);

    public Vector2 P3 = new Vector2(0, 1);

    public Vector2 Translation = Vector2.zero;

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

    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Matrix4x4 xRotate = GetRotateMatrix(AxisXRotate);
        Vector3 newX = xRotate.MultiplyPoint(AxisX);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero, newX);

        Matrix4x4 yRotate = GetRotateMatrix(AxisXRotate +90);
        Vector3 newY = yRotate.MultiplyPoint(AxisX);
        Gizmos.color = Color.yellow;
        //newX and newY has length 1 and orthogonal each other
        Gizmos.DrawLine(Vector3.zero, newY);

        //draw original triangle
        //Gizmos.color = Color.green;
        //GizmosExtension.DrawWireTriangle(P1,P2,P3);

        Matrix4x4 rigidbodyMatrix = Matrix4x4.identity;
        Matrix4x4 rotateMatrix = ConstructRotateMatrix(newX, newY);
        Matrix4x4 transMatrix = GetTranslationMatrix(Translation);
        rigidbodyMatrix = transMatrix * rotateMatrix * rigidbodyMatrix;
        Vector2 t1 = rigidbodyMatrix.MultiplyPoint(P1);
        Vector2 t2 = rigidbodyMatrix.MultiplyPoint(P2);
        Vector2 t3 = rigidbodyMatrix.MultiplyPoint(P3);
        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(t1,t2,t3);

    }

    Matrix4x4 ConstructRotateMatrix(Vector2 xAxis, Vector2 yAxis)
    {
        //we need column-major matrix or you will get the opposite direction of rotation
        //Imagine rotating the xAxis yAxis back to the standard basis((1,0),(0,1))
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = xAxis.x;
        matrix[1, 0] = xAxis.y;
        matrix[0, 1] = yAxis.x;
        matrix[1, 1] = yAxis.y;

        return matrix;
    }
}
