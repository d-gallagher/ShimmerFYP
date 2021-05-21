using Controllers;
using System.Collections.Generic;
using UI.Menus;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class PanelNotificationScript : MonoBehaviour
{
    [Header("UI Elements")]
    public Text LblMessage;
    public Text LblTimer;
    public Text LblQueueCount;

    [Header("Private variables")]
    [SerializeField]
    private float _notificationDuration = 3.0f;
    private float _timer;
    private Queue<string> _messageQueue;
    private PanelSlideScript _panelSlideScript;

    #region OnNotificationReceived
    protected void OnNotificationReceievd(string message)
    {
        // Add the new notification to the queue
        AddNotification(message);
    }
    #endregion

    /// <summary>
    /// Add a notification to the queue
    /// </summary>
    /// <param name="notification">Notifcation to add to queue</param>
    public void AddNotification(string notification)
    {
        if (notification != null && notification.Length > 0)
        {
            _messageQueue.Enqueue(notification);
        }
    }

    #region Unity Methods
    void Start()
    {
        _messageQueue = new Queue<string>();

        _panelSlideScript = GetComponent<PanelSlideScript>();
        _panelSlideScript.SetPanelState(PanelSlideState.CLOSED);

        // Add a listener to receive notifications.
        UIControllerScript.OnNotificationReceivedAction += OnNotificationReceievd;
    }

    private void Update()
    {
        // still time on timer, subract delta time
        if (_timer > 0) _timer -= Time.deltaTime;
        // no currently displaying message, message in queue
        else if (_messageQueue.Count > 0)
        {
            string msg = _messageQueue.Dequeue();
            LblMessage.text = msg;
            _timer += _notificationDuration;

            _panelSlideScript.SetPanelState(PanelSlideState.OPEN);
        }
        else _panelSlideScript.SetPanelState(PanelSlideState.CLOSED);

        SetTimerAndQueueCount(); // For debugging
    }
    #endregion

    /// <summary>
    /// For debugging - These may not always be present so handling here.
    /// </summary>
    private void SetTimerAndQueueCount()
    {
        if (LblQueueCount != null) LblQueueCount.text = _messageQueue.Count.ToString(); // Debug
        if (LblTimer != null) LblTimer.text = _timer.ToString();  // Debug
    }
}
