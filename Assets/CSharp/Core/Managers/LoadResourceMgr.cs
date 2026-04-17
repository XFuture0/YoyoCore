using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class LoadResourceMgr : BaseMgr<LoadResourceMgr>
{
    private LoadResourceMgr()
    {

    }
    /// <summary>
    /// 加载Resources文件夹资源(同步)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T Load<T>(string path) where T : UnityEngine.Object
    {
        if (File.Exists("Assets/Resources/" + path + ".prefab"))
        {
            T obj = Resources.Load<T>(path);
            obj.name = path + "(res)";
            return obj;
        }
        else
        {
            Debug.LogError("路径为空或者当前路径不存在: " + path);
            return null;
        }
    }
    /// <summary>
    /// 加载Resources文件夹资源(异步)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="callback"></param>
    public void LoadAsync<T>(string path, UnityAction<T> callback) where T : UnityEngine.Object
    {
        MonoMgr.Instance.StartCoroutine(ReallyLoadAsync<T>(path,callback));
    }
    private IEnumerator ReallyLoadAsync<T>(string path, UnityAction<T> callback) where T : UnityEngine.Object
    {
        ResourceRequest rq = Resources.LoadAsync<T>(path);
        yield return rq;
        callback?.Invoke(rq.asset as T);
    }
    /// <summary>
    /// 卸载Resources文件夹资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    public void UnLoadAsset<T>(T obj) where T : UnityEngine.Object
    {
        if (obj != null)
        {
            Resources.UnloadAsset(obj);
        }
    }
    /// <summary>
    /// 清理无用Resources资源
    /// </summary>
    /// <param name="callback"></param>
    public void Clear(UnityAction callback)
    {
        MonoMgr.Instance.StartCoroutine(ReallyClear(callback));
    }
    private IEnumerator ReallyClear(UnityAction callback)
    {
        AsyncOperation ao = Resources.UnloadUnusedAssets();
        yield return ao;
        callback?.Invoke();
    }
}
