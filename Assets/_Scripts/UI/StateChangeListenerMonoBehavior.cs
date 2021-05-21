using Controllers;
using ShimmerRT;
using UI;
using UnityEngine;
using static Enums;

public abstract class StateChangeListenerMonoBehavior : MonoBehaviour
{
    public MenuPanelType PanelType;

    #region ShimmerStateChangedListener
    protected bool _shimmerStateChanged;
    protected ShimmerState _shimmerState;
    protected void OnShimmerStateChanged(ShimmerState state)
    {
        _shimmerStateChanged = true;
        _shimmerState = state;
    }
    #endregion

    #region ApplicationStateChangedListener
    protected bool _ApplicationStateChanged;
    protected ApplicationState _ApplicationState;
    protected void OnApplicationStateChanged(ApplicationState state)
    {
        _ApplicationStateChanged = true;
        _ApplicationState = state;
    }
    #endregion

    #region Unity
    /// <summary>
    /// Set up listener(s)
    /// </summary>
    protected void OnAwake()
    {
        // Add a listener to the Action on the UI Manager Script.
        UIControllerScript.ShimmerStateChangedAction += OnShimmerStateChanged;
        UIControllerScript.ApplicationStateChangedAction += OnApplicationStateChanged;
    }
    #endregion
}