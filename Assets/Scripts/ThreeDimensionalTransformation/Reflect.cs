using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Reflect : MonoBehaviour
{
    public Vector3 CubeCenter;

    public Vector3 CubeSize;

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Vector3[] cubePoints = GizmosExtension.GetCubePoints(CubeCenter, CubeSize);
        Gizmos.color = Color.green;
        GizmosExtension.DrawWireCube(cubePoints);

        Matrix4x4 reflectMatrix = MatrixUtil.GetReflectZMatrix();

        for (int i = 0; i < cubePoints.Length; i++)
        {
            cubePoints[i] = reflectMatrix.MultiplyPoint(cubePoints[i]);
        }

        Gizmos.color = Color.red;
        GizmosExtension.DrawWireCube(cubePoints);
    }
}
