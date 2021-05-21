using Plugins;
using Plugins.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PanelConnect
{
    public class PanelSelectDeviceScript : MonoBehaviour
    {
        [Header("Devices")]
        // Update the list of devices.
        public Button BtnRefreshPairedDevices;
        // Object to add DevicePanelPrefabs to.
        public GameObject DeviceListContent;
        // Object representing a Device in the UI.
        public GameObject DevicePanelPrefab;
        private IShimmerPlugin m_bluetoothPlugin;
        private Dictionary<string, BluetoothDeviceModel> _devices;

        private void Start()
        {
            // Get a reference to the active plugin from the PluginManager.
            m_bluetoothPlugin = FindObjectOfType<PluginManagerScript>().GetPlugin();
            m_bluetoothPlugin.StartDiscovery();

            // Initialise the device dictionary
            _devices = new Dictionary<string, BluetoothDeviceModel>();

            BtnRefreshPairedDevices.onClick.AddListener(RefreshPairedDevices);
        }

        public void RefreshPairedDevices()
        {
            // Get any currently paired devices...
            var bondedDevices = m_bluetoothPlugin.GetBondedDevices();

            // Remove old devices from list
            _devices.Clear();
            // Remove all child gameObjects from the ContentDeviceList
            foreach (Transform t in DeviceListContent.transform)
            {
                Destroy(t.gameObject);
            }

            // Add each new device to the local dictionary...
            foreach (var d in bondedDevices) _devices.Add(d.address, d);

            // Add a panel to the list for each device - filter to only include Shimmer3 devices!?
            //foreach (var d in _devices.Values.Where(x => x.name.Contains("Shimmer3-")))
            foreach (var d in _devices.Values)
            {
                var devicePanel = Instantiate(DevicePanelPrefab, DeviceListContent.transform, false);
                var deviceScript = devicePanel.GetComponent<PanelDeviceDisplayScript>();
                // Note that the model is set on the panel script, rather than directly
                // setting the text values.
                deviceScript.SetBluetoothDeviceModel(d);
            }
        }
    }
}