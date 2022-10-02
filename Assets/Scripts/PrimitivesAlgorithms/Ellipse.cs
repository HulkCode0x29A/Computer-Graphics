using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ellipse : MonoBehaviour
{
    public PixelScreen Screen;

    public Vector2 Center;

    public int EllipseRx;

    public int EllipseRy;

    private void Update()
    {
        Screen.Clear();

        EllipseMidpoint((int) Center.x,(int) Center.y, EllipseRx, EllipseRy);
    }
    void EllipseMidpoint(int xCenter, int yCenter, int rx, int ry)
    {
        int rx2 = rx * rx;
        int ry2 = ry * ry;
        int twoRx2 = 2 * rx2;
        int twoRy2 = 2 * ry2;
        int p;
        int x = 0;
        int y = ry;
        int px = 0;
        int py = twoRx2 * y;

        EllipsePlotPoints(xCenter, yCenter, x, y);
        //Region 1
        p = Mathf.RoundToInt(ry2 - (rx2 * ry) + (0.25f * rx2));// in the PPT formula 9
        while (px < py)
        {
            x++;
            px += twoRy2;//in the PPT complete steps 3 because  2r_y^2 times x =0 so Later iterations only need add twoRy2
            if (p < 0)
                p += ry2 + px;//in the PPT formula 5
            else
            {
                y--;
                py -= twoRx2;//int the PPT complete steps 3
                p += ry2 + px - py;//in the PPT formula 6
            }

            EllipsePlotPoints(xCenter, yCenter, x, y);
        }

        //Region 2
        p = Mathf.RoundToInt(ry2 * (x + 0.5f) * (x + 0.5f) + rx2 * (y - 1) * (y - 1) - rx2 * ry2);//take last pos into ellipse equation
        while (y > 0)
        {
            y--;
            if (p > 0)
                p += rx2 - py;//in PPT formula 13
            else
            {
                x++;
                px += twoRy2;
                p += rx2 - py + px;//in PPT formula 14
            }

            EllipsePlotPoints(xCenter, yCenter, x, y);
        }
    }

    void EllipsePlotPoints(int xCenter, int yCenter, int x, int y)
    {
        Screen.SetPixel(xCenter + x, yCenter + y, Color.green);
        Screen.SetPixel(xCenter - x, yCenter + y, Color.green);
        Screen.SetPixel(xCenter + x, yCenter - y, Color.green);
        Screen.SetPixel(xCenter - x, yCenter - y, Color.green);
    }
}
