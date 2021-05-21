using Controllers;
using Plugins;
using ShimmerRT;
using UnityEngine;
using UnityEngine.UI;

public class PanelStreamScript : StateChangeListenerMonoBehavior
{
    private UnityShimmerDeviceScript _unityShimmerDeviceScript;

    #region UI
    public Button BtnStartStream;
    private Text _buttonText;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        BtnStartStream.interactable = false;
        _unityShimmerDeviceScript = GameObject.Find("UnityShimmerDevice").GetComponent<UnityShimmerDeviceScript>();
        _buttonText = BtnStartStream.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        // Only update the UI if the state has changed
        if (_shimmerStateChanged)
        {
            //BtnStartStream.onClick.RemoveAllListeners();
            BtnStartStream.interactable = false;
            string msg = "";
            Color color = Color.red;
            if (_shimmerState == ShimmerState.NONE)
            {
                // State is either NONE or CONNECTING...
                // Shimmer is already streaming...
                msg = "Disconnected";
                color = Color.red;
            }
            else if (_shimmerState == ShimmerState.CONNECTED)
            {
                // Shimmer is connected and ready for streaming...
                msg = "Starting Stream...";
                color = Color.yellow;
                // Start streaming...
                _unityShimmerDeviceScript.StartStreaming();
            }
            else if (_shimmerState == ShimmerState.CONNECTING)
            {
                // Shimmer is connecting and ready for streaming...
                msg = "Connecting...";
                color = Color.yellow;
            }
            else if (_shimmerState == ShimmerState.STREAMING)
            {
                msg = "Streaming";
                color = Color.green;
                UIControllerScript.RequestPanelEnable(MenuPanelType.RECORD);
            }
            _buttonText.text = msg;
            _buttonText.color = color;

            _shimmerStateChanged = false;
        }
    }
    #endregion
}
