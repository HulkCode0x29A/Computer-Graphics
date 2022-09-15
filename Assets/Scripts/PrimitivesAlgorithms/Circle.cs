using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CirclePos
{
    private int x, y;

    public void SetCoords(int xValue, int yValue)
    {
        x = xValue;
        y = yValue;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    public void IncrementX()
    {
        x++;
    }

    public void DecrementY()
    {
        y--;
    }
}
public class Circle : MonoBehaviour
{
    public PixelScreen Screen;

    public Vector2 Center;

    public int Radius;

    public void Update()
    {
        Screen.Clear();

        CircleMidpoint((int)Center.x,(int)Center.y, Radius);
    }
   void CircleMidpoint(int xc, int yc, int radius)
    {
        CirclePos circlePos = new CirclePos();

        int p = 1 - radius;//initial value for midoint

        circlePos.SetCoords(0,radius);//set coordinates for top point of circle

        CirclePlotPoints(xc, yc, circlePos);

        while (circlePos.GetX() < circlePos.GetY())
        {
            circlePos.IncrementX();
            if (p < 0)
                p += 2 * circlePos.GetX() + 1;
            else
            {
                circlePos.DecrementY();
                p += 2 * (circlePos.GetX() - circlePos.GetY()) + 1;
            }

            CirclePlotPoints(xc,yc, circlePos);
        }

        
    }

    void CirclePlotPoints(int xc, int yc,CirclePos pos )
    {
        Screen.SetPixel(xc + pos.GetX(), yc + pos.GetY(),Color.green);
        Screen.SetPixel(xc - pos.GetX(), yc + pos.GetY(), Color.green);
        Screen.SetPixel(xc + pos.GetX(), yc - pos.GetY(), Color.green);
        Screen.SetPixel(xc - pos.GetX(), yc - pos.GetY(), Color.green);
        Screen.SetPixel(xc + pos.GetY(), yc + pos.GetX(), Color.green);
        Screen.SetPixel(xc - pos.GetY(), yc + pos.GetX(), Color.green);
        Screen.SetPixel(xc + pos.GetY(), yc - pos.GetX(), Color.green);
        Screen.SetPixel(xc - pos.GetY(), yc - pos.GetX(), Color.green);

    }
}
