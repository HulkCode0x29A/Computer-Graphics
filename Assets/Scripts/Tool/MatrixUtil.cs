using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixUtil
{
    /// <summary>
    /// translation transformation
    /// </summary>
    /// <param name="trans"></param>
    /// <returns></returns>
    public static Matrix4x4 GetTDTranslationMatrix(Vector2 trans)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 3] = trans.x;
        matrix[1, 3] = trans.y;
        return matrix;
    }

    /// <summary>
    /// Rotate about z axis
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static Matrix4x4 GetRotateZMatrix(float angle)
    {
        //formula (4.1.2)
        float theta = angle * Mathf.Deg2Rad;
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = Mathf.Cos(theta);
        matrix[0, 1] = -Mathf.Sin(theta);
        matrix[1, 0] = Mathf.Sin(theta);
        matrix[1, 1] = Mathf.Cos(theta);
        return matrix;
    }

    /// <summary>
    /// Rotate about x axis
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static Matrix4x4 GetRotateXMatrix(float angle)
    {
        //formula (4.1.6)
        float theta = angle * Mathf.Deg2Rad;
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[1, 1] = Mathf.Cos(theta);
        matrix[1, 2] = -Mathf.Sin(theta);
        matrix[2, 1] = Mathf.Sin(theta);
        matrix[2, 2] = Mathf.Cos(theta);
        return matrix;
    }

    /// <summary>
    /// Rotate about y axis
    /// </summary>
    /// <returns></returns>
    public static Matrix4x4 GetRotateYMatrix(float angle)
    {
        //formula (4.1.8)
        float theta = angle * Mathf.Deg2Rad;
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = Mathf.Cos(theta);
        matrix[0, 2] = Mathf.Sin(theta);
        matrix[2, 0] = -Mathf.Sin(theta);
        matrix[2, 2] = Mathf.Cos(theta);

        return matrix;
    }
    /// <summary>
    /// Two dimensional reference point rotation
    /// </summary>
    /// <param name="point"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static Matrix4x4 GetTDPointRotateMatrix(Vector2 point, float angle)
    {
        float theta = angle * Mathf.Deg2Rad;

        //formula (1.10.10)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = Mathf.Cos(theta);
        matrix[0, 1] = -Mathf.Sin(theta);
        matrix[0, 3] = point.x * (1 - Mathf.Cos(theta)) + point.y * Mathf.Sin(theta);
        matrix[1, 0] = Mathf.Sin(theta);
        matrix[1, 1] = Mathf.Cos(theta);
        matrix[1, 3] = point.y * (1 - Mathf.Cos(theta)) - point.x * Mathf.Sin(theta);

        return matrix;
    }

    /// <summary>
    /// Two dimensional reference point scaling
    /// </summary>
    /// <param name="point"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static Matrix4x4 GetTDPointScaleMatrix(Vector2 point, Vector2 scale)
    {
        //formula (1.10.12)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = scale.x;
        matrix[0, 3] = point.x * (1 - scale.x);
        matrix[1, 1] = scale.y;
        matrix[1, 3] = point.y * (1 - scale.y);
        return matrix;
    }

    /// <summary>
    /// Two dimensional directional scaling
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static Matrix4x4 GetTDDirectionScaleMatrix(Vector2 direction, Vector2 scale)
    {
        //formula (1.10.14)
        float angle = Vector2.Angle(direction, Vector2.right);
        float theta = angle * Mathf.Deg2Rad;
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = scale.x * Mathf.Cos(theta) * Mathf.Cos(theta) + scale.y * Mathf.Sin(theta) * Mathf.Sin(theta);
        matrix[0, 1] = (scale.y - scale.x) * Mathf.Cos(theta) * Mathf.Sin(theta);
        matrix[1, 0] = (scale.y - scale.x) * Mathf.Cos(theta) * Mathf.Sin(theta);
        matrix[1, 1] = scale.x * Mathf.Sin(theta) * Mathf.Sin(theta) + scale.y * Mathf.Cos(theta) * Mathf.Cos(theta);

        return matrix;
    }

    /// <summary>
    /// Scale-rotation-translation of two-dimensional composite transformation
    /// </summary>
    /// <param name="scale"></param>
    /// <param name="angle"></param>
    /// <param name="trans"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public static Matrix4x4 GetTDPointTRSMatrix(Vector2 scale, float angle, Vector2 trans, Vector2 point)
    {
        //formula (1.10.16)
        float theta = angle * Mathf.Deg2Rad;

        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = scale.x * Mathf.Cos(theta);
        matrix[0, 1] = -scale.y * Mathf.Sin(theta);
        matrix[0, 3] = point.x * (1 - scale.x * Mathf.Cos(theta)) + scale.y * point.y * Mathf.Sin(theta) + trans.x;
        matrix[1, 0] = scale.x * Mathf.Sin(theta);
        matrix[1, 1] = scale.y * Mathf.Cos(theta);
        matrix[1, 3] = point.y * (1 - scale.y * Mathf.Cos(theta)) - scale.x * point.x * Mathf.Sin(theta) + trans.y;

        return matrix;
    }

    /// <summary>
    /// Two dimensional rigid body transformation
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="trans"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public static Matrix4x4 GetTDRigidbodyMatrix(float angle, Vector2 trans, Vector2 point)
    {
        //formula (1.10.22)
        float theta = angle * Mathf.Deg2Rad;

        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = Mathf.Cos(theta);
        matrix[0, 1] = -Mathf.Sin(theta);
        matrix[0, 3] = point.x * (1 - Mathf.Cos(theta)) + point.y * Mathf.Sin(theta) + trans.x;
        matrix[1, 0] = Mathf.Sin(theta);
        matrix[1, 1] = Mathf.Cos(theta);
        matrix[1, 3] = point.y * (1 - Mathf.Cos(theta)) - point.y * Mathf.Sin(theta) + trans.y;

        return matrix;
    }

    /// <summary>
    /// Two-dimensional lookat matrix
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="up"></param>
    /// <returns></returns>
    public static Matrix4x4 GetTDLookAtMatrix(Vector2 from, Vector2 to, Vector2 up)
    {
        Vector2 u = (from - to).normalized;
        Vector2 v = up.normalized;
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = u.x;
        matrix[0, 1] = u.y;
        matrix[1, 0] = v.x;
        matrix[1, 1] = v.y;

        return matrix;
    }

    /// <summary>
    /// Three-dimensional lookat matrix
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="up"></param>
    /// <returns></returns>
    public static Matrix4x4 GetLookAtMatrix(Vector3 from, Vector3 to, Vector3 up)
    {
        Vector3 uz = (from - to).normalized;
        Vector3 uy = up.normalized;
        Vector3 ux = Vector3.Cross(uy, uz);
        uy = Vector3.Cross(uz, ux);

        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = ux.x;
        matrix[0, 1] = ux.y;
        matrix[0, 2] = ux.z;
        matrix[1, 0] = uy.x;
        matrix[1, 1] = uy.y;
        matrix[1, 2] = uy.z;
        matrix[2, 0] = uz.x;
        matrix[2, 1] = uz.y;
        matrix[2, 2] = uz.z;
        return matrix;
    }

    /// <summary>
    /// Obtain the X-axis reflection matrix
    /// </summary>
    /// <returns></returns>
    public static Matrix4x4 GetReflectXMatrix()
    {
        //formula (1.11.1)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[1, 1] = -1;
        return matrix;
    }

    /// <summary>
    /// Obtain the Y-axis reflection matrix
    /// </summary>
    /// <returns></returns>
    public static Matrix4x4 GetReflectYMatrix()
    {
        //formula (1.11.2)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = -1;
        return matrix;
    }

    /// <summary>
    /// Obtain the Z-axis reflection matrix
    /// </summary>
    /// <returns></returns>
    public static Matrix4x4 GetReflectZMatrix()
    {
        //formula (4.6.1)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[2, 2] = -1;
        return matrix;
    }

    /// <summary>
    /// Obtain the X-axis and Y-axis reflection matrix
    /// </summary>
    /// <returns></returns>
    public static Matrix4x4 GetReflectXAndYMatrix()
    {
        //formula (1.11.3)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = -1;
        matrix[1, 1] = -1;
        return matrix;
    }

    /// <summary>
    ///  Reflected by the line y = x
    /// </summary>
    /// <returns></returns>
    public static Matrix4x4 GetReflectYEqualXMatrix()
    {
        //formula (1.11.4)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = 0;
        matrix[0, 1] = 1;
        matrix[1, 0] = 1;
        matrix[1, 1] = 0;
        return matrix;
    }

    /// <summary>
    /// Reflected by the line y =- x
    /// </summary>
    /// <returns></returns>
    public static Matrix4x4 GetReflectYEqualMinusXMatrix()
    {
        //formula (1.11.5)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = 0;
        matrix[0, 1] = -1;
        matrix[1, 0] = -1;
        matrix[1, 1] = 0;
        return matrix;
    }

    /// <summary>
    /// Shear at the origin
    /// </summary>
    /// <param name="shear"></param>
    /// <returns></returns>
    public static Matrix4x4 GetShearByOriginalMatrix(float shear)
    {
        //formula (1.12.1)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 1] = shear;
        return matrix;
    }

    /// <summary>
    /// Shear according to the line yref
    /// </summary>
    /// <param name="shear"></param>
    /// <param name="yref"></param>
    /// <returns></returns>
    public static Matrix4x4 GetShearByYref(float shear, float yref)
    {
        //formula (1.12.3)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 1] = shear;
        matrix[0, 3] = -shear * yref;
        return matrix;
    }

    /// <summary>
    /// Shear according to the line xref
    /// </summary>
    /// <param name="shear"></param>
    /// <param name="xref"></param>
    /// <returns></returns>
    public static Matrix4x4 GetShearByXref(float shear, float xref)
    {
        //formula (1.12.5)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[1, 0] = shear;
        matrix[1, 3] = -shear * xref;
        return matrix;
    }

    /// <summary>
    /// Three dimensional Z-axis shear
    /// </summary>
    /// <param name="shear"></param>
    /// <param name="zref"></param>
    /// <returns></returns>
    public static Matrix4x4 GetShearByZref(float shearzx, float shearzy ,float zref)
    {
        //formula (4.7.1)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 2] = shearzx;
        matrix[0, 3] = -shearzx * zref;
        matrix[1, 2] = shearzy;
        matrix[1, 3] = -shearzy * zref;

        return matrix;
    }

    /// <summary>
    /// Gets the matrix of the window to the normalized square
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="bottom"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    public static Matrix4x4 GetWindowToNormsquareMatrix(float left, float right, float bottom, float top)
    {
        //formula (3.0.9)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = 2 / (right - left);
        matrix[0, 3] = -(right + left) / (right - left);
        matrix[1, 1] = 2 / (top - bottom);
        matrix[1, 3] = -(top + bottom) / (top - bottom);
        return matrix;
    }

    /// <summary>
    /// Gets the matrix of normalized square to viewport
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="bottom"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    public static Matrix4x4 GetNormsquareToViewportMatrix(float left, float right, float bottom, float top)
    {
        //formula (3.0.10)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = (right - left) / 2;
        matrix[0, 3] = (right + left) / 2;
        matrix[1, 1] = (top - bottom) / 2;
        matrix[1, 3] = (top + bottom) / 2;
        return matrix;
    }

    /// <summary>
    /// Obtain translation matrix
    /// </summary>
    /// <param name="trans"></param>
    /// <returns></returns>
    public static Matrix4x4 GetTranslationMatrix(Vector3 trans)
    {
        //formula (4.0.2)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 3] = trans.x;
        matrix[1, 3] = trans.y;
        matrix[2, 3] = trans.z;

        return matrix;
    }

    /// <summary>
    /// Obtain scaling matrix
    /// </summary>
    /// <returns></returns>
    public static Matrix4x4 GetScaleMatrix(float sx, float sy, float sz)
    {
        //formula (4.4.1)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = sx;
        matrix[1, 1] = sy;
        matrix[2, 2] = sz;
        return matrix;
    }

    /// <summary>
    /// Obtain the scaling matrix for the point
    /// </summary>
    /// <returns></returns>
    public static Matrix4x4 GetPointScaleMatrix(float sx, float sy, float sz, Vector3 point)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = sx;
        matrix[0, 3] = (1 - sx) * point.x;
        matrix[1, 1] = sy;
        matrix[1, 3] = (1 - sy) * point.y;
        matrix[2, 2] = sz;
        matrix[2, 3] = (1 - sz) * point.z;
        return matrix;
    }

    /// <summary>
    /// Obtain viewport matrix
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="bottom"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    public static Matrix4x4 GetViewPortMatrix(int left, int right, int bottom, int top)
    {
        //formula (3.0.10)
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = (float)(right - left) / 2;
        matrix[0, 3] = (float)(right + left) / 2;
        matrix[1, 1] = (float)(top - bottom) / 2;
        matrix[1, 3] = (float)(top + bottom) / 2;
        matrix[2, 2] = 1.0f / 2.0f;
        matrix[2, 3] = 1.0f / 2.0f;
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
        //formula (5.0.6)
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


    /// <summary>
    /// The projection matrix is obtained from the field of ivew and aspect ratio
    /// </summary>
    /// <param name="fov"></param>
    /// <param name="aspect"></param>
    /// <param name="zNear"></param>
    /// <param name="zFar"></param>
    /// <returns></returns>
    public static Matrix4x4 GetProjectionMatrix(float fov, float aspect, float zNear, float zFar)
    {
      
        zNear = -zNear;
        zFar = -zFar;

        //formula (5.1.33)
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

    /// <summary>
    /// Get the projection matrix according to the clipping window
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="bottom"></param>
    /// <param name="top"></param>
    /// <param name="zNear"></param>
    /// <param name="zFar"></param>
    /// <returns></returns>
    public static Matrix4x4 GetProjectionMatrix(float left, float right, float bottom, float top, float zNear, float zFar)
    {
        
        zNear = -zNear;
        zFar = -zFar;

        //formula (5.1.30)
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

}
