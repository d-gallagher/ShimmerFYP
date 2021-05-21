using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    public class RotateAnimationScript : BaseUIAnimationBehaviour
    {
        [Header("Rotation Settings")]
        [SerializeField]
        private Vector3 DoRotateEndValue = new Vector3(0, 0, 180);
        [SerializeField]
        private float DoRotateDuration = 1;

        [Header("Rotation Mode")]
        [SerializeField]
        private RotateMode _rotateMode = RotateMode.FastBeyond360;

        protected override void Start()
        {
            base.Start();
        }

        protected override void Animate()
        {
            _rect.DORotate(DoRotateEndValue, DoRotateDuration, _rotateMode)
                .SetEase(_moveEase)
                .OnComplete(() =>
                {
                    DoRotateEndValue.z *= -1;
                    _isAnimationComplete = true;
                });
        }
    }
}