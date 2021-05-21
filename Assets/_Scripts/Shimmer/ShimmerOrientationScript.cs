using Models;
using System.Collections.Generic;
using UnityEngine;

public class ShimmerOrientationScript : MonoBehaviour
{
    private List<UnityShimmerDataModel> _dataModels;

    // Start is called before the first frame update
    void Start()
    {
        _dataModels = new List<UnityShimmerDataModel>();
    }

    // Update is called once per frame
    void Update()
    {
        // If a data model is present...
        if (_dataModels.Count != 0)
        {
            // Apply it to the target.
            ApplyDataModel(_dataModels[0]);
            // And clear the list.
            _dataModels.Clear();
        }
    }


    public void AddDataModel(UnityShimmerDataModel m)
    {
        // Add the new Data Model to the list of models.
        _dataModels.Add(m);

        // TODO: should we just apply this directly to the Shimmer object and
        // get rid of the list completely??
        //
        // If so, apply directly here as it is received  and remove the Update 
        // method
        //ApplyDataModel(m);
    }

    /// <summary>
    /// Apply the model's rotation to the transform
    /// </summary>
    /// <param name="m"></param>
    private void ApplyDataModel(UnityShimmerDataModel m)
    {
#if UNITY_EDITOR
        ApplyCSharpRotation(m);
#else

        // not in editor - call appropriate method for platform
#if UNITY_STANDALONE_WIN
        ApplyCSharpRotation(m);

#elif UNITY_ANDROID
        ApplyJavaRotation(m);
#endif

#endif
    }

    // TODO:  Can the separate Apply Rotation methods now be removed and consolidated
    // into a single method!??
    // We should test a bit futher to make sure it is correct to do so...
    #region Apply Rotation to Object - this currently differs between C# and Android

    /// <summary>
    /// Currently the Java rotation is applied by using the Transformed rotation values.
    /// 
    /// This transformation is applied in the Java code using the Angle_Axis values from the Sensor.
    /// </summary>
    /// <param name="m"></param>
    private void ApplyJavaRotation(UnityShimmerDataModel m)
    {
        Quaternion q = new Quaternion(
           m.Q_1,
           m.Q_2,
           m.Q_3,
           m.Q_0
           );

        transform.localRotation = RotationHelper.ConvertRightHandedToLeftHandedQuaternion(q);
    }

    /// <summary>
    /// This rotation is applied by using the Quaternion values directly from the Sensor data.
    /// </summary>
    /// <param name="m"></param>
    private void ApplyCSharpRotation(UnityShimmerDataModel m)
    {
        Quaternion q = new Quaternion(
            m.Q_1,
           m.Q_2,
           m.Q_3,
           m.Q_0
            );

        transform.localRotation = RotationHelper.ConvertRightHandedToLeftHandedQuaternion(q);
    }

    #endregion
}
