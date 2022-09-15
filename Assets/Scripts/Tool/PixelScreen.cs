using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelScreen : MonoBehaviour
{
    public GameObject PixelGo;

    public Vector3 PixelSize = Vector3.one;

    public  int WidthCount = 50;

    public  int HeightCount = 50;

    public int ScreenWidth = 10;

    public int ScreenHeight = 10;

    private GameObject[][] pixels;

    private Material[][] pixelMaterials;

    private Color ClearColor = Color.white;
    // Start is called before the first frame update
    void Awake()
    {
        pixels = new GameObject[WidthCount][];
        pixelMaterials = new Material[WidthCount][];
        for (int i = 0; i < WidthCount; i++)
        {
            for (int j = 0; j < HeightCount; j++)
            {
                if (null == pixels[i])
                {
                    pixels[i] = new GameObject[HeightCount];
                    pixelMaterials[i] = new Material[HeightCount];
                }
                    
                GameObject temp = GameObject.Instantiate(PixelGo);
                temp.name += "_"+i+"_" + j;
                float w = i * (float)ScreenWidth / WidthCount;
                float h = j* (float)ScreenHeight / HeightCount;
                temp.transform.SetParent(this.transform);
                temp.transform.localPosition = new Vector3(w,h,0);
                temp.transform.localScale = PixelSize;

                pixels[i][j] = temp;
                pixelMaterials[i][j] = temp.GetComponent<MeshRenderer>().material;
            }
        }
    }

    public void SetLine(float x0, float y0, float x1, float y1, Color startColor,Color endColor)
    {
        SetLine((int)x0, (int)y0, (int)x1,(int)y1,  startColor,  endColor);
    }

    public void SetLine(int x0,int y0, int x1, int y1, Color startColor, Color endColor)
    {
        if (!CheckPosition(x0, y0, x1, y1))
            return;

        bool steep = false;
        if(Mathf.Abs(x0 - x1) < Mathf.Abs(y0 - y1))
        {
            Swap(ref x0, ref y0);
            Swap(ref x1, ref y1);
            steep = true;
        }

        if(x0 > x1)//make it left-to-right
        {
            Swap(ref x0, ref x1);
            Swap(ref y0, ref y1);
        }

        for (int x = x0; x <= x1; x++)
        {
            float t = (x - x0) / (float)(x1 - x0);
            int y = (int)(y0 * (1.0 - t) + y1 * t);
            Color color = startColor * (float)(1.0 - t) + endColor * t;
            if (steep)
                SetPixel(y, x, color);
            else
                SetPixel(x,y,color);
        }
    }

    private bool CheckPosition(int x0, int y0, int x1, int y1)
    {
        if (x0 >= this.WidthCount || y0 >= this.HeightCount || x1 >= this.WidthCount || y1 >= this.HeightCount)
            return false;
        if ((x1 - x0) == 0 || (x0 - x1) == 0)
            return false;
        if (x0 < 0 || y0 < 0 || x1 < 0 || y1 < 0)
            return false;

        return true;
    }

    private void Swap(ref int a, ref int b)
    {
        int c = a;
        a = b;
        b = c;
    }

    public void SetPixel(int x, int y, Color color)
    {
        if (x >= this.WidthCount || y >= this.HeightCount || x < 0 || y < 0)
            return;

        pixelMaterials[x][y].color = color;
    }

    public void SetClearColor(Color color)
    {
        ClearColor = color;
    }
    public void Clear()
    {
        for (int i = 0; i < WidthCount; i++)
        {
            for (int j = 0; j < HeightCount; j++)
            {
                pixelMaterials[i][j].color = ClearColor;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
