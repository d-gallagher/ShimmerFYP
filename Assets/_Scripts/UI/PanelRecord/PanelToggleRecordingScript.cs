using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.PanelRecord
{
    /// <summary>
    /// Toggle the record button between StartRecord and StopRecord
    /// </summary>
    public class PanelToggleRecordingScript : MonoBehaviour
    {
        public Sprite SpriteStartRecord;
        public Sprite SpriteStopRecord;

        private Button BtnToggle;
        private Text TxtLabel;
        private Image _btnImage;

        private void Start()
        {
            BtnToggle = GetComponentInChildren<Button>();
            TxtLabel = GetComponentInChildren<Text>();
            _btnImage = BtnToggle.GetComponentInChildren<Image>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecord">If true, currently setting to StartRecord</param>
        /// <param name="action"></param>
        public void SetButtonState(bool isRecord, UnityAction action)
        {
            BtnToggle.onClick.RemoveAllListeners();
            BtnToggle.onClick.AddListener(action);
            _btnImage.sprite = isRecord ? SpriteStartRecord : SpriteStopRecord;
            TxtLabel.text = isRecord ? "Start" : "Stop";
        }

        public void SetButtonEnabled(bool isEnabled) => BtnToggle.interactable = isEnabled;
    }
}