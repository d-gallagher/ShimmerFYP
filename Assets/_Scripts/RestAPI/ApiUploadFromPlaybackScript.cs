using Controllers;
using Models;
using Newtonsoft.Json;
using RestAPI;
using UnityEngine;
using UnityEngine.UI;

public class ApiUploadFromPlaybackScript : MonoBehaviour
{
    public Button BtnApiUploadFromPlayback;
    private PlaybackControllerScript _playbackControllerScript;
    private UnityPlayerModel _selectedPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _playbackControllerScript = FindObjectOfType<PlaybackControllerScript>();

        BtnApiUploadFromPlayback.onClick.AddListener(UploadFromPlayback);

        UIControllerScript.SelectedPlayerChangedAction += OnPlayerSelected;
    }

    private void OnPlayerSelected(UnityPlayerModel p)
    {
        Debug.Log(p.FirstName + " selected");
        _selectedPlayer = p;
    }

    private void UploadFromPlayback()
    {
        var models = _playbackControllerScript.GetData();
        Debug.Log("MODELS: " + models.Count);

        //var json = JsonConvert.SerializeObject(models);
        //var json = CustomJsonSerializer.ToJsonList(models);
        var json = JsonUtility.ToJson(models);
        Debug.Log(json);

        StartCoroutine(RestService.CreatePlayerTrainingRecord(_selectedPlayer.Id, json, onSuccess, onError));
    }

    private void onSuccess(string obj)
    {
        Debug.Log(obj);
    }

    private void onError(string obj)
    {
        Debug.Log(obj);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
