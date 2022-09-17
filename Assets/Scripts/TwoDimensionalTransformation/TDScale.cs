using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDScale : MonoBehaviour
{
    public enum ScaleType
    {
        Origin,
        Point,
    }



    public Vector2[] Points = new Vector2[3];

    public float Sx= 1f;

    public float Sy =1f;

    public Vector2 FixedPoint = Vector2.zero;

    public Vector2[] Scale(Vector2[] p,Vector2 fixedPoint, float sx, float sy)
    {
        Vector2[] newPos = new Vector2[p.Length];
        for (int i = 0; i < p.Length; i++)
        {
            newPos[i].x = p[i].x * sx + fixedPoint.x * (1 - sx);
            newPos[i].y = p[i].y * sy + fixedPoint.y * (1 - sy);
        }

        return newPos;
    }
    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        GizmosExtension.DrawWireTriangle(Points[0],Points[1],Points[2]);

        Vector2[] newPos = Scale(Points,FixedPoint,Sx, Sy);

        Gizmos.color = Color.red;
        GizmosExtension.DrawWireTriangle(newPos[0],newPos[1],newPos[2]);
    }
}
