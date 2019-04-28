# Launch

```C#
        Vector3 n = end - start;

        Vector3 k = -Vector3.Cross(G, n);
        Vector3 a = -Vector3.Cross(k, G).normalized;
        Vector3 b = -G.normalized;

        float x = Vector3.Dot(n, a);
        float y = Vector3.Dot(n, b);
        float g = Vector3.Dot(G, b);
```

- v
```
        float C = g * x * x;
        float v = Mathf.Sqrt( Q(x * x  ,  2 * C * y , -C * C , Root.Max) );
```

- Angle
```
        float Const = (g / 2) * Mathf.Pow(x / v, 2);
        float Angle = Mathf.Atan(   Q(Const , x ,  Const - y , Root.Max)   );
```

## anchor
```C#
        Vector3 anchor = a * (x / 2) + b * (Mathf.Tan(Angle)) * (x / 2);
        Bezier bezier_ = new Bezier(start, start + anchor, end);
```

![Debug](https://imgur.com/5tQboag)
