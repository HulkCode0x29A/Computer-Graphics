
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDimensionalMatrix
{
    public static Matrix4x4 GetViewPortMatrix(int left, int right, int bottom ,int top)
    {
        //in ppt matrix 9
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = (float)(right - left) / 2;
        matrix[0, 3] = (float)(right + left) / 2;
        matrix[1, 1] = (float)(top - bottom) / 2;
        matrix[1, 3] = (float)(top + bottom) / 2;
        matrix[2, 2] = 1.0f / 2.0f;
        matrix[2, 3] = 1.0f / 2.0f;
        return matrix;
    }

    public static Matrix4x4 GetProjectionMatrix(float fov, float aspect, float zNear, float zFar)
    {
        //in ppt matrix 16
        zNear = -zNear;
        zFar = -zFar;

        float radians = (fov / 2) * Mathf.Deg2Rad;
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = (1 / Mathf.Tan(radians)) / aspect;
        matrix[1, 1] = 1 / Mathf.Tan(radians);
        matrix[2, 2] = (zNear + zFar) / (zNear - zFar);
        matrix[2, 3] = -2 * zNear * zFar / (zNear - zFar);
        matrix[3, 2] = -1;
        matrix[3, 3] = 0;

        return matrix;
    }

    public static Matrix4x4 GetProjectionMatrix(float left, float right, float bottom, float top, float zNear, float zFar)
    {
        //in ppt matrx 15
        zNear = -zNear;
        zFar = -zFar;

        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = -2 * zNear / (right - left);
        matrix[0, 2] = (right + left) / (right - left);
        matrix[1, 1] = -2 * zNear / (top - bottom);
        matrix[1, 2] = (top + bottom) / (top - bottom);
        matrix[2, 2] = (zNear + zFar) / (zNear - zFar);
        matrix[2, 3] = -2 * zNear * zFar / (zNear - zFar);
        matrix[3, 2] = -1;
        matrix[3, 3] = 0;
        return matrix;
    }
    /// <summary>
    /// To be consistent with Unity and use the right hand coordinate system, 
    /// we enter the Z coordinate as positive and take the inverse inside the method
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="bottom"></param>
    /// <param name="top"></param>
    /// <param name="zNear"></param>
    /// <param name="zFar"></param>
    public static Matrix4x4 GetOrthoMatrix(float left, float right, float bottom, float top, float zNear, float zFar)
    {
        //in ppt matrix 1
        zNear = -zNear;
        zFar = -zFar;
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = 2 / (right - left);
        matrix[0, 3] = -(right + left) / (right - left);
        matrix[1, 1] = 2 / (top - bottom);
        matrix[1, 3] = -(top + bottom) / (top - bottom);
        matrix[2, 2] = (-2) / (zNear - zFar);
        matrix[2, 3] = (zNear + zFar) / (zNear - zFar);
        return matrix;
    }
    public static Matrix4x4 GetTranslationMatrix(Vector3 pos)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 3] = pos.x;
        matrix[1, 3] = pos.y;
        matrix[2, 3] = pos.z;
        return matrix;
    }

    public static Matrix4x4 GetLookAtMatrix(Vector3 eye, Vector3 center, Vector3 up)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        Vector3 zAxis = (eye - center).normalized;
        Vector3 yAxis = up;
        Vector3 xAxis = Vector3.Cross(yAxis, zAxis).normalized;
        yAxis = Vector3.Cross(zAxis, xAxis);

        matrix[0, 0] = xAxis.x;
        matrix[0, 1] = xAxis.y;
        matrix[0, 2] = xAxis.z;
        matrix[1, 0] = yAxis.x;
        matrix[1, 1] = yAxis.y;
        matrix[1, 2] = yAxis.z;
        matrix[2, 0] = zAxis.x;
        matrix[2, 1] = zAxis.y;
        matrix[2, 2] = zAxis.z;

        return matrix;
    }
}
