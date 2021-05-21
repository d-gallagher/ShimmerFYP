using Controllers;
using ShimmerRT;
using System;
using UI.Menus;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

namespace UI.PanelPlayback
{
    public class PanelPlaybackScript : StateChangeListenerMonoBehavior
    {
        private PlaybackControllerScript _playbackManagerScript;
        private string _selectedPath;

        #region UI Elements
        //[Header("Sub Panels")]
        public Button BtnMoreInfo;

        [Header("Playback Controls")]
        public Button BtnRewindToStart;
        public Button BtnRewindStep;
        public Button BtnPlayForwards;
        public Button BtnPause;
        public Button BtnFfwdStep;
        public Button BtnPlayBackwards;

        [Header("Playback File")]
        public Button BtnLoadSession;
        public Text TxtFilePath;
        [Header("Playback Feedback")]
        public Text TxtPlaybackStatusText;
        public Image PlaybackStatusColor;
        // Not sure if you want these...?
        public Text TxtMinFrame;
        public Text TxtMaxFrame;
        public Text TxtCurrentFrameIndex;
        public Slider SldPlaybackControl;
        #endregion


        // Slide Panel for More Info
        private PanelSlideScript _panelSlideScript;
        private bool _moreInfo = false;
        private Vector2 _originalPanelOpenPos;
        private Vector2 _originalPanelClosedPos;
        private Vector2 _moreInfoPanelOpenPos;
        private Vector2 _moreInfoPanelClosedPos;


        #region Unity
        private void Awake()
        {
            OnAwake();
            // Add a listener to the Action on the Playback Script.
            UIControllerScript.PlaybackDataChangedAction += OnPlaybackDataChanged;
            PlaybackControllerScript.PlaybackFrameChangedAction += OnPlaybackFrameChanged;
        }

        private void OnPlaybackDataChanged()
        {
            SldPlaybackControl.minValue = 0;
            SldPlaybackControl.maxValue = _playbackManagerScript.GetData().Count;
        }

        private void Start()
        {
            // For more info button
            _panelSlideScript = GetComponentInChildren<PanelSlideScript>();
            _originalPanelOpenPos = _panelSlideScript.OpenPosition;
            _originalPanelClosedPos = _panelSlideScript.ClosedPosition;
            BtnMoreInfo.onClick.AddListener(Moreinfo);

            _playbackManagerScript = FindObjectOfType<PlaybackControllerScript>();

            BtnLoadSession.onClick.AddListener(SelectSessionToLoad);
            BtnRewindToStart.onClick.AddListener(RewindToStart);
            //BtnFfwdToEnd.onClick.AddListener();


            BtnRewindStep.onClick.AddListener(FrameBackwards);
            BtnPlayBackwards.onClick.AddListener(Reverse);
            BtnPlayForwards.onClick.AddListener(Play);
            BtnPause.onClick.AddListener(Pause);
            BtnFfwdStep.onClick.AddListener(FrameForwards);

            // Reference to playback status display
            //PlaybackStatusColor = this.GetComponentInChildren<Image>();
            //_playbackStatusText = PnlStatus.GetComponentInChildren<Text>();

            SldPlaybackControl.minValue = 0;
            SldPlaybackControl.maxValue = 0;
            SldPlaybackControl.onValueChanged.AddListener(SliderValueChanged);
        }

        private void Update()
        {
            if (_shimmerStateChanged)
            {
                if (_shimmerState == ShimmerState.STREAMING)
                {
                    // TODO: Display some text to inform the user that they should 
                    // disable streaming before attempting any playback...?

                    // Disable Playback while streaming is active
                    EnableFileControls(false);
                    EnablePlaybackControls(false);
                }
                else
                {
                    EnableFileControls(true);
                }
                _shimmerStateChanged = false;
            }
            else
            {
                EnablePlaybackControls(_playbackManagerScript.HasData);
            }
            SetPlaybackStatus();

        }
        #endregion

        private void EnablePlaybackControls(bool isEnabled)
        {
            BtnRewindToStart.interactable = isEnabled;
            BtnRewindStep.interactable = isEnabled;
            BtnPlayForwards.interactable = isEnabled;
            BtnPause.interactable = isEnabled;
            BtnFfwdStep.interactable = isEnabled;
            BtnPlayBackwards.interactable = isEnabled;
        }

        private void EnableFileControls(bool isEnabled)
        {
            BtnLoadSession.interactable = isEnabled;
        }

