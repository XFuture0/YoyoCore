using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneChangeMgr : BaseMgr<SceneChangeMgr>
{
    private SceneChangeMgr()
    {

    }
    /// <summary>
    /// 加载场景(同步)
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="callback"></param>
    public void LoadScene(string sceneName, UnityAction callback = null)
    {
        SceneManager.LoadScene(sceneName);
        callback?.Invoke();
    }
    /// <summary>
    /// 加载场景(异步)
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="callback"></param>
    public void LoadSceneAsync(string sceneName, UnityAction callback = null)
    {
        MonoMgr.Instance.StartCoroutine(ReallyLoadSceneAsync(sceneName, callback));
    }
    private IEnumerator ReallyLoadSceneAsync(string sceneName, UnityAction callback = null)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        yield return ao;
        callback?.Invoke();
    }
}
