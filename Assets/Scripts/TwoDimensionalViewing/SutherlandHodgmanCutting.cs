using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SutherlandHodgmanCutting : MonoBehaviour
{
    public enum Boundary
    {
        Left,
        Right,
        Bottom,
        Top
    }

    public Vector2 WindowMin;

    public Vector2 WindowMax;

    public Vector2 Point1;

    public Vector2 Point2;

    public Vector2 Point3;

    private static int debugIdnex = -1;

    private void Start()
    {
      
    }

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawQuad(WindowMin, WindowMax);

        Gizmos.color = Color.green;
        GizmosExtension.DrawWireTriangle(Point1, Point2, Point3);

        Dictionary<int, Vector2> pIn = new Dictionary<int, Vector2>() {
            { 0,Point1},
            { 1,Point2},
            { 2,Point3},
        };
        Dictionary<int, Vector2> pOut = new Dictionary<int, Vector2>();
        PolygonClipSuthHodg(WindowMin, WindowMax, 3, pIn, pOut);

        Gizmos.color = Color.red;
        GizmosExtension.DrawWirePolygon(pOut.Values.ToArray());
    }

    public bool Inside(Vector2 p, Boundary b, Vector2 wMin, Vector2 wMax)
    {
        switch (b)
        {
            case Boundary.Left:
                if (p.x < wMin.x)
                    return false;
                break;
            case Boundary.Right:
                if (p.x > wMax.x)
                    return false;
                break;
            case Boundary.Bottom:
                if (p.y < wMin.y)
                    return false;
                break;
            case Boundary.Top:
                if (p.y > wMax.y)
                    return false;
                break;
        }

        return true;
    }

    public bool Cross(Vector2 p1, Vector2 p2, Boundary winEdge, Vector2 wMin, Vector2 wMax)
    {
        if (Inside(p1, winEdge, wMin, wMax) == Inside(p2, winEdge, wMin, wMax))
            return false;
        else
            return true;
    }

    public Vector2 Intersect(Vector2 p1, Vector2 p2, Boundary winEdge, Vector2 wMin, Vector2 wMax)
    {
        Vector2 iPt = new Vector2();
        float m = 0;

        if (p1.x != p2.x)
            m = (p1.y - p2.y) / (p1.x - p2.x);
        switch (winEdge)
        {
            case Boundary.Left:
                iPt.x = wMin.x;
                iPt.y = p2.y + (wMin.x - p2.x) * m;
                break;
            case Boundary.Right:
                iPt.x = wMax.x;
                iPt.y = p2.y + (wMax.x - p2.x) * m;
                break;
            case Boundary.Bottom:
                iPt.y = wMin.y;
                if (p1.x != p2.x)
                    iPt.x = p2.x + (wMin.y - p2.y) / m;
                else
                    iPt.x = p2.x;
                break;
            case Boundary.Top:
                iPt.y = wMax.y;
                if (p1.x != p2.x)
                    iPt.x = p2.x + (wMax.y - p2.y) / m;
                else
                    iPt.x = p2.x;
                break;
        }

        return iPt;
    }

    public void ClipPoint(Vector2 p, Boundary winEdge, Vector2 wMin, Vector2 wMax,
                          Dictionary<int, Vector2> pOut, ref int cnt, Dictionary<Boundary, Vector2> first, Dictionary<Boundary, Vector2> s)
    {
        debugIdnex++;
        Vector2 iPt;

        //first records the unprocessed points
        if (!first.ContainsKey(winEdge))
            first[winEdge] = p;
        else
        {
            if (Cross(p, s[winEdge], winEdge, wMin, wMax))
            {
                iPt = Intersect(p, s[winEdge], winEdge, wMin, wMax);
                if (winEdge < Boundary.Top)
                    ClipPoint(iPt, winEdge + 1, wMin, wMax, pOut, ref cnt, first, s);
                else
                {
                    pOut[cnt] = iPt;
                    cnt++;
                }
            }
        }

        //s records the last point processed
        s[winEdge] = p;

        if (Inside(p, winEdge, wMin, wMax))
        {
            if (winEdge < Boundary.Top)
                ClipPoint(p, winEdge + 1, wMin, wMax, pOut, ref cnt, first, s);
            else
            {
                pOut[cnt] = p;
                cnt++;
            }
        }

    }

    public void CloseClip(Vector2 wMin, Vector2 wMax, Dictionary<int, Vector2> pOut,
         ref int cnt, Dictionary<Boundary, Vector2> first, Dictionary<Boundary, Vector2> s)
    {
        Vector2 pt;
        Boundary winEdge;

        for (winEdge = Boundary.Left; winEdge <= Boundary.Top; winEdge++)
        {
            if (Cross(s[winEdge], first[winEdge], winEdge, wMin, wMax))
            {
                pt = Intersect(s[winEdge], first[winEdge], winEdge, wMin, wMax);
                if (winEdge < Boundary.Top)
                    ClipPoint(pt, winEdge + 1, wMin, wMax, pOut, ref cnt, first, s);
                else
                {
                    pOut[cnt] = pt;
                    cnt++;
                }

            }
        }
    }

    public int PolygonClipSuthHodg(Vector2 wMin, Vector2 wMax, int n, Dictionary<int, Vector2> pIn, Dictionary<int, Vector2> pOut)
    {
        debugIdnex = -1;
        Dictionary<Boundary, Vector2> first = new Dictionary<Boundary, Vector2>();
        Dictionary<Boundary, Vector2> s = new Dictionary<Boundary, Vector2>();
        int k, cnt = 0;
        for (k = 0; k < n; k++)
            ClipPoint(pIn[k], Boundary.Left, wMin, wMax, pOut, ref cnt, first, s);

        CloseClip(wMin, wMax, pOut, ref cnt, first, s);
        return cnt;
    }
}
