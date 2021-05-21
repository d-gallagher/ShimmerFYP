using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GizmoScript : MonoBehaviour
{
    public GameObject Target;
    public float radius = 25.0f;
    public Axis Axis;

    #region Rotation variables
    private Vector3 _origin;
    private Vector3 _startPosition;
    #endregion
    private LineRenderer _lineRenderer;

    protected bool _isDebug;
    private float _angleAroundAxis;

    private void Start() => Init();

    private void Init()
    {
        _origin = transform.parent.position;
        SetStartPositionForAxis();

        Debug.Log(_origin);
        //_startPosition = GetStartPositionForAxis(_origin, radius, AxisToWatch);
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetAngle();
        DrawArc();
    }

    private void DrawArc()
    {
        if (_angleAroundAxis == 0) return;
        Debug.Log(_angleAroundAxis);

        var points = GetArcPoints();

        _lineRenderer.positionCount = points.Length;
        _lineRenderer.SetPositions(points);
    }



    private Vector3[] GetArcPoints()
    {
        float direction = _angleAroundAxis > 0 && _angleAroundAxis < 180 ? 1.0f : -1.0f;
        float arcLength = direction == 1 ? _angleAroundAxis : 360 - _angleAroundAxis;

        int numSegments = 128;
        float angle = 0.0f;
        //float arcLength = mod == 1 ? angleToAxis : 360 + angleToAxis;
        //float arcLength = angleToAxis * mod;


        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < numSegments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle * direction) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle * direction) * radius;

            points.Add(GetPointForAxis(x, y));

            angle += (arcLength / numSegments);
        }
        points.Add(_origin);

        return points.ToArray();
    }

    private void GetAngle()
    {
        // draw an arc with the origin as its centre, which extends from YCap, 
        // to the given angle
        float angleAroundAxis = 0.0f;
        switch (Axis)
        {
            case Axis.X:
                angleAroundAxis = Target.transform.rotation.eulerAngles.x;
                break;
            case Axis.Y:
                angleAroundAxis = Target.transform.rotation.eulerAngles.y;
                break;
            case Axis.Z:
                angleAroundAxis = Target.transform.rotation.eulerAngles.z;
                break;
            default:
                break;
        }

        _angleAroundAxis = angleAroundAxis;
    }

    private Vector3 GetPointForAxis(float x, float y)
    {
        Vector3 retVal = new Vector3();
        switch (Axis)
        {
            case Axis.X:
                retVal = new Vector3(0.0f, y, x);
                break;
            case Axis.Y:
                retVal = new Vector3(x, 0.0f, y);
                break;
            case Axis.Z:
                retVal = new Vector3(-x, y, 0.0f);
                break;
            default:
                break;
        }
        return retVal + _origin;
    }

    private void SetStartPositionForAxis()
    {
        Vector3 retVal = new Vector3();
        switch (Axis)
        {
            case Axis.X:
                retVal = new Vector3(0, radius, 0);
                break;
            case Axis.Y:
                retVal = new Vector3(0, 0, radius);
                break;
            case Axis.Z:
                retVal = new Vector3(0, radius, 0);
                break;
            default:
                break;
        }
        _startPosition = retVal + _origin;
    }

    
}

public enum Axis
{
    X,
    Y,
    Z
}

