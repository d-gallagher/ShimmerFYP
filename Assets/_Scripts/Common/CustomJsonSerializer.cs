using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class CustomJsonSerializer
{
    #region Standard
    public static T FromJson<T>(string json)
    {
        json = "{\"Item\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Item;
    }

    public static string ToJson<T>(ref T t)
    {
        return JsonUtility.ToJson(t);
    }

    [Serializable]
    private class Wrapper<T>
    {
#pragma warning disable 0649
        public T Item;
#pragma warning restore 0649
    }
    #endregion

    #region Lists
    public static List<T> FromJsonList<T>(string json)
    {
        json = "{\"Items\":" + json + "}";
        ArrayWrapper<T> wrapper = JsonUtility.FromJson<ArrayWrapper<T>>(json);
        return new List<T>(wrapper.Items);
    }

    public static string ToJsonList<T>(List<T> list)
    {
        ArrayWrapper<T> wrapper = new ArrayWrapper<T>();
        wrapper.Items = list.ToArray();
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJsonSB<T>(List<T> list)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        for(int i =0; i < list.Count -1; i++)
        {
            sb.Append(JsonUtility.ToJson(list[i]));
            sb.Append(",");
        }
        sb.Append(JsonUtility.ToJson(list[list.Count - 1]));
        sb.Append("]");

        return sb.ToString();
    }

    [Serializable]
    private class ArrayWrapper<T>
    {
        public T[] Items;
    }
    #endregion
}