using System.Collections.Generic;
using System.Linq;
using UI.Animations;
using UI.Menus;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public enum MenuPanelType
{
    CONNECT,
    PLAYBACK,
    RECORD,
    PLAYERS,
    FEEDBACK
}


class PanelButtonPair
{
    public GameObject Panel { get; set; }
    public Button Button { get; set; }
    public PanelSlideScript SlideScript { get; set; }
    public PulseAnimationScript ButtonPulseAnimationScript { get; set; }
    public bool IsActive => SlideScript.SlideState == PanelSlideState.OPEN;
}

public class MenuPanelController : MonoBehaviour
{
    [Header("Connect")]
    public GameObject PanelConnect;
    public Button BtnConnect;
    [Header("Playback")]
    public GameObject PanelPlayback;
    public Button BtnPlayback;
    [Header("Record")]
    public GameObject PanelRecord;
    public Button BtnRecord;
    [Header("Players")]
    public GameObject PanelPlayers;
    public Button BtnPlayers;
    [Header("Feedback")]
    public GameObject PanelFeedback;
    public Button BtnFeedback;

    private Dictionary<MenuPanelType, PanelButtonPair> _panelDict;
    

    #region Unity Methods
    private void Start()
    {
        BtnConnect.onClick.AddListener(delegate { UpdatePanelStates(MenuPanelType.CONNECT); });
        BtnRecord.onClick.AddListener(delegate { UpdatePanelStates(MenuPanelType.RECORD); });
        BtnPlayback.onClick.AddListener(delegate { UpdatePanelStates(MenuPanelType.PLAYBACK); });
        BtnPlayers.onClick.AddListener(delegate { UpdatePanelStates(MenuPanelType.PLAYERS); });
        BtnFeedback.onClick.AddListener(delegate { UpdatePanelStates(MenuPanelType.FEEDBACK); });
        InitPanels();
    }
    #endregion

    private void UpdatePanelStates(MenuPanelType menuPanelType)
    {
        var requestedPanelPair = _panelDict[menuPanelType];
        // if this panel is already enabled, just disable it.
        if (requestedPanelPair.IsActive)
        {
            requestedPanelPair.SlideScript.SetPanelState(PanelSlideState.CLOSED);
            requestedPanelPair.ButtonPulseAnimationScript.IsAnimating = false;
        }
        else
        {
            EnableSinglePanel(menuPanelType);
        }

    }

    /// <summary>
    /// Enable the panel of menuPanelType and disable ALL other MenuPanels.
    /// </summary>
    /// <param name="menuPanelType"></param>
    public void EnableSinglePanel(MenuPanelType menuPanelType)
    {
        var panelToEnable = _panelDict[menuPanelType];

        // Disable all other panels.
        _panelDict.Where(x => x.Key != menuPanelType)
            .Select(x => x.Value)
            .ToList()
            .ForEach(x =>
            {
                x.SlideScript.SetPanelState(PanelSlideState.CLOSED);
                x.ButtonPulseAnimationScript.IsAnimating = false;
            });
        // Enable the panel of type.
        panelToEnable.SlideScript.SetPanelState(PanelSlideState.OPEN);
        panelToEnable.ButtonPulseAnimationScript.IsAnimating = true;
    }

    #region Populate Dictionary with Panels
    private void InitPanels()
    {
        _panelDict = new Dictionary<MenuPanelType, PanelButtonPair>();
        // Connect Panel
        _panelDict.Add(MenuPanelType.CONNECT, new PanelButtonPair
        {
            Panel = PanelConnect,
            Button = BtnConnect
        });

        // Playback Panel
        _panelDict.Add(MenuPanelType.PLAYBACK, new PanelButtonPair
        {
            Panel = PanelPlayback,
            Button = BtnPlayback
        });

        // Record Panel
        _panelDict.Add(MenuPanelType.RECORD, new PanelButtonPair
        {
            Panel = PanelRecord,
            Button = BtnRecord
        });

        // Players Panel
        _panelDict.Add(MenuPanelType.PLAYERS, new PanelButtonPair
        {
            Panel = PanelPlayers,
            Button = BtnPlayers
        });

        // Feedback Panel
        _panelDict.Add(MenuPanelType.FEEDBACK, new PanelButtonPair
        {
            Panel = PanelFeedback,
            Button = BtnFeedback
        });

        _panelDict.Values
            .ToList()
            .ForEach(x =>
            {
                x.SlideScript = x.Panel.GetComponent<PanelSlideScript>();
                x.ButtonPulseAnimationScript = x.Button.GetComponent<PulseAnimationScript>();
            });
    }
    #endregion
}
