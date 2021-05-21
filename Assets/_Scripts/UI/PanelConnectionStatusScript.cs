using Controllers;
using ShimmerRT;
using UnityEngine;
using UnityEngine.UI;

public class PanelConnectionStatusScript : StateChangeListenerMonoBehavior
{
    public Sprite SpriteConnected;
    public Sprite SpriteDisconnected;

    private Image _imgConnectionStatus;

    // Start is called before the first frame update
    private void Awake()
    {
        UIControllerScript.ShimmerStateChangedAction += OnShimmerStateChanged;
    }

    void Start()
    {
        _imgConnectionStatus = GetComponentInChildren<Button>().GetComponent<Image>();

        // Set default
        _imgConnectionStatus.sprite = SpriteDisconnected;
    }

    private void Update()
    {
        if (_shimmerStateChanged)
        {
            switch (_shimmerState)
            {
                case ShimmerState.NONE:
                    _imgConnectionStatus.sprite = SpriteDisconnected;
                    _imgConnectionStatus.color = Color.red;
                    break;
                case ShimmerState.CONNECTING:
                    _imgConnectionStatus.sprite = SpriteDisconnected;
                    _imgConnectionStatus.color = Color.yellow;
                    break;
                case ShimmerState.CONNECTED:
                    _imgConnectionStatus.sprite = SpriteConnected;
                    _imgConnectionStatus.color = Color.green;
                    break;
                case ShimmerState.STREAMING:
                    _imgConnectionStatus.sprite = SpriteConnected;
                    _imgConnectionStatus.color = Color.black;
                    break;
                default:
                    break;
            }
        }
    }
}
