using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI.Menus
{
    public class ButtonAnimateScript : MonoBehaviour
    {
        [Header("Button Settings")]
        public Button BtnTarget;
        private Image BtnImage;
        private RectTransform rectComponent;

        [Header("Animation Type")]
        [Tooltip("Select Animation Type.")]
        [SerializeField]
        private AnimateType _animateType = AnimateType.PulseSize;

        [Header("Pulse Settings")]
        [Tooltip("DoPulseEndValue is the size of the pulse after one cycle.")]
        public float DoPulseEndValue = 1.5f;
        public float DoPulseDuration = .8f;
        public int DoPulseLoopCycles = 2;

        [Header("Pivot Settings")]
        [Tooltip("Not useful at the moment, maybe needed for Rotate..")]
        public Vector2 DoPivotEndValue;
        public float DoPivotDuration = 1.5f;

        [Header("Rotate Settings")]
        [Tooltip("Rotate works but not rotating in place, possible pivot issue..")]
        public Vector3 DoRotateEndValue;
        public float DoRotateDuration = 1.5f;

        [Header("FadeOutIn Settings")]
        [Tooltip("Fade Out/in End points is the alpha chanel value for the btn image.")]
        public float DoFadeDuration = 1.5f;
        public float DoFadeOutEndPoint;
        public float DoFadeInEndPoint;

        [Header("ChangeColour Settings")]
        [Tooltip("Transition between 2 Colours (Set Transparency full or button disappears)")]
        public Color DoColourStartingColour = new Color(255, 255, 255, 255);
        public float DoColourDuration = 1.5f;
        public float DOFillAmountEndValue;
        public float DOFillAmountDuration = 1.5f;
        public Color DoColourEndingColour = new Color(255, 255, 255, 255);

        [Header("Rotation, Easing & Loop Type Setter")]
        [Tooltip("Applies to all animations.")]
        [SerializeField]
        private Ease _moveEase = Ease.Linear;
        [SerializeField]
        private LoopType _loopType = LoopType.Yoyo;
        [SerializeField]
        private RotateMode _rotateMode = RotateMode.Fast;

        private enum AnimateType
        {
            ColourChange,
            FadeOutIn,
            PulseSize,
            Pivot,
            Rotate
        }

        // Start/Stop looping animations
        private bool _startAnimation;

        private void Start()
        {
            rectComponent = BtnTarget.GetComponent<RectTransform>();
            BtnImage = BtnTarget.GetComponent<Image>();

            if (_animateType.Equals(AnimateType.Rotate))
            {
                BtnTarget.onClick.AddListener(Rotate);
            }
            else if (_animateType.Equals(AnimateType.FadeOutIn))
            {
                BtnTarget.onClick.AddListener(FadeOutIn);
            }
            else if (_animateType.Equals(AnimateType.ColourChange))
            {
                _startAnimation = !_startAnimation;
                BtnTarget.onClick.AddListener(ColourChange);
            }
            else if (_animateType.Equals(AnimateType.PulseSize))
            {
                BtnTarget.onClick.AddListener(PulseSize);
            }
            else if (_animateType.Equals(AnimateType.Pivot))
            {
                BtnTarget.onClick.AddListener(Pivot);
            }
        }

        private void FixedUpdate()
        {
            //rectComponent.rotation = Quaternion.Euler(0f, 0f, rotateSpeed * Time.deltaTime );
            //rectComponent.RotateAround(Quaternion.Euler(0f, 0f, rotateSpeed * Time.deltaTime));
            //rectComponent.RotateAround(rectComponent.position, Vector3.up, 30 * Time.deltaTime);

        }

        void Rotate()
        {
            Debug.Log("Rotate...");

            rectComponent.DORotate(DoRotateEndValue, DoRotateDuration, _rotateMode);
            // Flip the rotation
            if (DoRotateEndValue.z == rectComponent.rotation.z)
            {
                DoRotateEndValue.z -= 180f;
            }
            //if (DoRotateEndValue.z == -180f)
            //{
            //    DoRotateEndValue.z = 0f;
            //}
        }

        void Pivot()
        {
            Debug.Log("Pivot...");
            rectComponent.DOPivot(DoPivotEndValue, DoPivotDuration);
        }

        void FadeOutIn()
        {
            Debug.Log("Fade...");
            DOTween.Sequence()
            .Append(BtnImage.DOFade(DoFadeOutEndPoint, DoFadeDuration).SetAutoKill(false))
            .Append(BtnImage.DOFade(DoFadeInEndPoint, DoFadeDuration).SetAutoKill(false));
        }

        void ColourChange()
        {
            if (_startAnimation)
            {
                Debug.Log("ColourChangeStart...");
                // Animate the circle outline's color and fillAmount
                BtnImage.DOColor(DoColourStartingColour, DoColourDuration).SetEase(_moveEase);
                BtnImage.DOFillAmount(DOFillAmountEndValue, DOFillAmountDuration).SetEase(_moveEase).SetLoops(-1, _loopType)
                    .OnStepComplete(() =>
                    {
                    //BtnImage.fillClockwise = !BtnImage.fillClockwise;
                    BtnImage.DOColor(DoColourEndingColour, DoColourDuration).SetEase(_moveEase);
                    });
            }

            Debug.Log("ColourChangeEnd...");
        }

        private void PulseSize()
        {
            Debug.Log("Pulse...");
            BtnTarget.transform.DOScale(DoPulseEndValue, DoPulseDuration)
                .SetLoops(DoPulseLoopCycles, _loopType)
                .SetEase(_moveEase);
        }

        //private void PulseColour()
        //{
        //    DOTween. Play();
        //}
    }
}