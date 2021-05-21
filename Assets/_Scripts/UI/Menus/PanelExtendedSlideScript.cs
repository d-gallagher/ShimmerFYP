using UnityEngine;
using UnityEngine.UI;

namespace UI.Menus
{
    /// <summary>
    /// Sub class of PanelSlideScript, can have a toggle state button attached.
    /// 
    /// DO NOT SET CLOSED POSITION ON THIS SCRIPT!!!
    /// </summary>
    public class PanelExtendedSlideScript : PanelSlideScript
    {
        private PanelSlideScript _panelSlideScript;

        [Header("PanelSlideWithButtonScript")]
        public Button BtnToggle;

        protected new void Start()
        {
            base.Start();
            _panelSlideScript = GetComponent<PanelSlideScript>();
            if (_panelSlideScript != null)
            {
                ClosedPosition = _panelSlideScript.OpenPosition;
            }

            // Not all panels will have a button attached, e.g. Notification panel.
            if (BtnToggle != null) BtnToggle.onClick.AddListener(TogglePanel);
        }

        public void TogglePanel()
        {
            SetPanelState();
            BtnToggle.GetComponentInChildren<Text>().text = IsOpen ? "- Info" : "+ Info";
        }
    }
}