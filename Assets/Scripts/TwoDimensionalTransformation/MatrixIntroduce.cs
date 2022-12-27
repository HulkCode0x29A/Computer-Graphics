using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixIntroduce : MonoBehaviour
{
    //m00 m01 m02 m03
    //m10 m11 m12 m13
    //m20 m21 m22 m23
    //m30 m31 m32 m33
    private Matrix4x4 matrix1 = Matrix4x4.identity;

    private Matrix4x4 matrix2 = Matrix4x4.identity;
    // Start is called before the first frame update
    void Start()
    {
        //multiplication
        Matrix4x4 matrix3 = matrix1 * matrix2;

        //inverse
        Matrix4x4 matrix4 = matrix3.inverse;

        Debug.Log(matrix3);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
