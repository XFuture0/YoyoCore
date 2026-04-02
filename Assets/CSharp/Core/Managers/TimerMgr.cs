using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Events;
public class TimeItem
{
    public int KeyID;
    public UnityAction overcallback;
    public UnityAction callback;
    public float allTime;
    public float MaxAllTime;
    public float intervalTime;
    public float MaxIntervalTime;
    public bool isRunning;
    public void InitInfo(int keyID, float allTime, float intervalTime = 0, UnityAction callback = null, UnityAction overcallback = null)
    {
        this.KeyID = keyID;
        this.allTime = allTime;
        this.MaxAllTime = allTime;
        this.intervalTime = intervalTime;
        this.MaxIntervalTime = intervalTime;
        this.callback = callback;
        this.overcallback = overcallback;
    }
    public void ResetTimer()
    {
        allTime = MaxAllTime;
        intervalTime = MaxIntervalTime;
    }
}
public class TimerMgr : BaseMgr<TimerMgr>
{
    private int TIME_KEY = 0;
    private Dictionary<int, TimeItem> TimerDic = new Dictionary<int, TimeItem>();
    private Dictionary<int, TimeItem> ReallyTimerDic = new Dictionary<int, TimeItem>();
    private List<TimeItem> DelList = new List<TimeItem>();
    private Coroutine Timer;
    private Coroutine RealTimer;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(Setting.IntervalTime);
    private WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(Setting.IntervalTime);
    private TimerMgr()
    {

    }
    /// <summary>
    /// 启动总计时器
    /// </summary>
    public void Start()
    {
        Timer = MonoMgr.Instance.StartCoroutine(StartTiming(false, TimerDic));
        RealTimer = MonoMgr.Instance.StartCoroutine(StartTiming(true, ReallyTimerDic));
    }
    private IEnumerator StartTiming(bool isRealTime, Dictionary<int, TimeItem> TimerDic)
    {
        while (true)
        {
            if (!isRealTime)
            {
                yield return waitForSeconds;
            }
            else
            {
                yield return waitForSecondsRealtime;
            }
            foreach (var item in TimerDic.Values)
            {
                if (!item.isRunning)
                {
                    continue;
                }
                if (item.callback != null)
                {
                    item.intervalTime -= Setting.IntervalTime;
                    if (item.intervalTime <= 0)
                    {
                        item.callback?.Invoke();
                        item.intervalTime = item.MaxIntervalTime;
                    }
                }
                item.allTime -= Setting.IntervalTime;
                if (item.allTime <= 0)
                {
                    if (item.overcallback != null)
                    {
                        item.overcallback?.Invoke();
                    }
                    DelList.Add(item);
                }
            }
            for (int i = 0; i < DelList.Count; i++)
            {
                TimerDic.Remove(DelList[i].KeyID);
            }
            DelList.Clear();
        }
    }
    /// <summary>
    /// 停止总计时器
    /// </summary>
    public void Stop()
    {
        MonoMgr.Instance.StopCoroutine(Timer);
        MonoMgr.Instance.StopCoroutine(RealTimer);
    }
    /// <summary>
    /// 创建新计时器
    /// </summary>
    /// <param name="isRealTime"></param>
    /// <param name="allTime"></param>
    /// <param name="intervalTime"></param>
    /// <param name="callback"></param>
    /// <param name="overcallback"></param>
    /// <returns></returns>
    public int CreateTimer(bool isRealTime, float allTime, float intervalTime = 0, UnityAction callback = null, UnityAction overcallback = null)
    {
        int keyid = TIME_KEY++;
        TimeItem timeItem = new TimeItem();
        timeItem.InitInfo(keyid, allTime, intervalTime, callback, overcallback);
        if (!isRealTime)
        {
            TimerDic.Add(keyid, timeItem);
        }
        else
        {
            ReallyTimerDic.Add(keyid, timeItem);
        }
        return keyid;
    }
    /// <summary>
    /// 移除计时器
    /// </summary>
    /// <param name="keyID"></param>
    public void RemoveTimer(int keyID)
    {
        if (TimerDic.ContainsKey(keyID))
        {
            TimerDic.Remove(keyID);
        }
        else if (ReallyTimerDic.ContainsKey(keyID))
        {
            ReallyTimerDic.Remove(keyID);
        }
    }
    /// <summary>
    /// 重置计时器
    /// </summary>
    /// <param name="keyID"></param>
    public void ReSetTimer(int keyID)
    {
        if (TimerDic.ContainsKey(keyID))
        {
            TimerDic[keyID].ResetTimer();
        }
        else if (ReallyTimerDic.ContainsKey(keyID))
        {
            ReallyTimerDic[keyID].ResetTimer();
        }
    }
    /// <summary>
    /// 启动当前ID计时器
    /// </summary>
    /// <param name="keyID"></param>
    public void StartTimer(int keyID)
    {
        if(Timer == null)
        {
            Start();
        }
        if (TimerDic.ContainsKey(keyID))
        {
            TimerDic[keyID].isRunning = true;
        }
        else if (ReallyTimerDic.ContainsKey(keyID))
        {
            ReallyTimerDic[keyID].isRunning = true;
        }
    }
    /// <summary>
    /// 停止当前ID计时器
    /// </summary>
    /// <param name="keyID"></param>
    public void StopTimer(int keyID)
    {
        if (TimerDic.ContainsKey(keyID))
        {
            TimerDic[keyID].isRunning = false;
        }
        else if (ReallyTimerDic.ContainsKey(keyID))
        {
            ReallyTimerDic[keyID].isRunning = false;
        }
    }
    /// <summary>
    /// 检查当前ID计时器剩余时间
    /// </summary>
    /// <param name="keyID"></param>
    /// <returns></returns>
    public float CheckTimer(int keyID)
    {
        float curtime = 0;
        if (TimerDic.ContainsKey(keyID))
        {
            curtime = TimerDic[keyID].allTime;
        }
        else if (ReallyTimerDic.ContainsKey(keyID))
        {
            curtime = ReallyTimerDic[keyID].allTime;
        }
        return curtime;
    }
}
