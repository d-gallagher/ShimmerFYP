using System;

namespace Plugins.Models
{
    /// <summary>
    /// POCO class to represent a Bluetooth device in the application.
    /// 
    /// This class will have an identical counterpart in Java so if one is changed,
    /// so too should the other.
    /// </summary>
    [Serializable]
    public class BluetoothDeviceModel
    {
        public string name;
        public string address;

        public BluetoothDeviceModel(string name, string address)
        {
            this.name = name;
            this.address = address;
        }
    }
}