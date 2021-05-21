using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RestAPI;
using UI.PanelPlayers;
using static Enums;
using Controllers;
using Models;

public class APITestScript : MonoBehaviour
{
    public Button BtnSendRequest;
    // Dropdown to populate.
    public Dropdown playersDropdown;
    // Panel to update on Dropdown ValueChanged.
    public GameObject PanelPlayerDetails;

    // List of players retrieved from the API.
    private List<UnityPlayerModel> _players;
    // Reference to the script on the PlayerPanelDetails.
    private PanelSelectedPlayerScript _playerPanelScript;
    private UnityPlayerModel _selectedPlayer;

    public Button BtnGetTrainingRecords;
    public GameObject SvTrainingRecordsContent;
    public GameObject PanelTrainingRecordPrefab;
    private PanelTrainingRecordPrefabScript _trainingRecordDisplayScript;

    public Button BtnUploadJson;

    public Button BtnTestShimmerData;

    [Header("IDs")]
    public int trainingRecordId;

    private void Start()
    {
        // Attach the handler to the Send Request button.
        BtnSendRequest.onClick.AddListener(GetPlayers);
        // Get the script from the PlayerPanel so the Selected Player can be updated.
        _playerPanelScript = PanelPlayerDetails.GetComponent<PanelSelectedPlayerScript>();
        // Add on ValueChanged handler to Dropdown.
        playersDropdown.onValueChanged.AddListener(DdlPlayersDropdown_Changed);

        // Try to get the players now...
        GetPlayers();


        BtnGetTrainingRecords.onClick.AddListener(GetPlayerTrainingRecords);
        BtnUploadJson.onClick.AddListener(UploadJsonTest);
        BtnTestShimmerData.onClick.AddListener(GetTrainingRecordData);
    }

    public bool IsInternetAvailable()
    {
        return (Application.internetReachability != NetworkReachability.NotReachable);
    }

    #region GET Methods

    #region Players
    private void GetPlayers()
    {
        //StartCoroutine(RestService.Get<List<PlayerModel>>("/players", GetPlayersRequestCompleted, GetPlayersRequestError));
        StartCoroutine(RestService.GetPlayers(GetPlayersRequestCompleted, GetPlayersRequestError));
    }

    private void GetPlayersRequestCompleted(List<UnityPlayerModel> players)
    {
        // Use the result.
        _players = players;
        RefreshDropdown();
    }

    private void GetPlayersRequestError(string msg)
    {
        Debug.Log(msg);
    }
    #endregion

    #region Training Records
    private void GetPlayerTrainingRecords()
    {
        StartCoroutine(RestService.GetPlayerTrainingRecords(_selectedPlayer.Id, GetPlayerTrainingRecordsRequestCompleted, GetPlayerTrainingRecordsRequestError));
    }

    private void GetPlayerTrainingRecordsRequestCompleted(List<UnityTrainingRecord> trainingRecords)
    {
        // Remove all child gameObjects from the Content
        foreach (Transform t in SvTrainingRecordsContent.transform)
        {
            Destroy(t.gameObject);
        }
        foreach (var t in trainingRecords)
        {
            // Add a prefab to the scroll view...
            var prefab = GameObject.Instantiate(PanelTrainingRecordPrefab, SvTrainingRecordsContent.transform);
            var script = prefab.GetComponent<PanelTrainingRecordPrefabScript>();
            script.SetTrainingRecord(t);
        }
    }
    private void GetPlayerTrainingRecordsRequestError(string msg)
    {
        Debug.Log(msg);
    }
    #endregion

    #region ShimmerData
    private void GetTrainingRecordData()
    {
        Debug.Log("Internet Connection: " + IsInternetAvailable());
        StartCoroutine(RestService.GetTrainingRecordData(trainingRecordId, GetTrainingRecordDataRequestCompleted, GetTrainingRecordDataRequestError));
    }

    private void GetTrainingRecordDataRequestCompleted(List<UnityShimmerDataModel> trainingRecordData)
    {
        Debug.Log($"GOT {trainingRecordData.Count} records");
        PlaybackControllerScript playbackManagerScript = FindObjectOfType<PlaybackControllerScript>();
        playbackManagerScript.SetData(trainingRecordData, PlaybackDataType.LOADED);
    }
    private void GetTrainingRecordDataRequestError(string msg)
    {
        Debug.Log(msg);
    }
    #endregion

    #endregion // GET

    #region POST Methods
    // POST Json
    private void UploadJsonTest()
    {
        if (_selectedPlayer != null)
        {
            string json = FileHelper.GetFileData("TestDataFile.json");
            Debug.Log("STARTING JSON POST");
            //StartCoroutine(RestService.PostJson("/players/1/training-records", json, OnJsonPostSuccess, OnPostJsonError));
            StartCoroutine(RestService.CreatePlayerTrainingRecord(_selectedPlayer.Id, json, OnJsonPostSuccess, OnPostJsonError));

        }
        else
        {
            Debug.Log("NO PLAYER SELECTED");
        }


    }

    public void OnJsonPostSuccess(string msg)
    {
        Debug.Log(msg);
    }

    public void OnPostJsonError(string msg)
    {
        Debug.Log(msg);
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
        var selectedIndex = playersDropdown.value;
        // Player in dropdown will have same index (-1) as Player in _players,
        // since the Please Select Option is also added.
        _selectedPlayer = _players[selectedIndex - 1];
        // Set the panel's player to the selected player.
        _playerPanelScript.SetPlayer(_selectedPlayer);
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
            playersDropdown.ClearOptions();
            playersDropdown.AddOptions(dropdownValues);
        }
        else
        {
            playersDropdown.ClearOptions();
            dropdownValues.Add("Noo Data to Display");
        }
    }
    #endregion
}