        /// <summary>
        /// Slide panel in to display further info about recording panel using the Info button.
        /// </summary>
        void Moreinfo()
        {
            // Swap Open/Closed Positions for more info
            _moreInfoPanelClosedPos = _panelSlideScript.OpenPosition;
            _moreInfoPanelOpenPos = new Vector2(380, -2450);

            // Switch on/off more info and change text on info button
            _moreInfo = !_moreInfo;
            BtnMoreInfo.GetComponentInChildren<Text>().text = _moreInfo ? "- Info" : "+ Info";

            if (_moreInfo)
            {
                _panelSlideScript.ClosedPosition = _moreInfoPanelClosedPos;
                _panelSlideScript.OpenPosition = _moreInfoPanelOpenPos;
                _panelSlideScript.SetPanelState(PanelSlideState.CLOSED);
                //_panelSlideScript.TogglePanel();
            }
            else
            {
                _panelSlideScript.ClosedPosition = _originalPanelClosedPos;
                _panelSlideScript.OpenPosition = _originalPanelOpenPos;
                _panelSlideScript.SetPanelState(PanelSlideState.CLOSED);
                //_panelSlideScript.TogglePanel();
            }
        }



        #region Load Data
        private void SelectSessionToLoad()
        {
            SimpleFileBrowser.FileBrowser.ShowLoadDialog((path) => { OnSuccess(path); }, () => { Debug.Log("CANCELLED"); },
            false, null, "Session To Load", "Select");
        }

        private void OnSuccess(string path)
        {
            _selectedPath = path;
            TxtFilePath.text = _selectedPath;
            Debug.Log("SELECTED: " + path);

            if (_playbackManagerScript.LoadData(path))
            {
                // Data Loaded Successfully, allow playback
                //Debug.Log("PLAYBACK DATA LOADED");
                // Maybe load the first frame here??
                UIControllerScript.SendNotification("Playback Data Loaded");
            }
            else
            {
                // Something went wrong with loading from the file, disable playback
                //Debug.Log("PLAYBACK DATA NOT LOADED");
                UIControllerScript.SendNotification("Playback Data Failed to Load");
            }

        }
        #endregion

        #region Playback Controls - Justin (from PlaybackScript)

        public void SetPlaybackStatus()
        {
            string statusDisplayText = _playbackManagerScript.IsPlaying ? "Enabled" : "Disabled";
            PlaybackStatusColor.color = _playbackManagerScript.IsPlaying ? Color.green : Color.red;
            TxtPlaybackStatusText.text = statusDisplayText;
        }

        private void RewindToStart()
        {
            _playbackManagerScript.PlayDirection = PlaybackDirection.FORWARD;
            // Another hack...
            _playbackManagerScript.ChangeFrame(PlaybackDirection.FORWARD);
            _playbackManagerScript.ChangeFrame(PlaybackDirection.BACKWARD);
            _playbackManagerScript.CurrentFrameIndex = 0;
        }

        private void PlaybackFromStart()
        {
            _playbackManagerScript.PlayDirection = PlaybackDirection.FORWARD;
            _playbackManagerScript.CurrentFrameIndex = 0;
            _playbackManagerScript.IsPlaying = true;
        }

        private void Stop()
        {
            _playbackManagerScript.IsPlaying = false;
            _playbackManagerScript.PlayDirection = PlaybackDirection.FORWARD;
            _playbackManagerScript.CurrentFrameIndex = 0;
        }

        private void Play()
        {
            _playbackManagerScript.PlayDirection = PlaybackDirection.FORWARD;
            _playbackManagerScript.IsPlaying = true;
        }

        private void Pause()
        {
            _playbackManagerScript.IsPlaying = false;
        }

        private void Reverse()
        {
            _playbackManagerScript.PlayDirection = PlaybackDirection.BACKWARD;
            _playbackManagerScript.IsPlaying = true;
        }

        private void FrameForwards()
        {
            _playbackManagerScript.IsPlaying = false;
            _playbackManagerScript.ChangeFrame(PlaybackDirection.FORWARD);
        }


        private void FrameBackwards()
        {
            _playbackManagerScript.IsPlaying = false;
            _playbackManagerScript.ChangeFrame(PlaybackDirection.BACKWARD);
        }

        private void SliderValueChanged(float arg0)
        {
            _playbackManagerScript.IsPlaying = false;
            _playbackManagerScript.CurrentFrameIndex = (int)arg0;
            _playbackManagerScript.ChangeFrame(PlaybackDirection.FORWARD);
        }

        public void OnPlaybackFrameChanged(int frameIndex)
        {
            SldPlaybackControl.SetValueWithoutNotify(frameIndex);
            TxtCurrentFrameIndex.text = frameIndex.ToString();
        }
        #endregion
    }
}
