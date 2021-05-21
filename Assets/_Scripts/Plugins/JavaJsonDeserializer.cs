using System;
using UnityEngine;

public static class JavaJsonDeserializer
{
    public static T FromJson<T>(string json)
    {
        json = "{\"Item\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Item;
    }

    [Serializable]
    private class Wrapper<T>
    {
#pragma warning disable 0649
        public T Item;
#pragma warning restore 0649
    }
}
