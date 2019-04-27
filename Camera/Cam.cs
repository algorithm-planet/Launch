using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Cam_path
{
    public static Orientation position(Vector3 O , Vector3 K , float yaw , float R = 10f , float t = 0f)
    {
        Vector3 v = -Vector3.Cross(K, Vector3.one);

        Vector3 k = -Vector3.Cross(v, K);

        Vector3 n = Tool_.Rotate(v, k, yaw * Mathf.Deg2Rad);
        float angle = t * 360f;
        Vector3 new_v = Tool_.Rotate(n, K, angle * Mathf.Deg2Rad);

        return new Orientation(O, new_v * R);
    }
}


public class Orientation
{
    public Vector3 postion;
    public Vector3 dir;

    public Orientation(Vector3 O , Vector3 v)
    {
        postion = O + v;
        dir = (-v).normalized;
    }
}
