using UnityEngine;

public static class RotationHelper
{
    // https://www.codeproject.com/Tips/1240454/How-to-Convert-Right-Handed-to-Left-Handed-Coordin
    public static Quaternion ConvertRightHandedToLeftHandedQuaternion(Quaternion rightHandedQuaternion)
    {
        return new Quaternion(-rightHandedQuaternion.x,
                               -rightHandedQuaternion.z,
                               -rightHandedQuaternion.y,
                                 rightHandedQuaternion.w);
    }
}