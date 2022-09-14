using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation : MonoBehaviour
{
    Matrix4x4 transMatrix = Matrix4x4.identity;

    public Vector3 P1 = new Vector3(-1,0,0);

    public Vector3 P2 = new Vector3(1, 0, 0);

    public Vector3 P3 = new Vector3(0, 1, 0);

    public Vector3 Translate = new Vector3(5,3,2);
  
    void Start()
    {
        Debug.Log(transMatrix);
    }


    private void OnDrawGizmos()
    {
        
        transMatrix[0, 3] = Translate.x;
        transMatrix[1, 3] = Translate.y;
        transMatrix[2, 3] = Translate.z;

        Gizmos.color = Color.green;
        GizmosExtension.DrawWireTriangle(P1,P2,P3);

        Gizmos.color = Color.red;
        Vector3 t1 = transMatrix.MultiplyPoint(P1);
        Vector3 t2 = transMatrix.MultiplyPoint(P2);
        Vector3 t3 = transMatrix.MultiplyPoint(P3);
        GizmosExtension.DrawWireTriangle(t1,t2,t3);
    }
}
