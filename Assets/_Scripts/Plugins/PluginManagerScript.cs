using Plugins.Android;
using Plugins.Windows;
using UnityEngine;

namespace Plugins
{
    public class PluginManagerScript : MonoBehaviour
    {
        public GameObject AndroidPlugin;
        public GameObject WindowsPlugin;

        private IShimmerPlugin _activePlugin;

        void Awake()
        {

            // if editor, always load windows code
#if UNITY_EDITOR
            // load windows code
            Debug.Log("IN EDITOR");
            LoadWindowsPlugin();
#else

        // not in editor - load appropriate code
#if UNITY_STANDALONE_WIN
        Debug.Log("IN WINDOWS");

        LoadWindowsPlugin();
        Debug.Log(_activePlugin.ToString());

#elif UNITY_ANDROID
        Debug.Log("IN ANDROID");
        LoadAndroidPlugin();
#endif

#endif
        }

        private void LoadWindowsPlugin()
        {
            var plugin = GameObject.Instantiate(WindowsPlugin, transform);
            _activePlugin = plugin.GetComponent<WindowsPluginScript>();
        }

        private void LoadAndroidPlugin()
        {
            var plugin = Instantiate(AndroidPlugin, transform);
            _activePlugin = plugin.GetComponent<AndroidPluginScript>();
        }

        public IShimmerPlugin GetPlugin()
        {
            return this._activePlugin;
        }
    }
}