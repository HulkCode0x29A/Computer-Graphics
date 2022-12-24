using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComposeTransformation : MonoBehaviour
{
    public Vector3 CubeCenter;

    public Vector3 CubeSize;

    public Vector3 Translation;

    public float RotateXAngle;

    public Vector3 Scale;
    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        Vector3[] cubePoints = GizmosExtension.GetCubePoints(CubeCenter,CubeSize);
        GizmosExtension.DrawWireCube(cubePoints);

        Matrix4x4 transMatrix = MatrixUtil.GetTranslationMatrix(Translation);
        Matrix4x4 rotateMatrix = MatrixUtil.GetRotateXMatrix(RotateXAngle);
        Matrix4x4 scaleMatrix = MatrixUtil.GetScaleMatrix(Scale.x, Scale.y, Scale.z);

        Matrix4x4 composeMatrix = scaleMatrix * rotateMatrix * transMatrix;

        for (int i = 0; i < cubePoints.Length; i++)
        {
            cubePoints[i] = composeMatrix.MultiplyPoint(cubePoints[i]);
        }

        Gizmos.color = Color.red;
        GizmosExtension.DrawWireCube(cubePoints);
        
    }
}
