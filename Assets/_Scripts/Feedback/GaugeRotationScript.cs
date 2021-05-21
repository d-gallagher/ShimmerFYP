using UnityEngine;
using UnityEngine.UI;

public class GaugeRotationScript : MonoBehaviour
{
    private RectTransform rectComponent;

    private float currentRotation = 0.0f;

    private void Start()
    {
        rectComponent = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectComponent.localRotation = Quaternion.Euler(0, 0, currentRotation);
    }

    public void SetAngle(float angle)
    {
        currentRotation = angle;
    }
}
