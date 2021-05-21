using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoYScript : BaseGizmoScript
{
    // Update is called once per frame
    void Update()
    {
        _angleAroundAxis = Target.transform.rotation.eulerAngles.y;
        //Debug.Log(_angleAroundAxis);
        _lineRenderer.positionCount = 0;
        DrawArc(_startPosition, _angleAroundAxis, AxisToWatch);
    }

    private void DrawArc(Vector3 startPosition, float angleToAxis, Axis axis)
    {
        if (angleToAxis == 0) return;

        var points = GetArcPoints(startPosition, angleToAxis, true);

        _lineRenderer.positionCount = points.Count;
        _lineRenderer.SetPositions(points.ToArray());
    }

    private List<Vector3> GetArcPoints(Vector3 startPoint, float angleToAxis, bool isPositive)
    {
        float direction = angleToAxis > 0 && angleToAxis < 180 ? 1.0f : -1.0f;
        float arcLength = direction == 1 ? angleToAxis : 360 - angleToAxis;

        int numSegments = 128;
        float angle = 0.0f;
        //float arcLength = mod == 1 ? angleToAxis : 360 + angleToAxis;
        //float arcLength = angleToAxis * mod;


        List<Vector3> points = new List<Vector3>();
        points.Add(_origin);
        points.Add(startPoint);

        for (int i = 0; i < numSegments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle * direction) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle * direction) * radius;

            points.Add(new Vector3(x, 0.0f, y) + _origin);

            //points.Add(GetPointForAxis(x, y, AxisToWatch, direction));

            angle += (arcLength / numSegments);
        }
        points.Add(_origin);

        return points;
    }
}