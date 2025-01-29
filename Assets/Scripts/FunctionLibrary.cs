using UnityEngine;

public enum FunctionName
{
    Wave,
    MultiWave,
    Ripple
}

public static class FunctionLibrary
{
    public delegate float Function(float u, float v, float t);


    private static readonly Function[] _functions = { SineWave, MultiWave, Ripple };

    public static Function GetFunction(FunctionName functionName)
    {
        return _functions[(int)functionName];
    }

    private static float SineWave(float x, float z, float t)
    {
        return Mathf.Sin(Mathf.PI * (x + z + t));
    }

    private static float MultiWave(float x, float z, float t)
    {
        var y = SineWave(x, z, t * 0.5f);
        y += 0.5f * Mathf.Sin(2.0f * Mathf.PI * (z + t));
        return y * (2f / 3f);
    }

    private static float Ripple(float x, float z, float t)
    {
        // Distance from center (0, 0)
        var d = Mathf.Sqrt(x * x + z * z);
        var y = Mathf.Sin(Mathf.PI * (4f * d - t));
        return y / (1f + 10f * d);
    }
}