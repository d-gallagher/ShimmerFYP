using UI.PanelConnect;
using UnityEngine;

namespace Plugins.Listeners
{
    /// <summary>
    /// Listens for Bluetooth Notifications from Java code and calls
    /// methods on registered receivers.
    /// </summary>
    public class BluetoothListenerScript : MonoBehaviour
    {
        // Register any receivers to be notified here.
        private IBluetoothListenable receiever_connectionPanel;

        private void Start()
        {
            receiever_connectionPanel = GameObject.Find("PanelConnect").GetComponentInChildren<PanelBluetoothScript>();
        }

        public void ReceiveBluetoothStateChanged(string isEnabled)
        {
            // Call a method on the receiver.
            receiever_connectionPanel.SetBluetoothState(bool.Parse(isEnabled));
        }
    }
}