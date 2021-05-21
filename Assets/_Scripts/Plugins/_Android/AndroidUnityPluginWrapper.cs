using UnityEngine;

namespace Plugins.Android
{
    /// <summary>
    /// Singleton class which can be used to call methods in the Java Plugin.
    /// 
    /// </summary>
    public sealed class AndroidUnityPluginWrapper
    {
        private readonly AndroidJavaClass _activityClass;
        private readonly AndroidJavaObject _activityContext;

        public AndroidUnityPluginWrapper()
        {
            // First get the activity class, this will always be UnityPlayer.
            _activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

            // Get the context - this will be an instance of AndroidPlugin.java in the jar file
            // This is defined in the AndroidManifest.xml as:
            //    <activity android:name="com.example.unity_plugin.AndroidUnityPlugin"..../>
            _activityContext = _activityClass.GetStatic<AndroidJavaObject>("currentActivity");
        }

        #region Shimmer Device methods
        public void ConnectToShimmer(string hardwareID)
        {
            _activityContext.Call("connectToShimmer", hardwareID);
        }

        public void DisconnectShimmer()
        {
            _activityContext.Call("disconnect");
        }

        public void StartStreaming()
        {
            _activityContext.Call("startStreaming");
        }

        public void StopStreaming()
        {
            _activityContext.Call("stopStreaming");
        }
        #endregion

        #region Bluetooth methods
        public void EnableDisableBluetooth()
        {
            _activityContext.Call("toggleBluetooth");
        }

        public string IsBluetoothEnabled()
        {
            return _activityContext.Call<string>("isBluetoothEnabled");
        }

        public void StartDiscovery()
        {
            _activityContext.Call("startDiscovery");
        }

        public string GetDiscoveredDevicesJson()
        {
            return _activityContext.Call<string>("getDiscoveredDevicesJson");
        }

        public string GetBondedDevicesJson()
        {
            return _activityContext.Call<string>("getBondedDevicesJson");
        }
        #endregion
    }
}
