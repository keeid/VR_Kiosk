using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(go);
    }

    public static string Pop(this List<string> go)
    {
        string tempString = go[0];
        go.RemoveAt(0);
        return tempString;
    }

    public static bool IsValid(this GameObject go)
    {
        return go != null && go.activeSelf;
    }
}
