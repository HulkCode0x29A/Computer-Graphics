using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public enum ScaleType
    {
        Origin,
        Point,
    }

    public Vector3 CubeCenter;

    public Vector3 CubeSize;

    public float Sx = 1f;

    public float Sy = 1f;

    public float Sz = 1f;

    public Vector3 ScalePoint = new Vector3(-1, 1, 0);

    public ScaleType ScaleEnum = ScaleType.Origin;



    private void OnDrawGizmos()
    {

        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Vector3[] cubePoints = GizmosExtension.GetCubePoints(CubeCenter, CubeSize);
        Gizmos.color = Color.green;
        GizmosExtension.DrawWireCube(cubePoints);

        Matrix4x4 composeMatrix = Matrix4x4.identity;
        switch (ScaleEnum)
        {
            case ScaleType.Origin:
                composeMatrix = composeMatrix * MatrixUtil.GetScaleMatrix(Sx, Sy, Sz);
                break;
            case ScaleType.Point:
                composeMatrix = composeMatrix * MatrixUtil.GetPointScaleMatrix(Sx, Sy, Sz, ScalePoint);
                break;
        }

        for (int i = 0; i < cubePoints.Length; i++)
        {
            cubePoints[i] = composeMatrix.MultiplyPoint(cubePoints[i]);
        }

        Gizmos.color = Color.red;
        GizmosExtension.DrawWireCube(cubePoints);
    }
}
