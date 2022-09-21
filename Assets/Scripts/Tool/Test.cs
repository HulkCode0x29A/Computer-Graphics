using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Vector3 P1;

    public Vector3 P2;

    public Vector3 P3;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(P1, P2);

        GizmosExtension.DrawLHCoordinate(Vector3.zero);
        GizmosExtension.DrawWireTriangle(P1, P2, P3);
    }
}
