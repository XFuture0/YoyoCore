using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class test : MonoBehaviour
{
    public GameObject obj;
    public List<GameObject> list = new List<GameObject>();
    private void Start()
    {
        PoolMgr.Instance.InitPool(obj);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject ga = PoolMgr.Instance.PopObj(obj.name);
            list.Add(ga);
        }
        if (Input.GetMouseButtonDown(1))
        {
            PoolMgr.Instance.PushObj(list[list.Count - 1]);
            list.RemoveAt(list.Count - 1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PoolMgr.Instance.ClearPool();
        }
    }
}
