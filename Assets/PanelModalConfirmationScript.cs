using UI.Menus;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PanelModalConfirmationScript : MonoBehaviour
{
    private PanelSlideScript _panelSlideScript;

    public Button BtnYes;
    public Button BtnNo;
    public Text TxtBody;

    private UnityAction _onConfirm;
    private UnityAction _onDeny;

    // Start is called before the first frame update
    void Start()
    {
        _panelSlideScript = GetComponent<PanelSlideScript>();
        BtnYes.onClick.AddListener(YesClicked);
        BtnNo.onClick.AddListener(NoClicked);

        TxtBody.text = @"
            Playback data already exists, if you choose Yes, this data will be replaced with new data...
        ";
    }

    public void SetAndEnable(string msg, UnityAction onConfirm, UnityAction onDeny)
    {
        _onConfirm = onConfirm;
        _onDeny = onDeny;
        TxtBody.text = msg;
        _panelSlideScript.SetPanelState();
    }

    private void YesClicked()
    {
        _onConfirm();
        Close();
    }

    private void NoClicked()
    {
        _onDeny();
        Close();
    }

    private void Close()
    {
        _panelSlideScript.SetPanelState();
    }

}
