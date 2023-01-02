using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class EllipseData
{
    public Dictionary<string, int> EllipseConst = new Dictionary<string, int>();

    public Dictionary<string, List<int>> EllipseParameter = new Dictionary<string, List<int>>();
}
public class Ellipse : MonoBehaviour
{
    public PixelScreen Screen;

    public Vector2 Center;

    public int EllipseRx;

    public int EllipseRy;

    private bool outputFlag = false;

    private EllipseData ellipseData = new EllipseData();

    private void Update()
    {
        Screen.Clear();

        EllipseMidpoint((int)Center.x, (int)Center.y, EllipseRx, EllipseRy);
    }
    void EllipseMidpoint(int xCenter, int yCenter, int rx, int ry)
    {

        int k = 0;

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
        p = Mathf.RoundToInt(ry2 - (rx2 * ry) + (0.25f * rx2));// formula (2.4.20)

        if (!outputFlag)
            ellipseData.EllipseConst["p1_0"] = p;

        while (px < py)
        {
            x++;

            //formula (2.4.27)
            //we draw ellipse from(x,y)=(0,r_y)
            //so  2r_y^2 times x =0 so Later iterations only need add twoRy2
            px += twoRy2;
            if (p < 0)
                p += ry2 + px;//formula (2.4.16)
            else
            {
                y--;

                py -= twoRx2;//formula (2.4.27)
                p += ry2 + px - py;//formula (2.4.17)
            }

            EllipsePlotPoints(xCenter, yCenter, x, y);

            if (!outputFlag)
            {
                CheckParameterKey(ellipseData);
                ellipseData.EllipseParameter["k"].Add(k);
                ellipseData.EllipseParameter["p1_k"].Add(p);
                ellipseData.EllipseParameter["x_{k+1}"].Add(x);
                ellipseData.EllipseParameter["y_{k+1}"].Add(y);
                ellipseData.EllipseParameter["2r_y^2 x_{k+1}"].Add(px);
                ellipseData.EllipseParameter["2r_x^2 y_{k+1}"].Add(py);
            }

            k++;
        }

        //Region 2
        p = Mathf.RoundToInt(ry2 * (x + 0.5f) * (x + 0.5f) + rx2 * (y - 1) * (y - 1) - rx2 * ry2);//formula (2.4.26)
        ellipseData.EllipseConst["p2_0"] = p;
        while (y > 0)
        {
            y--;

            if (p > 0)
                p += rx2 - py;//formula (2.4.24)
            else
            {
                x++;

                px += twoRy2;//formula (2.4.27)
                p += rx2 - py + px;//formula (2.4.25)
            }

            if (!outputFlag)
            {
                ellipseData.EllipseParameter["k"].Add(k);
                ellipseData.EllipseParameter["p1_k"].Add(p);
                ellipseData.EllipseParameter["x_{k+1}"].Add(x);
                ellipseData.EllipseParameter["y_{k+1}"].Add(y);
                ellipseData.EllipseParameter["2r_y^2 x_{k+1}"].Add(px);
                ellipseData.EllipseParameter["2r_x^2 y_{k+1}"].Add(py);
            }

            EllipsePlotPoints(xCenter, yCenter, x, y);

            k++;
        }

        if (!outputFlag)
            WriteEllipseData();

        outputFlag = true;
    }

    void CheckParameterKey(EllipseData ellipseData)
    {
        if (!ellipseData.EllipseParameter.ContainsKey("k"))
            ellipseData.EllipseParameter["k"] = new List<int>();
        if (!ellipseData.EllipseParameter.ContainsKey("p1_k"))
            ellipseData.EllipseParameter["p1_k"] = new List<int>();
        if (!ellipseData.EllipseParameter.ContainsKey("x_{k+1}"))
            ellipseData.EllipseParameter["x_{k+1}"] = new List<int>();
        if (!ellipseData.EllipseParameter.ContainsKey("y_{k+1}"))
            ellipseData.EllipseParameter["y_{k+1}"] = new List<int>();
        if (!ellipseData.EllipseParameter.ContainsKey("2r_y^2 x_{k+1}"))
            ellipseData.EllipseParameter["2r_y^2 x_{k+1}"] = new List<int>();
        if (!ellipseData.EllipseParameter.ContainsKey("2r_x^2 y_{k+1}"))
            ellipseData.EllipseParameter["2r_x^2 y_{k+1}"] = new List<int>();

    }
    void WriteEllipseData()
    {
        string path = Application.dataPath + "/Scenes/PrimitivesAlgorithms/EllipseData.csv";
        if (File.Exists(path))
            File.Delete(path);

        File.Create(path).Dispose();

        List<string> lines = new List<string>();
        lines.Add("p1_0:" + ellipseData.EllipseConst["p1_0"]+",p2_0:"+ ellipseData.EllipseConst["p2_0"]);

        lines.Add("k,p1_k,x_{k+1},y_{k+1},2r_y^2 x_{k+1},2r_x^2 y_{k+1}");
        for (int i = 0; i < ellipseData.EllipseParameter["k"].Count; i++)
        {

            string str = ellipseData.EllipseParameter["k"][i] + ",";
            str += ellipseData.EllipseParameter["p1_k"][i] + ",";
            str += ellipseData.EllipseParameter["x_{k+1}"][i] + ",";
            str += ellipseData.EllipseParameter["y_{k+1}"][i] + ",";
            str += ellipseData.EllipseParameter["2r_y^2 x_{k+1}"][i] + ",";
            str += ellipseData.EllipseParameter["2r_x^2 y_{k+1}"][i];
            lines.Add(str);
        }

        File.AppendAllLines(path, lines);
    }

    void EllipsePlotPoints(int xCenter, int yCenter, int x, int y)
    {
        Screen.SetPixel(xCenter + x, yCenter + y, Color.green);
        Screen.SetPixel(xCenter - x, yCenter + y, Color.green);
        Screen.SetPixel(xCenter + x, yCenter - y, Color.green);
        Screen.SetPixel(xCenter - x, yCenter - y, Color.green);
    }
}
