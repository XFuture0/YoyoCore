using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class EventInfoBase
{

}
public class EventInfo<T> : EventInfoBase
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}
public class EventInfo : EventInfoBase
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

public class EventMgr : BaseMgr_Mono<EventMgr>
{
    private Dictionary<EventType, EventInfoBase> eventDic = new Dictionary<EventType, EventInfoBase>();
    private EventMgr()
    {

    }
    #region 触发带参事件
    /// <summary>
    /// 触发目标事件(传入目标参数)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="param"></param>
    public void EventTrigger<T>(EventType eventName, T param)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T>).actions?.Invoke(param);
        }
    }
    /// <summary>
    /// 添加事件,事件添加后要记得移除(传入目标参数)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="func"></param>
    public void AddEventListener<T>(EventType name, UnityAction<T> func)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions += func;
        }
        else
        {
            eventDic.Add(name, new EventInfo<T>(func));
        }
    }
    /// <summary>
    /// 移除回调(传入目标参数)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="func"></param>
    public void RemoveEventListener<T>(EventType name, UnityAction<T> func)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions -= func;
        }
    }
    #endregion
    #region 触发无参事件
    /// <summary>
    /// 触发目标事件(无参数)
    /// </summary>
    /// <param name="eventName"></param>
    public void EventTrigger(EventType eventName)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo).actions?.Invoke();
        }
    }
    /// <summary>
    /// 添加事件,事件添加后要记得移除(无参数)
    /// </summary>
    /// <param name="name"></param>
    /// <param name="func"></param>
    public void AddEventListener(EventType name, UnityAction func)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions += func;
        }
        else
        {
            eventDic.Add(name, new EventInfo(func));
        }
    }
    /// <summary>
    /// 移除回调(无参数)
    /// </summary>
    /// <param name="name"></param>
    /// <param name="func"></param>
    public void RemoveEventListener(EventType name, UnityAction func)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions -= func;
        }
    }
    /// <summary>
    /// 移除普通事件
    /// </summary>
    /// <param name="name"></param>
    public void Clear(EventType name)
    {
        if (eventDic.ContainsKey(name))
        {
            eventDic.Remove(name);
        }
    }
    #endregion
    /// <summary>
    /// 清空所有事件
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}
