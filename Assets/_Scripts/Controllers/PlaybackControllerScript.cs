using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using static Enums;
using System.Linq;
using Models;

namespace Controllers
{
    public class PlaybackControllerScript : MonoBehaviour
    {
        // References
        private ShimmerOrientationScript _shimmerOrientationScript;

        #region Public Variables
        [Header("Public")]
        public GameObject Target;
        public bool IsPlaying;
        public PlaybackDirection PlayDirection;

        [SerializeField]
        private int _currentFrameIndex;
        public int CurrentFrameIndex
        {
            get => _currentFrameIndex;
            set
            {
                if (_currentFrameIndex != value)
                {
                    _currentFrameIndex = value;

                    // Trigger this action.

                    //// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    //      !!!! UNCOMMENT THIS IF IT IS COMMENTED !!!!
                    //// vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
                    //// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                    PlaybackFrameChangedAction(_currentFrameIndex);

                    //// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    //      !!!! DEFINITELY UNCOMMENT IT !!!!
                    /////// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                }
            }
        }

        private PlaybackDataType _currentPlaybackDataType;
        public PlaybackDataType PlaybackDataType => _currentPlaybackDataType;
        #endregion

        #region Properties
        public bool HasData => _playbackList.Count > 0;
        #endregion

        #region Private Serialised Variables
        [Header("Private Serialized")]
        [SerializeField]
        private float _startTime;
        [SerializeField]
        private float _endTime;
        [SerializeField]
        private float _length;
        [SerializeField]
        private float _currentTimestamp;
        [SerializeField]
        private float _nextTimestamp;
        [SerializeField]
        private float _timeToNext;
        [SerializeField]
        private float _playTime;
        #endregion

        #region Private Variables
        private List<UnityShimmerDataModel> _playbackList;
        // This Action will be triggered when the CurrentFrameIndex property changes.
        public static Action<int> PlaybackFrameChangedAction;
        #endregion

        #region Unity Methods
        void Start()
        {
            _shimmerOrientationScript = Target.GetComponent<ShimmerOrientationScript>();

            _playbackList = new List<UnityShimmerDataModel>();
        }

        private void Update()
        {
            if (IsPlaying)
            {
                // If there is data in the playback list...
                if (_playbackList != null && _playbackList.Count > 0)
                {
                    // Should the next model be applied?
                    if (_timeToNext <= 0)
                    {
                        IsPlaying = ChangeFrame(PlayDirection);
                    }
                    _timeToNext -= Time.deltaTime;
                }
                _playTime += Time.deltaTime;
            }
            else
            {
                //Debug.Log("Nothing to play");
            }
        }
        #endregion

        /// <summary>
        /// Load Json data from a file
        /// </summary>
        /// <param name="filePath">Path of file to read.</param>
        /// <returns>True if file data loaded and deserialised.</returns>
        public bool LoadData(string filePath)
        {
            // Try to get the JSON data from the file...
            string fileData;
            try
            {
                fileData = FileHelper.GetFileData(filePath);
            }
            catch (FileNotFoundException e)
            {
                Debug.Log("COULD NOT LOAD DATA FROM SPECIFIED FILE PATH");
                Debug.Log(e.Message);
                return false;
            }

            List<UnityShimmerDataModel> shimmerDataModels;
            try
            {
                // Convert the JSON file data to a List
                shimmerDataModels = JsonConvert.DeserializeObject<List<UnityShimmerDataModel>>(fileData);
            }
            catch (JsonReaderException e)
            {
                Debug.Log("COULD NOT PARSE SHIMMER DATA FROM JSON");
                Debug.Log(e.Message);
                return false;
            }
            SetData(shimmerDataModels, PlaybackDataType.LOADED);
            return true;
        }

        /// <summary>
        /// Temp method for testing data returned from API.
        /// </summary>
        /// <param name="data"></param>
        public void SetData(List<UnityShimmerDataModel> data, PlaybackDataType playbackDataType)
        {
            _playbackList = data;
            PrintDataModelsPerSecond();
            _currentPlaybackDataType = playbackDataType;
            CurrentFrameIndex = 0;
            // Hack
            ChangeFrame(PlaybackDirection.FORWARD);
            ChangeFrame(PlaybackDirection.BACKWARD);
            UIControllerScript.PlaybackDataChangedAction();
        }

        /// <summary>
        /// Apply a frame to the model in a given direction.
        /// </summary>
        /// <param name="index">Index of the frame in the playbackList</param>
        /// <param name="direction">Direction to move (1 or -1)</param>
        /// <returns>True if a movement was applied.</returns>
        public bool ChangeFrame(PlaybackDirection direction = PlaybackDirection.FORWARD)
        {
            // Get the increment acccording to the Enum value (1 or -1)
            int increment = (int)direction;
            // Calculate the next index according to the Enum value.
            int nextIndex = CurrentFrameIndex + increment;
            // Check that the index is within bounds.
            if (
                increment > 0 && nextIndex < _playbackList.Count
                || increment < 0 && nextIndex >= 0
                )
            {
                // Get the model at this index.
                var curr = _playbackList[CurrentFrameIndex];
                // Set the timestamp.
                _currentTimestamp = curr.T;
                // Apply the model.
                _shimmerOrientationScript.AddDataModel(curr);
                // Set the next timestamp
                _nextTimestamp = _playbackList[nextIndex].T;
                // And set the time to the next model to be applied.
                _timeToNext = Mathf.Abs((_nextTimestamp - _currentTimestamp) / 1000);

                CurrentFrameIndex = nextIndex;

                return true;
            }
            return false;
        }

        public List<UnityShimmerDataModel> GetData()
        {
            // return a copy of the playback list.
            return new List<UnityShimmerDataModel>(_playbackList);
        }

        private void PrintDataModelsPerSecond()
        {
            List<float> diffs = new List<float>();
            for(int i = 1; i < _playbackList.Count; i++)
            {
                diffs.Add(_playbackList[i].T - _playbackList[i - 1].T);
            }
            Debug.Log("Average diff: " + diffs.Average());
        }
    }
}