using Models;
using RestAPI;
using System.Collections.Generic;
using UI.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PanelPlayers
{
    public class PanelPlayerTrainingRecordsScript : MonoBehaviour
    {
        private RotateAnimationScript _rotateAnimationScript;

        [Header("UI Elements")]
        public GameObject SvTrainingRecordsContent;
        public Button BtnRefresh;
        [Header("Prefab")]
        public GameObject PanelTrainingRecordPrefab;

        private int _playerId;

        // Start is called before the first frame update
        void Start()
        {
            BtnRefresh.interactable = false;
            BtnRefresh.onClick.AddListener(RefreshTrainingRecords);
            _rotateAnimationScript = BtnRefresh.GetComponent<RotateAnimationScript>();
        }

        public void SetPlayerId(int playerId)
        {
            _playerId = playerId;
            BtnRefresh.interactable = true;
            RefreshTrainingRecords();
        }

        private void RefreshTrainingRecords()
        {
            GetPlayerTrainingRecords(_playerId);
        }

        #region GET Player Training Records
        public void GetPlayerTrainingRecords(int playerId)
        {
            _rotateAnimationScript.IsAnimating = true;
            StartCoroutine(RestService.GetPlayerTrainingRecords(playerId, GetPlayerTrainingRecordsRequestCompleted, GetPlayerTrainingRecordsRequestError));
        }

        private void GetPlayerTrainingRecordsRequestCompleted(List<UnityTrainingRecord> trainingRecords)
        {
            // Remove all child gameObjects from the Content
            foreach (Transform t in SvTrainingRecordsContent.transform)
            {
                Destroy(t.gameObject);
            }
            foreach (var t in trainingRecords)
            {
                // Add a prefab to the scroll view...
                var prefab = GameObject.Instantiate(PanelTrainingRecordPrefab, SvTrainingRecordsContent.transform);
                var script = prefab.GetComponent<PanelTrainingRecordPrefabScript>();
                script.SetTrainingRecord(t);
            }
            _rotateAnimationScript.IsAnimating = false;
        }
        private void GetPlayerTrainingRecordsRequestError(string msg)
        {
            if (msg != null) Debug.Log(msg);
            _rotateAnimationScript.IsAnimating = false;
        }
        #endregion
    }
}