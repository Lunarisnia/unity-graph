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
        var position = Vector3.zero;
        var step = 2f / resolution;
        var scale = Vector3.one * step;
        for (int i = 0, x = 0, z = 0; i < points.Length; x++, i++)
        {
            if (x == resolution)
            {
                x = 0;
                z++;
            }

            var point = Instantiate(PointPrefab, transform);
            position.x = (x + 0.5f) * step - 1.0f;
            position.z = (z + 0.5f) * step - 1.0f;

            point.localPosition = position;
            point.localScale = scale;
            points[i] = point;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (var point in points)
        {
            var position = point.localPosition;
            position.y = CalculateWave(functionName, position.x, position.z, Time.time);

            point.localPosition = position;
        }
    }

    private float CalculateWave(FunctionName functionName, float x, float z, float t)
    {
        return FunctionLibrary.GetFunction(functionName)(x, z, t);
    }
}