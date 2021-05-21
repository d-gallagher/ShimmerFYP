using UnityEngine;
using UnityEngine.UI;

namespace UI.Shimmer
{
    public class AdjustShimmerRig : MonoBehaviour
    {
        // Assign in the inspector
        public GameObject objectToRotate;
        public Slider sliderX;
        public Slider sliderY;
        public Slider sliderZ;

        // Preserve the original and current orientation
        private float previousValue;

        void Awake()
        {
            //SliderXListener();
            SliderYListener();
            //SliderZListener();
        }

        #region == SliderXAxis == 
        void OnSliderXChanged(float value)
        {
            // How much we've changed
            float delta = value - this.previousValue;
            this.objectToRotate.transform.Rotate(Vector3.left * delta * 360);

            // Set our previous value for the next change
            this.previousValue = value;
        }

        void SliderXListener()
        {
            // Assign a callback for when this slider changes
            this.sliderX.onValueChanged.AddListener(this.OnSliderXChanged);

            // And current value
            this.previousValue = this.sliderX.value;
        }
        #endregion
        #region == SliderYAxis == 
        void OnSliderYChanged(float value)
        {
            // How much we've changed
            float delta = this.previousValue - value;
            this.objectToRotate.transform.Rotate(Vector3.up * delta * 360);

            // Set our previous value for the next change
            this.previousValue = value;
        }


        void SliderYListener()
        {
            // Assign a callback for when this slider changes
            this.sliderY.onValueChanged.AddListener(this.OnSliderYChanged);

            // And current value
            this.previousValue = this.sliderY.value;
        }
        #endregion
        #region == SliderZAxis == 
        void OnSliderZChanged(float value)
        {
            // How much we've changed
            float delta = value - this.previousValue;
            this.objectToRotate.transform.Rotate(Vector3.forward * delta * 360);

            // Set our previous value for the next change
            this.previousValue = value;
        }

        void SliderZListener()
        {
            // Assign a callback for when this slider changes
            this.sliderZ.onValueChanged.AddListener(this.OnSliderZChanged);

            // And current value
            this.previousValue = this.sliderZ.value;
        }
        #endregion
    }
}