using Models;
using ShimmerRT;
using System;
using UnityEngine;
using UnityEngine.Events;
using static Enums;

namespace Controllers
{
    public class UIControllerScript : MonoBehaviour
    {
        public GameObject PanelModal;
        private static PanelModalConfirmationScript _panelModalConfirmationScript;

        private void Start()
        {
            menuPanelController = FindObjectOfType<MenuPanelController>();
            _panelModalConfirmationScript = PanelModal.GetComponent<PanelModalConfirmationScript>();
        }

        private static MenuPanelController menuPanelController;
        public static void RequestPanelEnable(MenuPanelType panelType)
        {
            menuPanelController.EnableSinglePanel(panelType);
        }

        #region ShimmerStateChanged
        // Action to be fired when ShimmerState Changes
        public static Action<ShimmerState> ShimmerStateChangedAction;
        // Method called on receiving ShimmerState from Shimmer device.
        public void HandleShimmerStateChanged(ShimmerState? state)
        {
            if (state != null)
            {
                // Trigger the action so any listeners can react.
                ShimmerStateChangedAction(state.Value);
                // Send a notification to any listeners.
                SendNotification($"Shimmer State: {state.Value.EnumValue()}");
            }

            #region Currently Unused
            //switch (state)
            //{
            //    case ShimmerState.NONE:
            //        Debug.Log(msg + "NONE");
            //        break;

            //    case ShimmerState.CONNECTING:
            //        Debug.Log(msg + "CONNECTING");
            //        break;

            //    case ShimmerState.CONNECTED:
            //        Debug.Log(msg + "CONNECTED");
            //        break;

            //    case ShimmerState.STREAMING:
            //        Debug.Log(msg + "STREAMING");

            //        // This is still valid if needed...
            //        _panelStreamScript.SetStreaming(true);
            //        break;

            //    default:
            //        break;
            //}
            #endregion
        }
        #endregion

        #region ApplicationStateChanged
        public static Action<ApplicationState> ApplicationStateChangedAction;
        public void HandleApplicationStateChanged(ApplicationState state)
        {
            ApplicationStateChangedAction(state);
        }
        #endregion

        #region SelectedPlayerChanged
        public static Action<UnityPlayerModel> SelectedPlayerChangedAction;
        public static void SetSelectedPlayer(UnityPlayerModel selectedPlayer)
        {
            SelectedPlayerChangedAction(selectedPlayer);
        }
        #endregion

        #region OnNotificationReceived
        public static Action<string> OnNotificationReceivedAction;
        public static void SendNotification(string message)
        {
            OnNotificationReceivedAction(message);
        }
        #endregion

        #region PlaybackDataChanged
        public static Action PlaybackDataChangedAction;

        public static void PlaybackDataChanged()
        {
            PlaybackDataChangedAction();
        }
        #endregion

        //public static Action<string, Action, Action> RequestModalAction;
        public static void RequestModal(string msg, UnityAction onConfirm, UnityAction onDeny)
        {
            Debug.Log("UI CONTROLLER: " + msg);
            _panelModalConfirmationScript.SetAndEnable(msg, onConfirm, onDeny);
        }
    }
}