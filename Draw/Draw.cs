using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Draw
public class Draw
{
    public static float time = Time.deltaTime;

    #region bezier
    public static void bezier(Bezier bezier, Color color, int N = 20)
    {
        float t_s = 1f / N;
        for (float t_i = t_s; t_i <= 1f; t_i = round_decimal(t_i + t_s, 2))
        {
            Vector3 current_p = bezier.position(t_i);
            Vector3 prev_p = bezier.position(t_i - t_s);

            Debug.DrawLine(current_p, prev_p, color, time);
        }
    }
    #endregion

    static float round_decimal(float Input_, int i = 2)
    {
        return Mathf.RoundToInt(Input_ * Mathf.Pow(10, i)) / Mathf.Pow(10, i);
    }
}

#endregion