using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InputInfo
{
    public E_KeyOrMouse KeyOrMouse;
    public E_InputType InputType;
    public KeyCode KeyCode;
    public int MouseCode;
    public InputInfo(E_InputType inputType, KeyCode keyCode)
    {
        KeyOrMouse = E_KeyOrMouse.Key;
        InputType = inputType;
        KeyCode = keyCode;
    }
    public InputInfo(E_InputType inputType, int mouseCode)
    {
        KeyOrMouse = E_KeyOrMouse.Mouse;
        InputType = inputType;
        MouseCode = mouseCode;
    }
    public enum E_KeyOrMouse
    {
        Key,
        Mouse
    }
    public enum E_InputType
    {
        Down,
        Up,
        Press
    }
}
public class InputMgr : BaseMgr<InputMgr>
{
    private bool IsStart = true;
    private Dictionary<EventType, InputInfo> InputDic = new Dictionary<EventType, InputInfo>();
    private InputInfo inputInfo;
    private InputMgr()
    {
        MonoMgr.Instance.AddUpdateListener(Update);
    }
    /// <summary>
    /// 启动或者关闭输入
    /// </summary>
    /// <param name="isStart"></param>
    public void StartOrCloseInput(bool isStart)
    {
        IsStart = isStart;
    }
    private void Update()
    {
        if (!IsStart) return;
        foreach (var eventtype in InputDic.Keys)
        {
            inputInfo = InputDic[eventtype];
            if (inputInfo.KeyOrMouse == InputInfo.E_KeyOrMouse.Key)
            {
                switch (inputInfo.InputType)
                {
                    case InputInfo.E_InputType.Down:
                        if (Input.GetKeyDown(inputInfo.KeyCode))
                        {
                            EventMgr.Instance.EventTrigger(eventtype);
                        }
                        break;
                    case InputInfo.E_InputType.Up:
                        if (Input.GetKeyUp(inputInfo.KeyCode))
                        {
                            EventMgr.Instance.EventTrigger(eventtype);
                        }
                        break;
                    case InputInfo.E_InputType.Press:
                        if (Input.GetKey(inputInfo.KeyCode))
                        {
                            EventMgr.Instance.EventTrigger(eventtype);
                        }
                        break;
                }
            }
            else
            {
                switch (inputInfo.InputType)
                {
                    case InputInfo.E_InputType.Down:
                        if (Input.GetMouseButtonDown(inputInfo.MouseCode))
                        {
                            EventMgr.Instance.EventTrigger(eventtype);
                        }
                        break;
                    case InputInfo.E_InputType.Up:
                        if (Input.GetMouseButtonUp(inputInfo.MouseCode))
                        {
                            EventMgr.Instance.EventTrigger(eventtype);
                        }
                        break;
                    case InputInfo.E_InputType.Press:
                        if (Input.GetMouseButton(inputInfo.MouseCode))
                        {
                            EventMgr.Instance.EventTrigger(eventtype);
                        }
                        break;
                }
            }
        }
    }
    /// <summary>
    /// 修改或加入键盘按键信息
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="key"></param>
    /// <param name="inputType"></param>
    public void AddKeyBoardInfo(EventType eventType, KeyCode key, InputInfo.E_InputType inputType,UnityAction callback = null)
    {
        if (!InputDic.ContainsKey(eventType))
        {
            InputDic.Add(eventType, new InputInfo(inputType, key));
        }
        else
        {
            InputDic[eventType].KeyOrMouse = InputInfo.E_KeyOrMouse.Key;
            InputDic[eventType].KeyCode = key;
            InputDic[eventType].InputType = inputType;
        }
        if(callback != null)
        {
            EventMgr.Instance.AddEventListener(eventType, callback);
        }
    }
    /// <summary>
    /// 修改或者加入鼠标按键信息
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="mouseCode"></param>
    /// <param name="inputType"></param>
    public void AddMouseInfo(EventType eventType, int mouseCode, InputInfo.E_InputType inputType, UnityAction callback = null)
    {
        if (!InputDic.ContainsKey(eventType))
        {
            InputDic.Add(eventType, new InputInfo(inputType, mouseCode));
        }
        else
        {
            InputDic[eventType].KeyOrMouse = InputInfo.E_KeyOrMouse.Mouse;
            InputDic[eventType].MouseCode = mouseCode;
            InputDic[eventType].InputType = inputType;
        }
        if (callback != null)
        {
            EventMgr.Instance.AddEventListener(eventType, callback);
        }
    }
    /// <summary>
    /// 移除输入事件
    /// </summary>
    /// <param name="eventType"></param>
    public void RemoveInputInfo(EventType eventType)
    {
        if (InputDic.ContainsKey(eventType))
        {
            InputDic.Remove(eventType);
        }
    }
}
