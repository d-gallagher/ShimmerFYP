using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PanelRecord
{
    /// <summary>
    /// Only manages the Recording Status UI color and text display
    /// </summary>
    public class PanelRecordingStatusScript : StateChangeListenerMonoBehavior
    {
        public Sprite SpriteRecordingEnabled;
        public Sprite SpriteRecordingDisabled;

        private Image _imgConnectionStatus;
        // I think this is wrong and we want to use the OnApplicationStateChanged to grab the Recording enum.. wasn't certain..
        private RecordingControllerScript _recordingControllerScript;

        private void Awake()
        {
            UIControllerScript.ApplicationStateChangedAction += OnApplicationStateChanged;
        }

        private void Start()
        {
            _imgConnectionStatus = GetComponentInChildren<Button>().GetComponent<Image>();
        }

        private void Update()
        {
            //if (_recordingControllerScript.IsRecording)
            //{
            //    //set icon visible and pulse color (if possible.. alternatively we can maybe turn on/off the icons as the states become active)
            //}    
        }


        // ==== Old status code (still used but this panel and code will be removed after the new application state panel is finished) == 
        #region UI Elements
        [Header("UI Elements")]
        //public RectTransform PnlStatus;
        public Text TxtRecordingStatusText;
        public Image _recordingStatusColor;
        #endregion

        /// <summary>
        /// Update the color of the recording panel and text
        /// </summary>
        /// <param name="isRecording"></param>
        public void SetRecordingStatus(bool isRecording)
        {
            _recordingStatusColor.color = isRecording ? Color.green : Color.red;
            string statusDisplayText = isRecording ? "Enabled" : "Disabled";
            TxtRecordingStatusText.text = statusDisplayText;
            
        }
    }
}
