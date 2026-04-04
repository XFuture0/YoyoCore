using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTool
{
    public static void Log(object info)
    {
        if (Setting.isDebugMode) Debug.Log(info);
        return;
    }
    public static void LogWarning(object info)
    {
        if (Setting.isDebugMode) Debug.LogWarning(info);
        return;
    }
    public static void LogError(object info)
    {
        if (Setting.isDebugMode) Debug.LogError(info);
        return;
    }
}
