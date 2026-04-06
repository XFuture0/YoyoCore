using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTimerTool : IDisposable
{
    private string Name;
    private float StartTime;
    /// <summary>
    /// 检测代码块的执行时间，单位为毫秒
    /// </summary>
    /// <param name="name"></param>
    /// <param name="num"></param>
    public CustomTimerTool(string name)
    {
        Name = name;
        StartTime = Time.realtimeSinceStartup;
    }
    public void Dispose()
    {
        float spendTime = (Time.realtimeSinceStartup - StartTime) * 1000f;
        Debug.Log(Name + ": " + spendTime + "ms");
    }
}
