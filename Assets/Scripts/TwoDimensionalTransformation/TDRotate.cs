using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDRotate : MonoBehaviour
{
    public Vector2[] Points = new Vector2[2];

    public Vector2 Pivot;

    public float Angle;


    Vector2[] Rotate(Vector2[] points, Vector2 pivot, float theta)
    {
        Vector2[] rotatePoints = new Vector2[points.Length];
        theta = theta * Mathf.Deg2Rad;
        for (int i = 0; i < points.Length; i++)
        {
            //formula (1.4.5)
            rotatePoints[i].x = pivot.x + (points[i].x - pivot.x) * Mathf.Cos(theta) - (points[i].y - pivot.y) * Mathf.Sin(theta);
            rotatePoints[i].y = pivot.y + (points[i].x - pivot.x) * Mathf.Sin(theta) + (points[i].y - pivot.y) * Mathf.Cos(theta);
        }
        return rotatePoints;
    }

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        GizmosExtension.DrawLineWithSphere(Points[0],Points[1],0.1f);
 
        Vector2[] newPos = Rotate(Points, Pivot,Angle);


        Gizmos.color = Color.red;
        GizmosExtension.DrawLineWithSphere(newPos[0], newPos[1], 0.1f);
    }


}
