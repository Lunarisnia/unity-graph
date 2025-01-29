using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform PointPrefab;

    [Range(10, 100)] public int resolution = 10;

    public FunctionName functionName;

    private Transform[] points;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        points = new Transform[resolution * resolution];
        var step = 2f / resolution;
        var scale = Vector3.one * step;
        for (var i = 0; i < points.Length; i++)
        {
            var point = Instantiate(PointPrefab, transform);

            point.localScale = scale;
            points[i] = point;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        var step = 2f / resolution;
        for (int i = 0, x = 0, z = 0; i < points.Length; x++, i++)
        {
            if (x == resolution)
            {
                x = 0;
                z++;
            }

            var u = (x + 0.5f) * step - 1.0f;
            var v = (z + 0.5f) * step - 1.0f;
            points[i].localPosition = CalculateWave(functionName, u, v, Time.time);
        }
    }

    private Vector3 CalculateWave(FunctionName functionName, float x, float z, float t)
    {
        return FunctionLibrary.GetFunction(functionName)(x, z, t);
    }
}