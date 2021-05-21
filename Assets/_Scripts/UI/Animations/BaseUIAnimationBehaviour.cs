using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Animations
{
    public abstract class BaseUIAnimationBehaviour : MonoBehaviour
    {
        protected Image _image;
        protected RectTransform _rect;

        [Header("Base settings")]
        [SerializeField]
        public bool IsAnimating;
        [SerializeField]
        protected Ease _moveEase = Ease.Linear;

        protected bool _isAnimationComplete = true;

        protected virtual void Start()
        {
            _rect = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
        }

        protected virtual void Update()
        {
            if (IsAnimating && _isAnimationComplete)
            {
                _isAnimationComplete = false;
                Animate();
            }
        }

        protected virtual void OnAnimationComplete()
        {
            _isAnimationComplete = true;
        }

        /// <summary>
        /// Don't forget to set _isAnimationComplete where needed.
        /// 
        /// </summary>
        protected abstract void Animate();
    }
}
