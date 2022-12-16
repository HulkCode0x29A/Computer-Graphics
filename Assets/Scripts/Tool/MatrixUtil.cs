using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixUtil 
{
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
}
