using Models;
using SimpleFileBrowser;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static Enums;

namespace Controllers
{
    public class RecordingControllerScript : MonoBehaviour
    {
        private PlaybackControllerScript _playbackControllerScript;

        [SerializeField]
        private bool _isRecording;
        public bool IsRecording => _isRecording;
        public bool HasData => _recordList != null && _recordList.Count > 0;

        [SerializeField]
        private List<UnityShimmerDataModel> _recordList;
        public List<UnityShimmerDataModel> GetData => _recordList;

        private void Start()
        {
            _playbackControllerScript = FindObjectOfType<PlaybackControllerScript>();
        }

        public void StartRecording()
        {
            _recordList = new List<UnityShimmerDataModel>();
            _isRecording = true;
        }

        public void StopRecording()
        {
            _isRecording = false;
            // Store the recorded data as playback data
            _playbackControllerScript.SetData(_recordList, PlaybackDataType.RECORDED);
        }


        public void RecordDataModel(UnityShimmerDataModel m)
        {
            _recordList.Add(m);
        }

        public bool SaveRecordedData(string filePath, string fileName)
        {
            //var fileName = "test-output.json";
            //var fullFilePath = filePath + fileName;
            //var fullFilePath = Application.persistentDataPath + "\\" + fileName;
            var fullFilePath = filePath + "/" + fileName;

            if (_recordList != null)
            {
                if (_recordList.Count > 0 && fullFilePath.Length != 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("[");
                    foreach (var m in _recordList)
                    {
                        sb.Append(JsonUtility.ToJson(m));
                        sb.Append(",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("]");

                    try
                    {
                        //File.WriteAllText(fullFilePath, JsonConvert.SerializeObject(_recordList));
                        FileBrowserHelpers.WriteTextToFile(fullFilePath, sb.ToString());
                        Debug.Log("File Saved as: " + fullFilePath);
                        return true;
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                    }

                }
            }
            return false;
        }
    }
}
