using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDA : MonoBehaviour
{
    public PixelScreen Screen;

    public Vector2 Start;

    public Vector2 End;

    private void Update()
    {
        Screen.Clear();

        LineDDA((int)Start.x, (int)Start.y, (int)End.x, (int)End.y);
    }

    void LineDDA(int x0, int y0, int xEnd, int yEnd)
    {
        int dx = xEnd - x0;
        int dy = yEnd - y0;
        int steps;

        float xIncrement, yIncrement, x = x0, y = y0;

        if (Mathf.Abs(dx) > Mathf.Abs(dy))
            steps = Mathf.Abs(dx);
        else
            steps = Mathf.Abs(dy);

        xIncrement = (float)dx / steps;
        yIncrement = (float)dy / steps;

        Screen.SetPixel(Mathf.RoundToInt(x) , Mathf.RoundToInt(y),Color.green);
        for (int k = 0; k < steps;k++)
        {
            x += xIncrement;
            y += yIncrement;
            Screen.SetPixel(Mathf.RoundToInt(x),Mathf.RoundToInt(y), Color.green);
        }
    }
}
