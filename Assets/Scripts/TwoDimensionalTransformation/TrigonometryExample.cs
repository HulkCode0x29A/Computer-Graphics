using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigonometryExample : MonoBehaviour
{
    

    public int Angle;
       
    
    void Start()
    {
        float radian = Angle * (Mathf.PI / 180);
        //float radian = Angle * Mathf.Deg2Rad;
        Debug.Log(Mathf.Sin(radian));
        Debug.Log(Mathf.Cos(radian));
        Debug.Log(Mathf.Tan(radian));
        //cot
        Debug.Log(1 / Mathf.Tan(radian));
    }

    
}
