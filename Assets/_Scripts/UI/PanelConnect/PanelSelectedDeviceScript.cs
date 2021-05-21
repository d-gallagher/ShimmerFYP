using Plugins;
using Plugins.Models;
using ShimmerRT;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PanelConnect
{
    public class PanelSelectedDeviceScript : StateChangeListenerMonoBehavior
    {
        private UnityShimmerDeviceScript _unityShimmerDeviceScript;
        private BluetoothDeviceModel _model;
        private bool _modelChanged;

        #region UI
        public Button BtnConnect;
        public Text TxtDeviceName;
        #endregion

        #region Unity
        private void Awake()
        {
            OnAwake();
        }
        // Start is called before the first frame update
        private void Start()
        {
            _unityShimmerDeviceScript = GameObject.Find("UnityShimmerDevice").GetComponent<UnityShimmerDeviceScript>();
            BtnConnect.onClick.AddListener(BtnConnect_onClick);

            BtnConnect.interactable = false;
            TxtDeviceName.text = "Please select a device";
        }

        private void Update()
        {
            // No device selected
            if (_modelChanged)
            {
                TxtDeviceName.text = "Connect to " + _model.name;
                BtnConnect.interactable = true;
                _modelChanged = false;
            }

            else if (_shimmerStateChanged)
            {
                BtnConnect.onClick.RemoveAllListeners();

                if (_shimmerState == ShimmerState.NONE)
                {
                    // Allow connection to selected.
                    BtnConnect.onClick.AddListener(BtnConnect_onClick);
                    BtnConnect.interactable = true;
                    TxtDeviceName.text = "Connect to " + _model.name;
                }
                else if (_shimmerState == ShimmerState.CONNECTING)
                {
                    // Don't allow any action - must wait...!?
                    BtnConnect.interactable = false;
                    TxtDeviceName.text = "Connecting to " + _model.name;
                }
                else
                {
                    // Allow Disconnection from connected.
                    BtnConnect.onClick.AddListener(BtnDisconnect_onClick);
                    BtnConnect.interactable = true;
                    TxtDeviceName.text = "Disconnect from " + _model.name;
                }
                _shimmerStateChanged = false;
            }

        }
        #endregion

        private void EnableConnectControls(bool isEnabled)
        {

        }

        public void SetBluetoothDeviceModel(BluetoothDeviceModel d)
        {
            this._model = d;
            _modelChanged = true;
        }

        private void BtnConnect_onClick()
        {
            _unityShimmerDeviceScript.SetAndConnectToDevice(_model);
        }
        private void BtnDisconnect_onClick()
        {
            _unityShimmerDeviceScript.StopStreaming();
            _unityShimmerDeviceScript.Disconnect();
        }
    }
}