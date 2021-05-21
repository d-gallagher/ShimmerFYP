using UnityEngine;

public class AngleGaugesScript : MonoBehaviour
{
    public GameObject Target;

    [Header("X Rotation")]
    public GameObject XGauge;
    public float XOffset;
    public int XModifier;
    private GaugeRotationScript _XGaugeRotationScript;

    [Header("Y Rotation")]
    public GameObject YGauge;
    public float YOffset;
    public int YModifier;
    private GaugeRotationScript _YGaugeRotationScript;

    [Header("Z Rotation")]
    public GameObject ZGauge;
    public float ZOffset;
    public int ZModifier;
    private GaugeRotationScript _ZGaugeRotationScript;

    private Vector3 _currentAngles;

    private void Start()
    {
        _currentAngles = new Vector3();


        _XGaugeRotationScript = XGauge.GetComponentInChildren<GaugeRotationScript>();
        _YGaugeRotationScript = YGauge.GetComponentInChildren<GaugeRotationScript>();
        _ZGaugeRotationScript = ZGauge.GetComponentInChildren<GaugeRotationScript>();
    }

    // Update is called once per frame
    private void Update()
    {
        GetAngles(Target.transform.localRotation.eulerAngles);

        _XGaugeRotationScript.SetAngle(_currentAngles.x);
        _YGaugeRotationScript.SetAngle(_currentAngles.y);
        _ZGaugeRotationScript.SetAngle(_currentAngles.z);
    }

    private void GetAngles(Vector3 eulers)
    {
        _currentAngles.x = (eulers.x - XOffset) * XModifier;
        _currentAngles.y = (eulers.y - YOffset) * YModifier;
        _currentAngles.z = (eulers.z - ZOffset) * ZModifier;
    }
}
