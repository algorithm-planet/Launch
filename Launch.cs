using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch
{
    public static Projectile projectile(Vector3 start , Vector3 end , Vector3 G)
    {
        Vector3 n = end - start;

        Vector3 k = -Vector3.Cross(G, n);
        Vector3 a = -Vector3.Cross(k, G).normalized;
        Vector3 b = -G.normalized;

        #region Initialize
        float x = Vector3.Dot(n, a);
        float y = Vector3.Dot(n, b);
        float g = Vector3.Dot(G, b);

        #region x = 0f
        if (x == 0f)
        {
            x = 0.01f;
        }
        #endregion
        #endregion

        #region v
        float C = g * x * x;
     
        float v = Mathf.Sqrt( Q(x * x  ,  2 * C * y , -C * C , Root.Max) );
        #endregion

        #region Debug
        Debug.DrawRay(start , a * x , Color.red , Time.deltaTime);
        Debug.DrawRay(start , b * y , Color.red , Time.deltaTime);
        #endregion

        float Const = (g / 2) * Mathf.Pow(x / v, 2);
        float Angle = Mathf.Atan(   Q(Const , x ,  Const - y , Root.Max)   );

        #region anchor
        Vector3 anchor = a * (x / 2) + b * (Mathf.Tan(Angle)) * (x / 2);
        #endregion

        Bezier bezier_ = new Bezier(start, start + anchor, end);

        #region time
        float time_ = Q(g / 2, v * Mathf.Sin(Angle), -y , Root.Max);
        Debug.Log("time : " + time_);
        #endregion

        return new Projectile()
        {
            bezier = bezier_,
            time = time_,
            O = start + (a * x + b * y) / 2f
        };
    }

    #region Methods
    #region enum
    public enum Root
    {
        Max , min
    }
    #endregion

    static float Q(float a , float b , float c , Root root = Root.Max)
    {
        float d = b * b - 4 * a * c;

        #region d
        if (d < 0f)
        {
            Debug.Log("d  <  0f");
            d = 0f;
        }
        #endregion

        float num_1 = -b  +  Mathf.Sqrt(d);
        float num_2 = -b  -  Mathf.Sqrt(d);
        float deno_ = 2 * a;

        #region x
        float x = Mathf.Min(num_1 / deno_, num_2 / deno_);
        if (root == Root.Max)
        {
            x = Mathf.Max(num_1 / deno_, num_2 / deno_);
        }
        #endregion

        return Round_decimal(x);
    }

    #region Round_decimal
    static float Round_decimal(float x, int i = 2)
    {
        float x_i = Mathf.Floor(x);
        float x_f = x - x_i;//

        float l = Mathf.Log(x_f, 10f);
        int index = -Mathf.FloorToInt(l) + (i - 1);

        return Mathf.Round(x * Mathf.Pow(10, index)) / Mathf.Pow(10, index);
    }
    #endregion

    #endregion
}

public static class Tool_
{
    #region Rotate
    public static Vector3 Rotate(Vector3 v , Vector3 k , float Angle)
    {
        Vector3 v_paralell = k * Vector3.Dot(v, k);

        Vector3 n1 = -Vector3.Cross(k, v);
        Vector3 n2 = -Vector3.Cross(-k, n1);

        Vector3 new_v_perpendicular = n2 * Mathf.Cos(Angle) + n1 * Mathf.Sin(Angle);

        Vector3 new_v = v_paralell + new_v_perpendicular;
        return new_v;
    }
    #endregion
    
    #region Angle

    #region Angle_btw
    public static float Angle_btw(Vector3 n1 , Vector3 n2 , Vector3 k)
    {
        float det_ = Vector3.Dot(k, -Vector3.Cross(n1, n2));
        float dot_ = Vector3.Dot(n1, n2);

        float A = Atan2(det_, dot_);
        return A;
    }
    #endregion

    #region Atan2
    static float Atan2(float y , float x)
    {
        float Angle = 0f;
        if(x == 0f)
        {
            if(y > 0f)
            {
                Angle = Mathf.PI / 2f;
            }
            else if(y < 0f)
            {
                Angle = -Mathf.PI / 2f;
            }
        }
        else if(x > 0f)
        {
            Angle = Mathf.Atan(y / x);
        }
        #region x < 0f
        else if (x < 0f)
        {
            if(y > 0f)
            {
                Angle = Mathf.Atan(y / x) + Mathf.PI;
            }
            else
            {
                Angle = Mathf.Atan(y / x) - Mathf.PI;
            }
        }
        #endregion

        return Angle;
    }
    #endregion

    #endregion
}


#region Bezier
public class Bezier
{
    Vector3[] p_;

    public Bezier(Vector3 a , Vector3 b , Vector3 c)
    {
        p_ = new Vector3[3]
        {
            a , b , c
        };
    }

    #region position
    public Vector3 position(float t)
    {
        Vector3 a = Lerp_(p_[0], p_[1], t);
        Vector3 b = Lerp_(p_[1], p_[2], t);

        return Lerp_(a, b, t);
    }
    Vector3 Lerp_(Vector3 a , Vector3 b , float t)
    {
        Vector3 n = b - a;
        return a + n * t;
    }
    #endregion

    #region velocity
    public Vector3 velocity(float t)
    {
        Vector3 Sum = (1 - t) * (p_[1] - p_[0]) +
                        (t)   * (p_[2] - p_[1]);
        return 2 * Sum;
    }
    #endregion

}
#endregion


public struct Projectile
{
    public Bezier bezier;
    public float time;

    public Vector3 O;
}



