using UnityEngine;
using DG.Tweening;
using static Enums;

namespace UI.Menus
{
    /// <summary>
    /// Base class for slideable UI element.
    /// 
    /// SetPanelState() should be used 
    /// </summary>
    public class PanelSlideScript : MonoBehaviour
    {
        // Start is called before the first frame update
        [Header("PanelSlideScript")]
        public Vector2 OpenPosition;
        public Vector2 ClosedPosition;
        public float Speed = 0.2f;

        public PanelSlideState SlideState = PanelSlideState.CLOSED;
        public bool IsOpen => SlideState == PanelSlideState.OPEN;

        protected RectTransform _rt;

        protected virtual void Start()
        {
            _rt = GetComponent<RectTransform>();
        }

        /// <summary>
        /// Change the current state of the panel and begin the tween.
        /// 
        /// If no argument is given, states will be toggled.
        /// </summary>
        /// <param name="newState"></param>
        public void SetPanelState(PanelSlideState? newState = null)
        {
            // Was newState passed in, if so, set the state.
            if (newState.HasValue) SlideState = newState.Value;
            // No state passed in, just toggle the current state.
            else SlideState = (SlideState == PanelSlideState.OPEN) ? PanelSlideState.CLOSED : PanelSlideState.OPEN;
            DoTween();
        }

        protected void DoTween()
        {
            Vector2 position = SlideState == PanelSlideState.OPEN ? OpenPosition : ClosedPosition;
            _rt.DOAnchorPos(position, Speed);
        }

    }
}
