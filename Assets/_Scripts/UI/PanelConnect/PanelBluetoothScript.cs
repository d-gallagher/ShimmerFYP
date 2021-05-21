using Plugins;
using Plugins.Listeners;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PanelConnect
{ 
    public class PanelBluetoothScript : MonoBehaviour, IBluetoothListenable
    {
        // Reference to the active plugin.
        public IShimmerPlugin m_bluetoothPlugin;
        private bool _allowNativeBluetooth;

        // Enable or Disable the device's Bluetooth Adapter.
        // NOTE: This panel will be disabled if bluetooth is not natively
        // supported, e.g. Windows
        [Header("Bluetooth Enable/Disable")]
        public GameObject PanelBluetoothEnableDisable;
        public GameObject LedBluetoothStatus;
        public Button BtnEnableDisable;
        public Text TxtBluetoothStatus;
        private Image ledBluetoothStatus;

        // Start is called before the first frame update
        void Start()
        {
            // Get a reference to the active plugin from the PluginManager.
            m_bluetoothPlugin = FindObjectOfType<PluginManagerScript>().GetPlugin();
            _allowNativeBluetooth = m_bluetoothPlugin.AllowEnableDisableBluetooth();

            // Set up any handlers...
            // Is bluetooth supported?
            BtnEnableDisable.onClick.AddListener(m_bluetoothPlugin.EnableDisableBluetooth);
            PanelBluetoothEnableDisable.SetActive(_allowNativeBluetooth);

            // Get a reference to the LED Status Light
            ledBluetoothStatus = LedBluetoothStatus.GetComponentInChildren<Image>();

            // Begin discovery on the plugin.
            //m_bluetoothPlugin.StartDiscovery();
        }

        public void GetBluetoothEnabled()
        {
            if (m_bluetoothPlugin.AllowEnableDisableBluetooth())
            {
                SetBluetoothState(m_bluetoothPlugin.IsBluetoothEnabled());
            }
        }

        public void SetBluetoothState(bool isEnabled)
        {
            string statusDisplayText = isEnabled ? "Enabled" : "Disabled";
            ledBluetoothStatus.color = isEnabled ? Color.green : Color.red;
            TxtBluetoothStatus.text = statusDisplayText;
        }
    }
}