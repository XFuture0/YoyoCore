using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTimerTool : IDisposable
{
    private string Name;
    private uint RepeatCount;
    private float StartTime;
    public CustomTimerTool(string name, uint num)
    {
        Name = name;
        RepeatCount = num;
        if (num == 0) num = 1;
        StartTime = Time.realtimeSinceStartup;
    }
    public void Dispose()
    {
        float spendTime = (Time.realtimeSinceStartup - StartTime) * 1000f;
        Debug.Log(Name + ": " + spendTime + "ms");
    }
}
