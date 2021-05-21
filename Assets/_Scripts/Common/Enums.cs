using ShimmerRT;
using static Enums;

public static class Enums
{
    public enum PlaybackDirection
    {
        FORWARD = 1,
        BACKWARD = -1
    }

    public enum PlaybackDataType
    {
        LOADED,
        RECORDED
    }

    // Manage if Recording or Playback
    public enum ApplicationState
    {
        NONE,
        RECORDING,
        PLAYBACK
    }

    public enum PanelSlideState
    {
        CLOSED,
        OPEN
    }
}
public static class ExtensionMethods
{
    public static string EnumValue(this ShimmerState e)
    {
        switch (e)
        {
            case ShimmerState.NONE:
                return "Disconnected";
            case ShimmerState.CONNECTING:
                return "Connecting";
            case ShimmerState.CONNECTED:
                return "Connected";
            case ShimmerState.STREAMING:
                return "Streaming";
        }
        return "Something went wrong... :)";
    }
}
