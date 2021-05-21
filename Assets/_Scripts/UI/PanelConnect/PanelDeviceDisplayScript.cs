using Plugins.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PanelConnect
{
    public class PanelDeviceDisplayScript : MonoBehaviour
    {
        private BluetoothDeviceModel _model;

        public string DisplayName
        {
            get
            {
                if (_model.name.Contains("Shimmer3-")) return _model.name.Split('-')[1];
                return _model.name;
            }
        }

        public Button BtnSelectDevice;
        public Text TxtDeviceName;

        // Start is called before the first frame update
        void Start()
        {
            BtnSelectDevice.onClick.AddListener(BtnSelect_onClick);
        }

        public void SetBluetoothDeviceModel(BluetoothDeviceModel d)
        {
            this._model = d;
            BtnSelectDevice.GetComponentInChildren<Text>().text = DisplayName;
        }

        private void BtnSelect_onClick()
        {
            // Set the selected device to this one - PanelSelectedDeviceScript
            Object.FindObjectOfType<PanelSelectedDeviceScript>().SetBluetoothDeviceModel(this._model);
        }
    }
}