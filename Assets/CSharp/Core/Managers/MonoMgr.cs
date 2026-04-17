using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Mono管理器,用于非Mono对象调用生命周期函数
/// </summary>
public class MonoMgr : BaseMgr_Mono<MonoMgr>
{
    private event UnityAction OnUpdate;
    private event UnityAction OnFixedUpdate;
    private event UnityAction OnLateUpdate;
    private MonoMgr()
    {

    }
    /// <summary>
    /// 添加Update事件,开启注册后需记得注销
    /// </summary>
    /// <param name="unityAction"></param>
    public void AddUpdateListener(UnityAction unityAction)
    {
        OnUpdate += unityAction;
    }
    /// <summary>
    /// 添加FixedUpdate事件，开启注册后需记得注销
    /// </summary>
    /// <param name="unityAction"></param>
    public void AddFixedUpdateListener(UnityAction unityAction)
    {
        OnFixedUpdate += unityAction;
    }
    /// <summary>
    /// 添加LateUpdate事件，开启注册后需记得注销
    /// </summary>
    /// <param name="unityAction"></param>
    public void AddLateUpdateListener(UnityAction unityAction)
    {
        OnLateUpdate += unityAction;
    }
    /// <summary>
    /// 移除Update事件
    /// </summary>
    /// <param name="unityAction"></param>
    public void RemoveUpdateListener(UnityAction unityAction)
    {
        OnUpdate -= unityAction;
    }
    /// <summary>
    /// 移除FixedUpdate事件
    /// </summary>
    /// <param name="unityAction"></param>
    public void RemoveFixedUpdateListener(UnityAction unityAction)
    {
        OnFixedUpdate -= unityAction;
    }
    /// <summary>
    /// 移除LateUpdate事件
    /// </summary>
    /// <param name="unityAction"></param>
    public void RemoveLateUpdateListener(UnityAction unityAction)
    {
        OnLateUpdate -= unityAction;
    }
    private void Update()
    {
        OnUpdate?.Invoke();
    }
    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }
    private void LateUpdate()
    {
        OnLateUpdate?.Invoke();
    }
}
