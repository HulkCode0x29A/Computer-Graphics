using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shear : MonoBehaviour
{
    public Vector3 CubeCenter;

    public Vector3 CubeSize;

    public float ShearZX;

    public float ShearZY;

    public float Zref;

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        Vector3[] cubePoints = GizmosExtension.GetCubePoints(CubeCenter,CubeSize);
        GizmosExtension.DrawWireCube(cubePoints);

        Matrix4x4 shearMatrix = MatrixUtil.GetShearByZref(ShearZX, ShearZY, Zref);

        for (int i = 0; i < cubePoints.Length; i++)
        {
            cubePoints[i] = shearMatrix.MultiplyPoint(cubePoints[i]);
        }

        Gizmos.color = Color.red;
        GizmosExtension.DrawWireCube(cubePoints);
    }
}
