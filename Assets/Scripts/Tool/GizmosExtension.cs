using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosExtension
{
    public static void DrawFrustum(float fov, float aspect, float zNear, float zFar)
    {

        float radians = (fov / 2) * Mathf.Deg2Rad;
        float halfTan = Mathf.Tan(radians);
        float top = (2 * (zNear) * halfTan) * 0.5f;
        float right = aspect * top;
        float bottom = -top;
        float left = -right;
        DrawFrustum(left, right, bottom, top, zNear, zFar);
    }
    public static void DrawFrustum(float left, float right, float bottom, float top, float zNear, float zFar)
    {
        //use right hand so  negation z
        zNear = -zNear;
        zFar = -zFar;

        Vector3 rightTop = new Vector3(right, top,zNear);
        Vector3 leftTop = new Vector3(left, top, zNear);
        Vector3 leftBottom = new Vector3(left,bottom, zNear);
        Vector3 rightBottom = new Vector3(right,bottom, zNear);
        DrawQuad(rightTop, leftTop, leftBottom, rightBottom);

        float topFar = top * zFar / zNear;
        float bottomFar = bottom * zFar / zNear;
        float leftFar = left * zFar / zNear;
        float rightFar = right * zFar / zNear;
        rightTop = new Vector3(rightFar, topFar, zFar);
        leftTop = new Vector3(leftFar, topFar, zFar);
        leftBottom = new Vector3(leftFar, bottomFar, zFar);
        rightBottom = new Vector3(rightFar, bottomFar, zFar);
        DrawQuad(rightTop, leftTop, leftBottom, rightBottom);

        Gizmos.DrawLine(Vector3.zero, rightTop);
        Gizmos.DrawLine(Vector3.zero, leftTop);
        Gizmos.DrawLine(Vector3.zero, leftBottom);
        Gizmos.DrawLine(Vector3.zero, rightBottom);
    }

    public static void DrawQuad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        Gizmos.DrawLine(p1,p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
    }

    
    public static void DrawOrthoCube(float left, float right, float bottom, float top, float zNear, float zFar)
    {
        //use right hand so  negation z
        zNear = -zNear;
        zFar = -zFar;

        float scalex = right - left;
        float scaley = top - bottom;
        float scalez = zFar - zNear;
        float centerx = left + (scalex) * 0.5f;
        float centery = bottom + (scaley) * 0.5f;
        float centerz = zNear + (scalez) * 0.5f;
        DrawWireCube(new Vector3(centerx, centery, centerz), new Vector3(scalex, scaley, scalez));
        
    }
    public static void DrawNormalizedCube()
    {
        DrawWireCube(Vector3.zero, new Vector3(2,2,2));
    }
    public static  void DrawLineWithSphere(Vector3 start, Vector3 end, float radius)
    {
        Gizmos.DrawLine(start,end);
        Gizmos.DrawSphere(start, radius);
        Gizmos.DrawSphere(end, radius);
    }
    public static void DrawWireTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p1);
    }

    public static void DrawWireCube(Vector3[] points)
    {
        Gizmos.DrawLine(points[0], points[1]);
        Gizmos.DrawLine(points[1], points[2]);
        Gizmos.DrawLine(points[2], points[3]);
        Gizmos.DrawLine(points[3], points[0]);

        Gizmos.DrawLine(points[4], points[5]);
        Gizmos.DrawLine(points[5], points[6]);
        Gizmos.DrawLine(points[6], points[7]);
        Gizmos.DrawLine(points[7], points[4]);

        Gizmos.DrawLine(points[0], points[4]);
        Gizmos.DrawLine(points[1], points[5]);
        Gizmos.DrawLine(points[2], points[6]);
        Gizmos.DrawLine(points[3], points[7]);
    }

    public static void DrawWireCube(Vector3 center, Vector3 size)
    {
        Vector3[] points = GetCubePoints(center, size);

        DrawWireCube(points);
    }

    public static Vector3[] GetCubePoints(Vector3 center, Vector3 size)
    {
        Vector3[] points = new Vector3[8];
        float halfx = size.x / 2;
        float halfy = size.y / 2;
        float halfz = size.z / 2;
        points[0] = new Vector3(center.x + halfx, center.y + halfy, center.z + halfz); 
        points[1] = new Vector3(center.x - halfx, center.y + halfy, center.z + halfz); 
        points[2] = new Vector3(center.x - halfx, center.y - halfy, center.z + halfz); 
        points[3] = new Vector3(center.x + halfx, center.y - halfy, center.z + halfz); 
        points[4] = new Vector3(center.x + halfx, center.y + halfy, center.z - halfz);
        points[5] = new Vector3(center.x - halfx, center.y + halfy, center.z - halfz);
        points[6] = new Vector3(center.x - halfx, center.y - halfy, center.z - halfz);
        points[7] = new Vector3(center.x + halfx, center.y - halfy, center.z - halfz);

        return points;
    }

    public static void DrawLHCoordinate(Vector3 center)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(center, center + new Vector3(1, 0, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(center, center + new Vector3(0, 0, 1));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(center, center + new Vector3(0, 1, 0));
    }

    public static void DrawLHCoordinate(Vector3 center, float scale)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(center, center + new Vector3(1, 0, 0) * scale);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(center, center + new Vector3(0, 0, 1) * scale);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(center, center + new Vector3(0, 1, 0) * scale);
    }

}
