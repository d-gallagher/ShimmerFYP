using UI.Menus;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Z_ToBeRemoved
{
    public class MainMenuScript : MonoBehaviour
    {
        public Button BtnBluetoothPanelToggle;
        public GameObject BluetoothPanel;

        private PanelSlideScript _bluetoothPanelScript;
        private MenuPanelOpenClose settingsPanel;

        // Start is called before the first frame update
        void Start()
        {
            _bluetoothPanelScript = BluetoothPanel.GetComponent<PanelSlideScript>();
            BtnBluetoothPanelToggle.onClick.AddListener(BluetoothPanelToggle);
        }

        private void BluetoothPanelToggle()
        {
            //BluetoothPanel.SetActive(!BluetoothPanel.activeSelf);
        }
    }
}
