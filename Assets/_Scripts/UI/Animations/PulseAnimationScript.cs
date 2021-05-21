using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    public class PulseAnimationScript : BaseUIAnimationBehaviour
    {
        [Header("Settings")]
        [Tooltip("DoPulseEndValue is the size of the pulse after one cycle.")]
        public float PulseMaxValue = 1.5f;
        public float PulseDuration = 1;
        public int Loops = 2;
        [SerializeField]
        private LoopType _loopType = LoopType.Yoyo;

        protected override void Start()
        {
            base.Start();
        }

        protected override void Animate()
        {
            transform.DOScale(PulseMaxValue, PulseDuration)
                .SetLoops(Loops, _loopType)
                .SetEase(_moveEase)
                .OnComplete(() =>
                {
                    _isAnimationComplete = true;
                });
        }
    }
}
