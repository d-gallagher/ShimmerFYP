using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGizmoScript : MonoBehaviour
{
    public GameObject Target;
    //public GameObject AxisCap;
    //public GameObject TempMarker;

    public float radius = 25.0f;

    protected Vector3 _origin;
    protected LineRenderer _lineRenderer;
    protected float _angleAroundAxis;
    protected Vector3 _startPosition;


    public Axis AxisToWatch;
    public Vector3 StartPointOffset;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _origin = transform.parent.position;
        //_startPosition = GetStartPositionForAxis(_origin, radius, AxisToWatch);
        _startPosition = _origin + StartPointOffset;
        _lineRenderer = GetComponent<LineRenderer>();

        //Debug.Log(_origin);
        //Debug.Log(_startPosition);
        //Debug.Log(_angleAroundAxis);
    }

    protected void DrawArc()
    {
        if (_angleAroundAxis == 0) return;

        var points = GetArcPoints(_startPosition, _angleAroundAxis, true);

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
