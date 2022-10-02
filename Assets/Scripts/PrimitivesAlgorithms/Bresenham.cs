using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bresenham : MonoBehaviour
{
    public PixelScreen Screen;

    public Vector2 Start;

    public Vector2 End;


    public float Slope;

    private void Update()
    {
        Screen.Clear();

        int dx = (int)(End.x - Start.x);
        int dy = (int)(End.y - Start.y);
        Slope = (float)dy / dx;

        LineBresenham((int)Start.x, (int)Start.y, (int)End.x, (int)End.y);
        //further read:https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
        //LineBresenhamGeneral((int)Start.x, (int)Start.y, (int)End.x, (int)End.y);
        //LineBresenhamHigh((int)Start.x, (int)Start.y, (int)End.x, (int)End.y);
        //LineBresenhamLow((int)Start.x, (int)Start.y, (int)End.x, (int)End.y);
    }


    //Bresenham line draw for 0 <Slope <1
    public void LineBresenham(int x0, int y0, int xEnd, int yEnd)
    {
        int dx = Mathf.Abs(xEnd - x0);
        int dy = Mathf.Abs(yEnd - y0);
        int p = 2 * dy - dx;
        int twoDy = 2 * dy, twoDyMinusDx = 2 * (dy - dx);
        int x, y;

        if (x0 > xEnd)
        {
            x = xEnd;
            y = yEnd;
            xEnd = x0;
        }
        else
        {
            x = x0;
            y = y0;
        }

        Screen.SetPixel(x, y, Color.green);

        while (x < xEnd)
        {
            x++;
            if (p < 0)
                p += twoDy;
            else
            {
                y++;
                p += twoDyMinusDx;
            }

            Screen.SetPixel(x, y, Color.green);
        }
    }


    public void LineBresenhamGeneral(int x0, int y0, int x1, int y1)
    {
        if (Mathf.Abs(y1 - y0) < Mathf.Abs(x1 - x0))
        {
            if (x0 > x1)
                LineBresenhamLow(x1, y1, x0, y0);
            else
                LineBresenhamLow(x0, y0, x1, y1);
        }
        else
        {
            if (y0 > y1)
                LineBresenhamHigh(x1, y1, x0, y0);
            else
                LineBresenhamHigh(x0, y0, x1, y1);
        }
    }

    //Bresenham line draw for |Slope|<1
    public void LineBresenhamLow(int x0, int y0, int x1, int y1)
    {
        int dx = x1 - x0;
        int dy = y1 - y0;
        int yi = 1;
        if (dy < 0)
        {
            yi = -1;
            dy = -dy;
        }

        int p = 2 * dy - dx;
        int y = y0;
        int x = x0;

        while (x < x1)
        {
            Screen.SetPixel(x, y, Color.green);
            if (p > 0)
            {
                y = y + yi;
                p = p + (2 * (dy - dx));
            }
            else
            {
                p = p + 2 * dy;
            }
            x++;
        }
    }

    //Bresenham line draw for Slope>1 or Slope < -1
    public void LineBresenhamHigh(int x0, int y0, int x1, int y1)
    {
        int dx = x1 - x0;
        int dy = y1 - y0;
        int xi = 1;
        if (dx < 0)
        {
            xi = -1;
            dx = -dx;
        }

        int p = 2 * dx - dy;
        int x = x0;
        int y = y0;

        while (y < y1)
        {
            Screen.SetPixel(x, y, Color.green);

            if (p > 0)
            {
                x = x + xi;
                p = p + (2 * (dx - dy));
            }
            else
            {
                p += 2 * dx;
            }

            y++;
        }
    }
}
