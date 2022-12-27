using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosIntroduce : MonoBehaviour
{
    public Vector3 P1;

    public Vector3 P2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(P1,P2);

        GizmosExtension.DrawLHCoordinate(Vector3.zero);
    }
}
