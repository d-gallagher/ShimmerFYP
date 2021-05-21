using Controllers;
using ShimmerRT.models;
using UnityEngine;

namespace Assets._Scripts.Feedback
{
    public class ImpactDetectionScript : MonoBehaviour
    {
        // variable used in impact checking to compare a model to the previous one applied to the transform
        private ShimmerDataModel lastShimmerModel = null;
        public float impactThreshold = 1.0f;

        public void AddModel(ShimmerDataModel s)
        {
            if (lastShimmerModel != null)
            {
                if (CheckImpact(s))
                {
                    // Handle Impact
                    UIControllerScript.SendNotification("--IMPACT-DETECTED--");
                }
            }
            lastShimmerModel = s;

        }

        #region Check for an Impact 
        private bool CheckImpact(ShimmerDataModel s)
        {

            float dX = Mathf.Abs((float)(lastShimmerModel.LN_X - s.LN_X));
            float dY = Mathf.Abs((float)(lastShimmerModel.LN_Y - s.LN_Y));
            float dZ = Mathf.Abs((float)(lastShimmerModel.LN_Z - s.LN_Z));

            if (dX > impactThreshold || dY > impactThreshold || dZ > impactThreshold)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
