using Controllers;
using Models;
using RestAPI;
using System.Collections.Generic;
using UI.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PanelPlayers
{
    public class PanelSelectPlayer : MonoBehaviour
    {
        private List<UnityPlayerModel> _players;
        private UnityPlayerModel _selectedPlayer;

        private PanelSelectedPlayerScript _panelSelectedPlayerScript;
        private RotateAnimationScript _rotateGetPlayers;

        [Header("UI Elements")]
        public Button BtnGetPlayers;
        public Dropdown DdSelectPlayer;

        #region Unity
        void Start()
        {

            _panelSelectedPlayerScript = FindObjectOfType<PanelSelectedPlayerScript>();
            _rotateGetPlayers = BtnGetPlayers.GetComponent<RotateAnimationScript>();

            BtnGetPlayers.onClick.AddListener(GetPlayers);
            DdSelectPlayer.onValueChanged.AddListener(DdlPlayersDropdown_Changed);

            GetPlayers();
        }
        #endregion

        #region Get Players
        private void GetPlayers()
        {
            // Animate the button
            _rotateGetPlayers.IsAnimating = true;
            // Clear the dropdown and add a loading value...
            DdSelectPlayer.ClearOptions();
            DdSelectPlayer.AddOptions(new List<string> { "Loading..." });

            //StartCoroutine(RestService.Get<List<PlayerModel>>("/players", GetPlayersRequestCompleted, GetPlayersRequestError));
            StartCoroutine(RestService.GetPlayers(GetPlayersRequestCompleted, GetPlayersRequestError));
        }

        private void GetPlayersRequestCompleted(List<UnityPlayerModel> players)
        {
            // Use the result.
            _players = players;
            RefreshDropdown();
            _rotateGetPlayers.IsAnimating = false;
        }

        private void GetPlayersRequestError(string msg)
        {
            Debug.Log(msg);
            _rotateGetPlayers.IsAnimating = false;
        }
        #endregion

        #region UI Methods
        /// <summary>
        /// On Dropdown Value changed
        /// </summary>
        /// <param name="arg0"></param>
        private void DdlPlayersDropdown_Changed(int arg0)
        {
            // Get index of selected Dropdown item.
            var selectedIndex = DdSelectPlayer.value;
            // Player in dropdown will have same index (-1) as Player in _players,
            // since the Please Select Option is also added.
            _selectedPlayer = _players[selectedIndex - 1];
            // Set the panel's player to the selected player.
            _panelSelectedPlayerScript.SetPlayer(_selectedPlayer);
            UIControllerScript.SetSelectedPlayer(_selectedPlayer);
        }

        private void RefreshDropdown()
        {
            // Create a new List to store player display values.
            List<string> dropdownValues = new List<string>();
            if (_players.Count > 0)
            {
                // First add the Select Option text
                dropdownValues.Add("Please Select...");

                // Add an entry for each player in the List.
                foreach (var p in _players)
                {
                    // Add the display value for this player to the Dropdown.
                    dropdownValues.Add(p.FirstName + " - " + p.Email);
                }
                // Clear and repopulate the Dropdown with List.
                DdSelectPlayer.ClearOptions();
                DdSelectPlayer.AddOptions(dropdownValues);
            }
            else
            {
                DdSelectPlayer.ClearOptions();
                dropdownValues.Add("No Data to Display");
            }
        }
        #endregion
    }
}