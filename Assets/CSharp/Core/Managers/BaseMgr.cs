using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
/// <summary>
/// 非继承Mono单例
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseMgr<T> where T : class
{
    private static T instance;
    protected static readonly object lockobj = new object();
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockobj)
                {
                    if (instance == null)
                    {
                        Type type = typeof(T);
                        ConstructorInfo info = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                                   null,
                                                                   Type.EmptyTypes,
                                                                   null);
                        if (info != null)
                        {
                            instance = info.Invoke(null) as T;//无参数
                        }
                        else Debug.LogError($"脚本{type.Name}的构造函数非静态，请检查是否构造函数为公开或不存在");
                    }
                }
            }
            return instance;
        }
    }
}
