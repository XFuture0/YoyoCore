using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTool
{
    /// <summary>
    /// 普通日志输出
    /// </summary>
    /// <param name="info"></param>
    public static void Log(object info)
    {
        if (Setting.isDebugMode) Debug.Log(info);
        return;
    }
    /// <summary>
    /// 警告日志输出
    /// </summary>
    /// <param name="info"></param>
    public static void LogWarning(object info)
    {
        if (Setting.isDebugMode) Debug.LogWarning(info);
        return;
    }
    /// <summary>
    /// 错误日志输出
    /// </summary>
    /// <param name="info"></param>
    public static void LogError(object info)
    {
        if (Setting.isDebugMode) Debug.LogError(info);
        return;
    }
}
