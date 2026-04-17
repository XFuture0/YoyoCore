using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCastTool
{
    /// <summary>
    /// 获取目标位置下的单一泛型同名组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="position"></param>
    /// <param name="name"></param>
    /// <param name="isTag"></param>
    /// <returns></returns>
    public static T RayCastUIComponent<T>(Vector3 position,string name,bool isTag = false) where T : UIBehaviour
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = position;
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        if (raycastResults.Count == 0) return null;
        foreach (RaycastResult raycastResult in raycastResults)
        {
            if (isTag)
            {
                if(raycastResult.gameObject.tag == name && raycastResult.gameObject.TryGetComponent<T>(out _))
                {
                    return raycastResult.gameObject.GetComponent<T>();
                }
            }
            else
            {
                if (raycastResult.gameObject.name == name && raycastResult.gameObject.TryGetComponent<T>(out _))
                {
                    return raycastResult.gameObject.GetComponent<T>();
                }
            }
        }
        return null;
    }
    /// <summary>
    /// 获取目标位置下的所有泛型组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="position"></param>
    /// <returns></returns>
    public static List<T> RayCastUIComponents<T>(Vector3 position) where T : UIBehaviour
    {
        List<T> Results = new List<T>();
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = position;
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        foreach (RaycastResult raycastResult in raycastResults)
        {
            if (raycastResult.gameObject.TryGetComponent<T>(out _))
            {
                Results.Add(raycastResult.gameObject.GetComponent<T>());
            }
        }
        return Results;
    }
}
