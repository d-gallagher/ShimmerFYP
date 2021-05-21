using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PanelPlayback
{
    public class PanelPlaybackStatusScript : StateChangeListenerMonoBehavior
    {
        public Sprite SpritePlaybackEnabled;
        public Sprite SpritePlaybackDisabled;

        private Image _imgConnectionStatus;

        private void Awake()
        {
            UIControllerScript.ApplicationStateChangedAction += OnApplicationStateChanged;
        }

        private void Start()
        {
        }

        private void Update()
        {

        }
    }
}