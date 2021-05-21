using Controllers;
using Models;
using RestAPI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PanelRecord
{
    public class PanelRecordScript : StateChangeListenerMonoBehavior
    {
        private RecordingControllerScript _recordingControllerScript;
        private PlaybackControllerScript _playbackControllerScript;

        private string _selectedFilePath;

        private UnityPlayerModel _selectedPlayer;

        #region UI Elements
        [Header("Sub Panels")]
        public GameObject PanelRecordingStatus;
        private PanelRecordingStatusScript _panelRecordingStatusScript;
        private PanelToggleRecordingScript _panelToggleRecordingScript;
        [Header("UI Elements")]
        public Button BtnSelectSaveLocation;
        public Button BtnSaveRecordedData;
        public Text TxtFile;
        public Button BtnUploadData;
        #endregion

        #region Unity Methods
        private void Awake() => base.OnAwake();

        private void Start()
        {
            // Subsribe to the player changed event
            UIControllerScript.SelectedPlayerChangedAction += OnSelectedPlayerChanged;
            
            // Get references to Controller scripts
            _recordingControllerScript = FindObjectOfType<RecordingControllerScript>();
            _playbackControllerScript = FindObjectOfType<PlaybackControllerScript>();

            // Slide Scripts
            //_panelSlideScript = GetComponentInChildren<PanelSlideScript>();

            // Sub Panels
            _panelToggleRecordingScript = GetComponentInChildren<PanelToggleRecordingScript>();
            // Set the initial state of the recording button, i.e. StartRecord
            _panelToggleRecordingScript.SetButtonState(true, StartRecord_Clicked);

            // Listeners
            BtnSaveRecordedData.onClick.AddListener(SaveRecordedData);
            BtnSelectSaveLocation.onClick.AddListener(SelectSaveLocation);
            BtnUploadData.onClick.AddListener(UploadJson);
            // Reference to update recording status UI
            _panelRecordingStatusScript = PanelRecordingStatus.GetComponent<PanelRecordingStatusScript>();

            // Disable all controls
            EnableControls(false);
            _panelToggleRecordingScript.SetButtonEnabled(false);
        }

        private void Update()
        {
            if (_shimmerStateChanged)
            {
                // Only allow recording if the state has changed to streaming
                _panelToggleRecordingScript.SetButtonEnabled(true);
            }
        }
        #endregion

        public void OnSelectedPlayerChanged(UnityPlayerModel selectedPlayer) => _selectedPlayer = selectedPlayer;

        private void EnableControls(bool isEnabled)
        {
            BtnSelectSaveLocation.interactable = isEnabled && _selectedPlayer != null;
            BtnSaveRecordedData.interactable = isEnabled && _selectedPlayer != null;
            BtnUploadData.interactable = isEnabled && _selectedPlayer != null;
        }

        public void StartRecord_Clicked()
        {
            // Only prompt for confirmation if data exists
            if (_recordingControllerScript.HasData || _playbackControllerScript.HasData)
            {
                string msg;
                if (_recordingControllerScript.HasData && _playbackControllerScript.HasData) msg = "This will overwrite existing recorded and playback data.";
                else
                {
                    if (_recordingControllerScript.HasData) msg = "This will overwrite current recorded data.";
                    else if (_playbackControllerScript.HasData) msg = "This will overwrite current playback data.";
                    else msg = "Something went wrong but assigning msg anyways :)";
                }

                UIControllerScript.RequestModal(
                    msg,
                    StartRecording,
                    () => UIControllerScript.SendNotification("Action Cancelled"));
            }
            else
            {
                StartRecording();
            }
        }

        public void StopRecord_Clicked()
        {
            _recordingControllerScript.StopRecording();
            _panelRecordingStatusScript.SetRecordingStatus(false);
            _panelToggleRecordingScript.SetButtonState(true, StartRecord_Clicked);
            // Enable controls
            EnableControls(true);
        }

        #region Recording Manager
        public void StartRecording()
        {
            _recordingControllerScript.StartRecording();
            _panelRecordingStatusScript.SetRecordingStatus(true);
            // Toggle the record button
            _panelToggleRecordingScript.SetButtonState(false, StopRecord_Clicked);
            // Disable all other controls while recording
            EnableControls(false);
        }

        #endregion

        #region Select Save Location
        /// <summary>
        /// Open a file browser to choose save location.
        /// </summary>
        private void SelectSaveLocation()
        {
            SimpleFileBrowser.FileBrowser.ShowSaveDialog(
                   (path) => { OnSuccess(path); },
                   () => { OnCancel(); },
                   true, null, "Select Folder", "Select");
        }

        /// <summary>
        /// Set the oath to the selected location.
        /// </summary>
        /// <param name="path"></param>
        public void OnSuccess(string path)
        {
            _selectedFilePath = path;
            TxtFile.text = _selectedFilePath;
            Debug.Log("SELECTED: " + path);
        }

        /// <summary>
        /// Do Nothing, action is cancelled.
        /// </summary>
        public void OnCancel()
        {
            Debug.Log("CANCELLED");
        }
        #endregion

        #region Save Recorded Data
        public void SaveRecordedData()
        {
            string filename = "test.json";
            if (_recordingControllerScript.SaveRecordedData(_selectedFilePath, filename))
            {
                Debug.Log("File Saved");
            }
            else
            {
                Debug.Log("Error Saving File");
            }
        }
        #endregion

        #region Upload Recorded Data
        // POST Json
        private void UploadJson()
        {
            if (_selectedPlayer != null)
            {
                //string json = FileHelper.GetFileData("TestDataFile.json");
                var data = _recordingControllerScript.GetData;
                Debug.Log("MODEL COUNT FOR UPLOAD: " + data.Count);

                //string json = JsonUtility.ToJson(_recordingControllerScript.GetData);
                //string json = CustomJsonSerializer.ToJsonList(data);
                string json = CustomJsonSerializer.ToJsonSB(data);


                Debug.Log(json);
                Debug.Log("STARTING JSON POST");
                //StartCoroutine(RestService.PostJson("/players/1/training-records", json, OnJsonPostSuccess, OnPostJsonError));
                StartCoroutine(RestService.CreatePlayerTrainingRecord(_selectedPlayer.Id, json, OnJsonPostSuccess, OnPostJsonError));

            }
            else
            {
                // This should not happen because of EnableControls()
                Debug.Log("NO PLAYER SELECTED");
            }
        }

        public void OnJsonPostSuccess(string msg)
        {
            Debug.Log(msg);
        }

        public void OnPostJsonError(string msg)
        {
            Debug.Log(msg);
        }

        #endregion
    }
}
