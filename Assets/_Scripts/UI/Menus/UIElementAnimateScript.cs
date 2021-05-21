using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI.Animations
{
    public class UIElementAnimateScript : MonoBehaviour
    {
        [Header("Button Settings")]
        //public Button BtnTarget;
        private Image _image;
        private RectTransform _rect;

        [Header("Animation Type")]
        [Tooltip("Select Animation Type.")]
        [SerializeField]
        private AnimateType _animateType = AnimateType.Pulse;

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
        public Color DoColourStartingColour;
        public float DoColourDuration = 1.5f;
        public float DOFillAmountEndValue;
        public float DOFillAmountDuration = 1.5f;
        public Color DoColourEndingColour;

        [Header("Rotation, Easing & Loop Type Setter")]
        [Tooltip("Applies to all animations.")]
        [SerializeField]
        private Ease _moveEase = Ease.Linear;
        [SerializeField]
        private LoopType _loopType = LoopType.Yoyo;
        [SerializeField]
        private RotateMode _rotateMode = RotateMode.Fast;

        public enum AnimateType
        {
            None,
            ColourChange,
            FadeOutIn,
            Pulse,
            Pivot,
            Rotate
        }
        private void Start()
        {
            _rect = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            switch (_animateType)
            {
                case AnimateType.ColourChange:
                    ColourChange();
                    break;
                case AnimateType.FadeOutIn:
                    FadeOutIn();
                    break;
                case AnimateType.Pulse:
                    Pulse();
                    break;
                case AnimateType.Pivot:
                    Pivot();
                    break;
                case AnimateType.Rotate:
                    Rotate();
                    break;
                default:
                    break;
            }
        }

        public void SetAnimation(AnimateType animateType)
        {
            _animateType = animateType;
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

            _rect.DORotate(DoRotateEndValue, DoRotateDuration, _rotateMode);
            // Flip the rotation
            if (DoRotateEndValue.z == _rect.rotation.z)
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
            _rect.DOPivot(DoPivotEndValue, DoPivotDuration);
        }

        void FadeOutIn()
        {
            Debug.Log("Fade...");
            DOTween.Sequence()
            .Append(_image.DOFade(DoFadeOutEndPoint, DoFadeDuration).SetAutoKill(false))
            .Append(_image.DOFade(DoFadeInEndPoint, DoFadeDuration).SetAutoKill(false));
        }

        void ColourChange()
        {
            Debug.Log("ColourChange...");
            // Animate the circle outline's color and fillAmount
            _image.DOColor(DoColourStartingColour, DoColourDuration).SetEase(_moveEase);
            _image.DOFillAmount(DOFillAmountEndValue, DOFillAmountDuration).SetEase(_moveEase).SetLoops(-1, _loopType)
                .OnStepComplete(() =>
                {
                    _image.fillClockwise = !_image.fillClockwise;
                    _image.DOColor(DoColourEndingColour, DoColourDuration).SetEase(_moveEase);
                });
        }

        private void Pulse()
        {
            Debug.Log("Pulse...");
            transform.DOScale(DoPulseEndValue, DoPulseDuration)
                .SetLoops(DoPulseLoopCycles, _loopType)
                .SetEase(_moveEase);
        }
    }
}
