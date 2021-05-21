using UnityEngine;
using UnityEngine.UI;

namespace UI.Menus
{
    /// <summary>
    /// Sub class of PanelSlideScript, can have a toggle state button attached.
    /// </summary>
    public class PanelSlideWithButtonScript : PanelSlideScript
    {
        [Header("PanelSlideWithButtonScript")]
        public Button BtnToggle;

        protected new void Start()
        {
            base.Start();
            // Not all panels will have a button attached, e.g. Notification panel.
            if (BtnToggle != null) BtnToggle.onClick.AddListener(TogglePanel);
        }

        public void TogglePanel()
        {
            SetPanelState();
        }
    }
}
