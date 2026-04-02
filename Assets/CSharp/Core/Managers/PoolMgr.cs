using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
public class PoolData
{
    private Stack<GameObject> dataStack = new Stack<GameObject>();
    private GameObject rootObj;
    private GameObject SaveObj;
    private int MaxCount;
    public PoolData(GameObject root, string name, GameObject usedObj,int maxcount)
    {
        if (PoolMgr.IsOpenLayOut)
        {
            rootObj = new GameObject();
            rootObj.transform.SetParent(root.transform);
            rootObj.name = name + "Pool";
        }
        SaveObj = GameObject.Instantiate(usedObj);
        Push(SaveObj);
        MaxCount = maxcount;
    }
     ~PoolData()
    {
        rootObj = null;
        SaveObj = null;
        dataStack = null;
    }
    public int Count
    {
        get
        {
            return dataStack.Count;
        }
    }
    public GameObject Pop()
    {
        GameObject obj = null;
        if (Count > 0 && Count <= MaxCount)
        {
            obj = dataStack.Pop();
            obj.transform.SetParent(null);
            obj.SetActive(true);
        }
        else if(Count == 0)
        {
            GameObject tempobj = GameObject.Instantiate(SaveObj);
            return tempobj;
        }
        else
        {
            Debug.LogWarning($"该对象池{rootObj}超出最大数量限制,当前最大数量为{MaxCount},请执行扩容操作");
        }
        return obj;
    }
    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        if (PoolMgr.IsOpenLayOut)
        {
            obj.transform.SetParent(rootObj.transform);
        }
        dataStack.Push(obj);
    }
}
/// <summary>
/// 对象池管理单例
/// </summary>
public class PoolMgr : BaseMgr<PoolMgr>
{
    private PoolMgr()
    {

    }
    private Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();
    private GameObject PoolObj;
    public static bool IsOpenLayOut = true;
    /// <summary>
    /// 初始化对象池
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="maxcount"></param>
    public void InitPool(GameObject obj,int maxcount = 50)
    {
        if (poolDic.Count == 0 && IsOpenLayOut)
        {
            PoolObj = new GameObject("Pool");
        }
        if (!poolDic.ContainsKey(obj.name))
        {
            poolDic.Add(obj.name, new PoolData(PoolObj, obj.name, obj, maxcount));
        }
        else
        {
            Debug.LogWarning($"此对象池{obj.name}已存在,请勿重复创建");
        }
    }
    /// <summary>
    /// 获取对象池对象
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject PopObj(string name)
    {
        GameObject obj = null;
        if (poolDic.ContainsKey(name))
        {
            obj = poolDic[name].Pop();
        }
        else
        {
            Debug.LogWarning($"此对象{name}先前无Init记录,请先执行Init操作");
        }
        obj.name = name;
        return obj;
    }
    /// <summary>
    /// 返回对象到对象池
    /// </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        poolDic[obj.name].Push(obj);
    }
    /// <summary>
    /// 清空对象池
    /// </summary>
    public void ClearPool()
    {
        poolDic.Clear();
        GameObject.Destroy(PoolObj);
        PoolObj = null;
    }
}
