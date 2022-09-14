using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherTransformation : MonoBehaviour
{
    public enum TransformationType
    {
        Reflect,
        Shear,
    }
    Matrix4x4 composeMatrix = Matrix4x4.identity;

    public Vector3 P1 = new Vector3(-1, 1, 0);

    public Vector3 P2 = new Vector3(1, 1, 0);

    public Vector3 P3 = new Vector3(0, 2, 0);

    public TransformationType Type = TransformationType.Reflect;

    public float ShearZX = 1f;

    public float ShearZY = 1f;

    public float ShearZref = 1f;


    private void OnDrawGizmos()
    {


        GizmosExtension.DrawLHCoordinate(Vector3.zero);


        composeMatrix = Matrix4x4.identity;
        switch (Type)
        {
            case TransformationType.Reflect:


                Gizmos.color = Color.green;
                GizmosExtension.DrawWireTriangle(P1, P2, P3);

                Matrix4x4 reflectMatrix = Matrix4x4.identity;
                reflectMatrix[2, 2] = -1;
                composeMatrix = reflectMatrix * composeMatrix;

                Vector3 t1 = composeMatrix.MultiplyPoint(P1);
                Vector3 t2 = composeMatrix.MultiplyPoint(P2);
                Vector3 t3 = composeMatrix.MultiplyPoint(P3);

                Gizmos.color = Color.red;
                GizmosExtension.DrawWireTriangle(t1, t2, t3);
                break;
            case TransformationType.Shear:

                Gizmos.color = Color.green;
                GizmosExtension.DrawWireCube(new Vector3(0.25f, 0.25f, 0.25f), new Vector3(0.5f, 0.5f, 0.5f));

                Matrix4x4 shearMatrix = Matrix4x4.identity;
                shearMatrix[0, 2] = ShearZX;
                shearMatrix[0, 3] = -ShearZX * ShearZref;
                shearMatrix[1, 2] = ShearZY;
                shearMatrix[1, 3] = -ShearZY * ShearZref;

                composeMatrix = shearMatrix * composeMatrix;

                Vector3[] points = GizmosExtension.GetCubePoints(new Vector3(0.25f, 0.25f, 0.25f), new Vector3(0.5f, 0.5f, 0.5f));

                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = composeMatrix.MultiplyPoint(points[i]);
                }

                Gizmos.color = Color.red;
                GizmosExtension.DrawWireCube(points);
                break;
            default:
                break;
        }


    }
}
