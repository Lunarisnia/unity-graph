using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform PointPrefab;

    [Range(10, 100)] public int resolution = 10;

    private Transform[] points;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        points = new Transform[resolution];
        var position = Vector3.zero;
        var step = 2f / resolution;
        var scale = Vector3.one * step;
        for (var i = 0; i < resolution; i++)
        {
            var point = Instantiate(PointPrefab, transform);
            position.x = (i + 0.5f) * step - 1.0f;
            position.y = position.x * position.x * position.x;
            point.localPosition = position;
            point.localScale = scale;
            points[i] = point;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        for (var i = 0; i < points.Length; i++)
        {
            var point = points[i];

            var position = point.localPosition;
            position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time));

            point.localPosition = position;
        }
    }
}