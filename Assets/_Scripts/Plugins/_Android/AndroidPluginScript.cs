using Plugins.Models;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.Android
{
    /// <summary>
    /// Behaviour to be placed on a GameObject to access Java Android code.
    /// </summary>
    public class AndroidPluginScript : MonoBehaviour, IShimmerPlugin
    {
        /// <summary>
        /// Wrapper object to access Java code easily.
        /// </summary>
        private AndroidUnityPluginWrapper m_wrapper;

        private void Awake()
        {
            // get the singleton
            //m_wrapper = AndroidUnityPluginWrapper.GetInstance();
            m_wrapper = new AndroidUnityPluginWrapper();
        }

        #region Shimmer Device methods
        public void ConnectToShimmer(string hardwareID)
        {
            m_wrapper.ConnectToShimmer(hardwareID);
        }

        public void DisconnectShimmer()
        {
            m_wrapper.DisconnectShimmer();
        }

        public void StartStreaming()
        {
            m_wrapper.StartStreaming();
        }

        public void StopStreaming()
        {
            m_wrapper.StopStreaming();
        }
        #endregion

        #region Bluetooth methods
        public bool AllowEnableDisableBluetooth()
        {
            return true;
        }

        public void EnableDisableBluetooth()
        {
            m_wrapper.EnableDisableBluetooth();
        }

        public bool IsBluetoothEnabled()
        {
            try
            {
                return bool.Parse(m_wrapper.IsBluetoothEnabled());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BluetoothDeviceModel> GetBondedDevices()
        {
            var json = m_wrapper.GetBondedDevicesJson();
            return JavaJsonDeserializer.FromJson<List<BluetoothDeviceModel>>(json);
        }

        public void StartDiscovery()
        {
            m_wrapper.StartDiscovery();
        }

        public List<BluetoothDeviceModel> GetDiscoveredDevices()
        {
            var json = m_wrapper.GetDiscoveredDevicesJson();
            return JavaJsonDeserializer.FromJson<List<BluetoothDeviceModel>>(json);
        }
        #endregion

    }
}