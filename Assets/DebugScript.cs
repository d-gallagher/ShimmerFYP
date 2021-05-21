using Controllers;
using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{
    public Button BtnTestModal;

    // Start is called before the first frame update
    void Start()
    {
        BtnTestModal.onClick.AddListener(OnClickedTest);
    }

    private void OnClickedTest()
    {
        UIControllerScript.RequestModal("FROM DEBUG TEST", onConfirm, onDeny);
    }

    private void onConfirm()
    {
        Debug.Log("CONFIRMED");
    }

    private void onDeny()
    {
        Debug.Log("DENIED");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
