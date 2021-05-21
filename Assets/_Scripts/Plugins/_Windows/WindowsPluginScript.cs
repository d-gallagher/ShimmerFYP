using Plugins.Listeners;
using Plugins.Models;
using ShimmerRT;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.Windows
{
    public class WindowsPluginScript : MonoBehaviour, IShimmerPlugin, IDeviceDiscoveredListener
    {
        private List<BluetoothDeviceModel> _discoveredDevices;

        private ShimmerControllerRT _shimmerController;
        private ShimmerDataListenerScript _dataListenerScript;

        private WindowsDeviceDiscoveryRunner _discoveryProcessRunner;

        void Awake()
        {
            Debug.Log("CREATED WINDOWS PLUGIN");
            //_discovery = GetComponent<WindowsDeviceDiscoveryScript>();
            _discoveredDevices = new List<BluetoothDeviceModel>();
            _discoveryProcessRunner = new WindowsDeviceDiscoveryRunner(5000, this);

            _dataListenerScript = FindObjectOfType<ShimmerDataListenerScript>();
        }

        #region Shimmer Device methods
        public void ConnectToShimmer(string hardwareID)
        {
            _shimmerController = new ShimmerControllerRT(_dataListenerScript);
            _shimmerController.Connect(hardwareID);
        }

        public void DisconnectShimmer()
        {
            _shimmerController.Disconnect();
        }

        public void StartStreaming()
        {
            _shimmerController.StartStream();
        }

        public void StopStreaming()
        {
            _shimmerController.StopStream();
        }
        #endregion

        #region Bluetooth methods
        public bool AllowEnableDisableBluetooth()
        {
            return false;
        }

        public void EnableDisableBluetooth()
        {
            throw new System.NotImplementedException();
        }

        public bool IsBluetoothEnabled()
        {
            throw new System.NotImplementedException();
        }

        public List<BluetoothDeviceModel> GetBondedDevices()
        {
            StartDiscovery();
            return _discoveredDevices;
        }

        public void StartDiscovery()
        {
            _discoveryProcessRunner.Run();
        }

        public List<BluetoothDeviceModel> GetDiscoveredDevices()
        {
            return new List<BluetoothDeviceModel>();
        }

        public void ReceiveBluetoothDeviceListJson(string json)
        {
            _discoveredDevices = JavaJsonDeserializer.FromJson<List<BluetoothDeviceModel>>(json);
        }
        #endregion
    }
}
