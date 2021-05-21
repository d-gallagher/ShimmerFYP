namespace Plugins.Listeners
{
    public interface IDeviceDiscoveredListener
    {
        void ReceiveBluetoothDeviceListJson(string json);
    }
}
