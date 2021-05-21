using Controllers;
using Models;
using RestAPI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

namespace UI.PanelPlayers
{
    public class PanelTrainingRecordPrefabScript : MonoBehaviour
    {
        private PlaybackControllerScript _playbackControllerScript;
        private RecordingControllerScript _recordingControllerScript;

        private UnityTrainingRecord _model;

        public Text TxtId;
        public Text TxtCreatedAt;
        public Button BtnGetTrainingRecordData;

        private void Start()
        {
            _playbackControllerScript = FindObjectOfType<PlaybackControllerScript>();
            _recordingControllerScript = FindObjectOfType<RecordingControllerScript>();
        }

        public void SetTrainingRecord(UnityTrainingRecord trainingRecord)
        {
            _model = trainingRecord;
            TxtId.text = trainingRecord.Id.ToString();
            TxtCreatedAt.text = trainingRecord.CreatedAt.ToString();
            BtnGetTrainingRecordData.onClick.AddListener(GetTrainingRecordData);
        }

        #region ShimmerData
        private void GetTrainingRecordData()
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
                    GetData,
                    () => UIControllerScript.SendNotification("Action Cancelled"));
            }
            else
            {
                GetData();
            }
        }

        private void GetData()
        {
            StartCoroutine(RestService.GetTrainingRecordData(_model.Id, GetTrainingRecordDataRequestCompleted, GetTrainingRecordDataRequestError));
        }

        private void GetTrainingRecordDataRequestCompleted(List<UnityShimmerDataModel> trainingRecordData)
        {
            Debug.Log($"GOT {trainingRecordData.Count} records");
            PlaybackControllerScript playbackControllerScript = FindObjectOfType<PlaybackControllerScript>();
            playbackControllerScript.SetData(trainingRecordData, PlaybackDataType.LOADED);
            UIControllerScript.RequestPanelEnable(MenuPanelType.PLAYBACK);
        }
        private void GetTrainingRecordDataRequestError(string msg)
        {
            Debug.Log(msg);
        }
        #endregion
    }
}