using Plugins;
using UnityEngine;

namespace UI.PanelConnect
{
    public class PanelConnectScript : StateChangeListenerMonoBehavior
    {
        // Reference to the active plugin.
        public IShimmerPlugin m_bluetoothPlugin;

        [Header("Bluetooth Enable/Disable")]
        public GameObject PanelBluetoothEnableDisable;

        private bool _allowNativeBluetooth;

        void Start()
        {
            // Get a reference to the active plugin from the PluginManager.
            m_bluetoothPlugin = FindObjectOfType<PluginManagerScript>().GetPlugin();
            _allowNativeBluetooth = m_bluetoothPlugin.AllowEnableDisableBluetooth();
            PanelBluetoothEnableDisable.SetActive(_allowNativeBluetooth);
        }
    }
}