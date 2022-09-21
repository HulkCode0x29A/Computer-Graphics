using UnityEngine;

public class LiangBarskyLineCutting : MonoBehaviour
{

    public Vector2 WindowMin;

    public Vector2 WindowMax;

    public Vector2 Point1;

    public Vector2 Point2;

    private void Start()
    {
        
    }
    private void OnDrawGizmos()
    {
        GizmosExtension.DrawQuad(new Vector3(WindowMin.x, WindowMin.y, 0), new Vector3(WindowMax.x, WindowMax.y, 0));
        Gizmos.color = Color.green;
        GizmosExtension.DrawLineWithSphere(Point1, Point2, 0.1f);

        Gizmos.color = Color.red;
        LineClipLiangBarsky(WindowMin, WindowMax, Point1, Point2);
    }

    bool CliptTest(float p, float q, ref float u1, ref float u2)
    {
        float r;
        bool returnValue = true;
        if (p < 0.0)
        {
            r = q / p;
            if (r > u2)
                returnValue = false;
            else
            {
                if (r > u1)
                    u1 = r;
            }
        }
        else
        {
            if (p > 0.0)
            {
                r = q / p;
                if (r < u1)
                    returnValue = false;
                else if (r < u2)
                    u2 = r;
            }
            else
            {
                //p ==0 and line is parallel to clipping boundary
                if (q < 0.0)
                    returnValue = false;//line ouside clipping boundary
            }
        }

        return returnValue;
    }

    void LineClipLiangBarsky(Vector2 wMin, Vector2 wMax, Vector2 p1, Vector2 p2)
    {
        float u1 = 0f, u2 = 1f, dx = p2.x - p1.x, dy;
        if (CliptTest(-dx,p1.x - wMin.x, ref u1, ref u2))
        {
            if (CliptTest(dx, wMax.x - p1.x, ref u1, ref u2))
            {
                dy = p2.y - p1.y;
                if (CliptTest(-dy, p1.y - wMin.y, ref u1, ref u2))
                {
                    if(CliptTest(dy, wMax.y - p1.y, ref u1, ref u2))
                    {
                        if (u2 < 1.0)
                            p2 = new Vector2(p1.x + u2 * dx, p1.y + u2 * dy);
                        if (u1 > 0.0)
                            p1 = new Vector2(p1.x + u1 * dx, p1.y + u1 * dy);

                        Gizmos.DrawLine(p1, p2);
                    }
                }
            }
        }
    }
}
