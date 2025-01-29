using UnityEngine;

public enum FunctionName
{
    None,
    Wave,
    MultiWave,
    Ripple,
    Sphere,
    Torus
}

public static class FunctionLibrary
{
    public delegate Vector3 Function(float u, float v, float t);


    private static readonly Function[] _functions = { None, SineWave, MultiWave, Ripple, Sphere, Torus };

    public static Function GetFunction(FunctionName functionName)
    {
        return _functions[(int)functionName];
    }

    private static Vector3 None(float u, float v, float t)
    {
        return new Vector3(u, 0.0f, v);
    }

    private static Vector3 SineWave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Mathf.Sin(Mathf.PI * (u + v + t));
        p.z = v;
        return p;
    }

    private static Vector3 MultiWave(float u, float v, float t)
    {
        var y = SineWave(u, v, t * 0.5f).y;
        y += 0.5f * Mathf.Sin(2.0f * Mathf.PI * (v + t));

        Vector3 p;
        p.x = u;
        p.y = y * (2f / 3f);
        p.z = v;
        return p;
    }

    private static Vector3 Ripple(float u, float v, float t)
    {
        // Distance from center (0, 0)
        var d = Mathf.Sqrt(u * u + v * v);
        var y = Mathf.Sin(Mathf.PI * (4f * d - t));
        Vector3 p;
        p.x = u;
        p.y = y / (1f + 10f * d);
        p.z = v;
        return p;
    }

    private static Vector3 Sphere(float u, float v, float t)
    {
        var r = 0.9f + 0.1f * Mathf.Sin(Mathf.PI * (8f * u + 4f * v + t));
        var s = r * Mathf.Cos(Mathf.PI * 0.5f * v);
        Vector3 p;
        p.x = s * Mathf.Sin(Mathf.PI * u);
        p.y = r * Mathf.Sin(0.5f * Mathf.PI * v);
        p.z = s * Mathf.Cos(Mathf.PI * u);
        return p;
    }

    private static Vector3 Torus(float u, float v, float t)
    {
        var r1 = 0.7f + 0.1f * Mathf.Sin(Mathf.PI * (8f * u + 0.5f * t));
        var r2 = 0.15f + 0.05f * Mathf.Sin(Mathf.PI * (8f * u + 4f * v + 2f * t));
        var s = r1 + r2 * Mathf.Cos(Mathf.PI * v);
        Vector3 p;
        p.x = s * Mathf.Sin(Mathf.PI * u);
        p.y = r2 * Mathf.Sin(Mathf.PI * v);
        p.z = s * Mathf.Cos(Mathf.PI * u);
        return p;
    }
}