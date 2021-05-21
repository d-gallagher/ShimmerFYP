using Plugins.Models;
using UnityEngine;

namespace Plugins
{
    public class UnityShimmerDeviceScript : MonoBehaviour
    {
        private IShimmerPlugin _plugin;

        private BluetoothDeviceModel _device;

        public void SetAndConnectToDevice(BluetoothDeviceModel device)
        {
            _device = device;
            _plugin.ConnectToShimmer(_device.address);
        }

        private void Start()
        {
            _plugin = FindObjectOfType<PluginManagerScript>().GetPlugin();
        }

        public void Connect()
        {
            _plugin.ConnectToShimmer(_device.address);
        }

        public void Disconnect()
        {
            _plugin.DisconnectShimmer();
        }

        public void StartStreaming()
        {
            _plugin.StartStreaming();
        }

        public void StopStreaming()
        {
            _plugin.StopStreaming();
        }
    }
}
