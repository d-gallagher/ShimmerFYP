using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleTestScript : MonoBehaviour
{
    public GameObject Target;

    public Text X_Output;
    public Text Y_Output;
    public Text Z_Output;

    // Update is called once per frame
    void Update()
    {
        Y_Output.text = "Y: " + Vector3.Angle(Target.transform.up, Vector3.forward);
        X_Output.text = "X: " + Vector3.Angle(Target.transform.right, Vector3.left);
        Z_Output.text = "Z: " + Vector3.Angle(Target.transform.forward, Vector3.up);
    }
}
