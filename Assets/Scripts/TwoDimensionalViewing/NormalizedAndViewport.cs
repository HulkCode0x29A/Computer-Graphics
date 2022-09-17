using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalizedAndViewport : MonoBehaviour
{
    Matrix4x4 composeMatrix = Matrix4x4.identity;

    public PixelScreen Screen;

    public Vector2 ClipWindowCenter = new Vector2();
    //left right bottom top
    public Vector4 ClipWindowArg = new Vector4();

    public Vector2 P1;

    public Vector2 P2;

    public Vector2 T1;

    public Vector2 T2;

    public Vector2 V1;

    public Vector2 V2;

  

    private void Update()
    {
        Screen.Clear();
        Screen.SetLine(V1.x, V1.y, V2.x, V2.y, Color.green, Color.green);
    }
    private void OnDrawGizmos()
    {
        
        GizmosExtension.DrawQuad(ClipWindowCenter, ClipWindowArg.x, ClipWindowArg.y, ClipWindowArg.z, ClipWindowArg.w);
        Gizmos.color = Color.green;
        //set z to -1 void overlap
        GizmosExtension.DrawNormalizedQuad(new Vector3(0,0,-1));

        Gizmos.color = Color.green;
        GizmosExtension.DrawLineWithSphere(P1,P2, 0.1f);

        composeMatrix = Matrix4x4.identity;
        Matrix4x4 clipToNormalize = TwoDimensionalMatrix.GetClipToNormalizeMatrix(ClipWindowArg.x, ClipWindowArg.y, ClipWindowArg.z, ClipWindowArg.w);
        composeMatrix = clipToNormalize * composeMatrix;

        T1 = clipToNormalize.MultiplyPoint(P1);
        T2 = clipToNormalize.MultiplyPoint(P2);
        Gizmos.color = Color.red;
        GizmosExtension.DrawLineWithSphere(new Vector3(T1.x , T1.y, -1),new Vector3(T2.x, T2.y,-1),0.1f);

        Matrix4x4 normalizeToViewport = TwoDimensionalMatrix.GetNormalizeToViewportMatrix(0, Screen.WidthCount -1, 0,Screen.HeightCount -1);
        composeMatrix = normalizeToViewport * composeMatrix;
        V1 = normalizeToViewport.MultiplyPoint(T1);
        V2 = normalizeToViewport.MultiplyPoint(T2);
      


    }
}
