using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDTranslation : MonoBehaviour
{

    public Vector2 P1 = new Vector2();

    public Vector2 P2 = new Vector2();

    public float Tx;

    public float Ty;
    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        GizmosExtension.DrawLineWithSphere(P1, P2, 0.1f);

        Vector3 t1 = new Vector3(P1.x + Tx, P1.y + Ty);
        Vector3 t2 = new Vector3(P2.x + Tx, P2.y + Ty);

        Gizmos.color = Color.red;
        GizmosExtension.DrawLineWithSphere(t1, t2, 0.1f);

    }

    


}
