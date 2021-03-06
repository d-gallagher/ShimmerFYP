using System;

namespace Plugins.Models
{
    [Serializable]
    public class ComDevice
    {
        // COM port should be unique
        public int ComPort;
        // Shimmer ID should be unique
        public string ShimmerID;

        public string DisplayName { get => string.Format("Shimmer {0} on COM{1}", ShimmerID, ComPort); }

        public ComDevice(int comPort, string shimmerID)
        {
            ComPort = comPort;
            ShimmerID = shimmerID;
        }
    }
}