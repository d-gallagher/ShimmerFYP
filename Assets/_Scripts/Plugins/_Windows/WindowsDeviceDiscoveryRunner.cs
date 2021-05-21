using Plugins.Listeners;
using Plugins.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Plugins.Windows
{
    public class WindowsDeviceDiscoveryRunner
    {
        private static readonly string CURRENT_DIR = Environment.CurrentDirectory;
        private static readonly string _appPath = CURRENT_DIR + @"\Assets\Plugins\windows\discovery\BluetoothDiscoveryApp.exe";

        private IDeviceDiscoveredListener _bluetoothDiscoveryListener;

        //private IBluetoothDiscoveryListener _caller;
        private int _maxTimeToWait;

        private List<BluetoothDeviceModel> _devices;

        public WindowsDeviceDiscoveryRunner(int maxTimeToWait, IDeviceDiscoveredListener bluetoothDiscoveryListener)
        {
            _maxTimeToWait = maxTimeToWait;
            _bluetoothDiscoveryListener = bluetoothDiscoveryListener;
            _devices = new List<BluetoothDeviceModel>();
        }

        public List<BluetoothDeviceModel> GetDevices()
        {
            return _devices;
        }

        public void Run()
        {
            Process p = new Process()
            {
                EnableRaisingEvents = true
            };

            p.StartInfo = new ProcessStartInfo
            {
                FileName = _appPath,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            p.Exited += OnProcessExited;

            p.Start();
            p.WaitForExit(_maxTimeToWait);

            if (!p.HasExited)
            {
                p.Kill();
            }
        }

        private void OnProcessExited(object sender, EventArgs e)
        {
            var data = ((Process)(sender)).StandardOutput.ReadToEnd();
            if (data != null && data.Length > 0)
            {
                _bluetoothDiscoveryListener.ReceiveBluetoothDeviceListJson(data);
                //_caller.ReceiveBluetoothDeviceData(data);
            }
        }
    }
}
