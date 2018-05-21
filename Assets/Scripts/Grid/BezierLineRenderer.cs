using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierLineRenderer : MonoBehaviour {

    private LineRenderer _line;
    private Vector3 _start;
    private Vector3 _center;
    private Vector3 _end;
    public int MaxVertexCount = 20;
    public float ArcHeight = 10;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (_start == Vector3.zero)
            return;

        var pointList = new List<Vector3>();
        for (float ratio = 0; ratio <= 1; ratio += 1.0f / MaxVertexCount)
        {
            var tangentLineVertex1 = Vector3.Lerp(_start, _center, ratio);
            var tangentLineVertex2 = Vector3.Lerp(_center, _end, ratio);
            var bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
            pointList.Add(bezierPoint);
        }
        _line.positionCount = pointList.Count;
        _line.SetPositions(pointList.ToArray());
    }

    public void DrawLine(Vector3 start, Vector3 end)
    {
        _start = start;
        _center = (start + end) / 2;
        _center.y += ArcHeight;
        _end = end;
    }

    public void Clear()
    {
        _line.positionCount = 0;
        _start = Vector3.zero;
        _center = Vector3.zero;
        _end = Vector3.zero;
    }
}
