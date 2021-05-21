using Assets._Scripts.Feedback;
using Controllers;
using Models;
using ShimmerRT;
using ShimmerRT.models;
using System;
using UnityEngine;

namespace Plugins.Listeners
{
    /// <summary>
    /// Receive Data from Shimmer via C# as a ShimmerDataModel or via Java as Json.
    /// 
    /// The object whose trasform is to be manipulated by the data
    /// should be assigned to TargetTransform.
    /// </summary>
    public class ShimmerDataListenerScript : MonoBehaviour, IShimmerHandler
    {
        private UIControllerScript _uiManagerScript;

        // TODO: Add timeout if Device turned off or connection lost...?

        // Object to apply the rotation to.
        public GameObject Target;
        // Get a reference to this script from the above object.
        private ShimmerOrientationScript _shimmerOrientationScript;
        // Use this to record DataModels to a List
        private RecordingControllerScript _dataRecorderScript;
        // Impact Detection Script
        private ImpactDetectionScript _impactDetectionScript;

        private void Start()
        {
            // Setup references.
            _uiManagerScript = FindObjectOfType<UIControllerScript>();
            _shimmerOrientationScript = Target.GetComponent<ShimmerOrientationScript>();
            _dataRecorderScript = FindObjectOfType<RecordingControllerScript>();
            _impactDetectionScript = FindObjectOfType<ImpactDetectionScript>();
        }

        #region Implementation of IShimmerHandler - Handle Sensor Data

        /// <summary>
        /// Callback used by the C# ShimmerRT library to pass data from the Shimmer to Unity.
        /// </summary>
        /// <param name="model">Data model containing sensor values from the Shimmer device.</param>
        public void HandleShimmerModel(ShimmerDataModel model)
        {
            var unityModel = new UnityShimmerDataModel(model);
            // Apply the data model to the GameObject.
            _shimmerOrientationScript.AddDataModel(unityModel);
            // Store it in the record list if recording is enabled.
            if (_dataRecorderScript.IsRecording) _dataRecorderScript.RecordDataModel(unityModel);
            if(_impactDetectionScript!=null) _impactDetectionScript.AddModel(model);
        }

        /// <summary>
        /// Method called by Java to return data from the Shimmer.
        /// </summary>
        /// <param name="json">JSON string containing sensor values from the Shimmer device.</param>
        public void HandleShimmerJson(string json)
        {
            // Deserialise the json to a ShimmerDataModel.
            var model = JavaJsonDeserializer.FromJson<ShimmerDataModel>(json);
            HandleShimmerModel(model);
            // Send the model to the correct script.
            //_shimmerOrientationScript.AddDataModel(model);
        }
        #endregion

        #region Implementation of IShimmerHandler - Handle State Changes, Notifications, etc
        /// <summary>
        /// Java Callback used to handle Shimmer device state changes, e.g. Connect, Disconnect, etc
        /// </summary>
        /// <param name="state"></param>
        public void HandleShimmerStateChanged(string state)
        {
            ShimmerState? receivedState = null;
            switch (state)
            {
                case "NONE":
                    receivedState = ShimmerState.NONE;
                    break;
                case "CONNECTING":
                    receivedState = ShimmerState.CONNECTING;
                    break;
                case "CONNECTED":
                    receivedState = ShimmerState.CONNECTED;
                    break;
                case "STREAMING":
                    receivedState = ShimmerState.STREAMING;
                    break;
            }
            if (receivedState != null) HandleShimmerStateChanged(receivedState);
        }
        /// <summary>
        /// C# Callback used to handle Shimmer device state changes, e.g. Connect, Disconnect, etc
        /// </summary>
        /// <param name="state"></param>
        public void HandleShimmerStateChanged(ShimmerState? state)
        {
            if (state != null) _uiManagerScript.HandleShimmerStateChanged(state.Value);
        }

        public void HandleNotificationMessage()
        {
            throw new NotImplementedException();
        }

        public void HandleReceptionRatePacket()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
