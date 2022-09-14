using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDraw : MonoBehaviour
{
    public bool drawFrustum = true;

    public GameObject cube;

    public Vector3 start = new Vector3(10,10,-20);

    public Vector3 end = new Vector3(10, 10, -20);
    // Start is called before the first frame update
    void Start()
    {
     
      
    }

    // Update is called once per frame
    void Update()
    {

        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(transform.position, forward, Color.green);
    }

    private float q = 0.0f;
    private void FixedUpdate()
    {
        //Color color = new Color(q, q, 1.0f);

        //Debug.DrawLine(Vector3.zero, new Vector3(0, 5, 0), color);

        //q = q + 0.01f;

        //if (q > 1.0f)
        //    q = 0f;
    }


    private void OnDrawGizmos()
    {
        Camera camera = this.GetComponent<Camera>();
        if (drawFrustum)
        {
            Gizmos.color = Color.red;
            Matrix4x4 temp = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

            Gizmos.DrawFrustum(Vector3.zero, camera.fieldOfView, camera.farClipPlane, camera.nearClipPlane, camera.aspect);

            Gizmos.matrix = temp;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(start,end);
        Vector3 s1 = camera.projectionMatrix.MultiplyPoint(start);
        Vector3 e1 = camera.projectionMatrix.MultiplyPoint(end);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(s1, e1);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(2, 2, 2));
    }

}
