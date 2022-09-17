using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimensionalMatrix : MonoBehaviour
{
    public static Matrix4x4 GetClipToNormalizeMatrix(float left, float right, float bottom, float top)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = 2 / (right - left);
        matrix[0, 3] = -(right + left) / (right - left);
        matrix[1, 1] = 2 / (top - bottom);
        matrix[1, 3] = -(top + bottom) / (top - bottom);
        return matrix;
    }

    public static Matrix4x4 GetNormalizeToViewportMatrix(float left, float right, float bottom, float top)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = (right - left) / 2;
        matrix[0, 3] = (right + left) / 2;
        matrix[1, 1] = (top - bottom) / 2;
        matrix[1, 3] = (top + bottom) / 2;
        return matrix;
    }
}
