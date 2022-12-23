using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CohenSutherlandLineCutting : MonoBehaviour
{
  

    public Vector2 WinMin;

    public Vector2 WinMax;

    public Vector2 Point1;

    public Vector2 Point2;

    //byte order [top bottom right left]
    const int winLeftBitCode = 0x1;//0001
    const int winRightBitCode = 0x2;//0010
    const int winBottomBitCode = 0x4;//0100
    const int winTopBitCode = 0x8;//1000

    private void Start()
    {
      
    }

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawQuad(new Vector3(WinMin.x, WinMin.y,0),new Vector3(WinMax.x, WinMax.y, 0));
        Gizmos.color = Color.green;
        GizmosExtension.DrawLineWithSphere(Point1,Point2,0.1f);

        Gizmos.color = Color.red;
        LineClipCohSuth(WinMin, WinMax, Point1, Point2);
    }

    //0000 mean line is inside the clipping window
    //if 'or' operation for two points is false ,the line should be saved 
    //if 'and' operation for two poins is true, discard line 
    bool Inside(int code)
    {
        //0000 will return true
        return !Convert.ToBoolean(code);
    }

    bool Reject(int code1, int code2)
    {
        return Convert.ToBoolean(code1 & code2);
    }

    bool Accept(int code1, int code2)
    {
        //return true if(code1 | code2) is '0000')
        return !Convert.ToBoolean(code1 | code2);
    }

    int Encode(Vector2 pt, Vector2 winMin, Vector2 winMax)
    {
        int code = 0x00;

        if (pt.x < winMin.x)
            code = code | winLeftBitCode;
        if (pt.x > winMax.x)
            code = code | winRightBitCode;
        if (pt.y < winMin.y)
            code = code | winBottomBitCode;
        if (pt.y > winMax.y)
            code = code | winTopBitCode;

        return code;
    }

    void SwapPoints(ref Vector2 p1, ref Vector2 p2)
    {
        Vector2 temp = p1;

        p1.x = p2.x;
        p1.y = p2.y;

        p2.x = temp.x;
        p2.y = temp.y;
    }

    void SwapCodes(ref int c1, ref int c2)
    {
        int temp;

        temp = c1;
        c1 = c2;
        c2 = temp;
    }

    void LineClipCohSuth(Vector2 winMin, Vector2 winMax, Vector2 p1, Vector2 p2)
    {
        int code1, code2;
        bool done = false;
        bool plotLine = false;
        float m = 0;

        while (!done)
        {
            code1 = Encode(p1, winMin, winMax);
            code2 = Encode(p2, winMin, winMax);
            if(Accept(code1, code2))
            {
                done = true;
                plotLine = true;
            }
            else
            {
                if (Reject(code1, code2))
                    done = true;
                else
                {
                    //label the endpoint ouside the display window as p1
                    if (Inside(code1))
                    {
                        SwapPoints(ref p1,ref p2);
                        SwapCodes(ref code1, ref code2);
                    }

                    //use slope m find line-clipedge intersection
                    if (p2.x != p1.x)
                        m = (p2.y - p1.y) / (p2.x - p1.x);

                    if(Convert.ToBoolean(code1 & winLeftBitCode))
                    {
                        p1.y += (winMin.x - p1.x) * m;//formula (3.1.3) y=y0+(x-x0)*m
                        p1.x = winMin.x;
                    }
                    else
                    {
                        if(Convert.ToBoolean(code1 & winRightBitCode))
                        {
                            p1.y += (winMax.x - p1.x) * m;
                            p1.x = winMax.x;
                        }
                        else
                        {
                            if(Convert.ToBoolean(code1 & winBottomBitCode))
                            {
                                //need to update p1.x for nonvertical lines only
                                if (p2.x != p1.x)
                                    p1.x += (winMin.y - p1.y) / m;//formula (3.1.4) x=x0+(y-y0)/m
                                p1.y = winMin.y;
                            }
                            else
                            {
                                if(Convert.ToBoolean(code1 & winTopBitCode))
                                {
                                    if (p2.x != p1.x)
                                        p1.x += (winMax.y - p1.y) / m;
                                    p1.y = winMax.y;
                                }
                            }
                        }
                    }
                }
            }
        }

        if (plotLine)
            Gizmos.DrawLine(new Vector2(p1.x, p1.y), new Vector2(p2.x, p2.y));
    }
}
