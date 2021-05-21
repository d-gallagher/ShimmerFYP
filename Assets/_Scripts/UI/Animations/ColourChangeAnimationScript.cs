using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Animations
{
    public class ColourChangeAnimationScript : BaseUIAnimationBehaviour
    {
        [Header("ChangeColour Settings")]
        [Tooltip("Transition between 2 Colours (Set Transparency full or button disappears)")]
        public Color StartColor;
        public Color EndColor;

        public float Duration = 1f;
        public int Loops = 2;
        [SerializeField]
        private LoopType _loopType = LoopType.Yoyo;

        protected override void Animate()
        {
            _image = GetComponent<Image>();
            // Animate the circle outline's color and fillAmount
            _image.DOColor(EndColor, Duration)
                .SetLoops(Loops, _loopType)
                .SetEase(_moveEase)
                .OnComplete(() =>
                {
                    _isAnimationComplete = true;
                });
        }
    }
}
