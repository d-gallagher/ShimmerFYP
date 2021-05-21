using Plugins.Models;
using System.Collections.Generic;

namespace Plugins
{
    public interface IShimmerPlugin
    {
        #region Shimmer Device methods
        void ConnectToShimmer(string hardwareID);
        void DisconnectShimmer();
        void StartStreaming();
        void StopStreaming();
        #endregion

        #region Bluetooth methods
        bool AllowEnableDisableBluetooth();
        void EnableDisableBluetooth();
        bool IsBluetoothEnabled();

        List<BluetoothDeviceModel> GetBondedDevices();
        void StartDiscovery();
        List<BluetoothDeviceModel> GetDiscoveredDevices();
        #endregion
    }
}