using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MathTool
{
    /// <summary>
    /// 角度制转弧度制
    /// </summary>
    /// <param name="deg"></param>
    /// <returns></returns>
    public static float DegToRad(float deg)
    {
        return deg * Mathf.Deg2Rad;
    }
    /// <summary>
    /// 弧度制转角度制
    /// </summary>
    /// <param name="rad"></param>
    /// <returns></returns>
    public static float RadToDeg(float rad)
    {
        return rad * Mathf.Rad2Deg;
    }
    /// <summary>
    /// 获取在Y轴平面上两点之间的距离
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="pos2"></param>
    /// <returns></returns>
    public static float GetDistanceXZ(Vector3 pos1, Vector3 pos2)
    {
        pos1.y = 0;
        pos2.y = 0;
        return Vector3.Distance(pos1, pos2);
    }
    /// <summary>
    /// 获取在Z轴平面上两点之间的距离
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="pos2"></param>
    /// <returns></returns>
    public static float GetDistanceXY(Vector3 pos1, Vector3 pos2)
    {
        pos1.z = 0;
        pos2.z = 0;
        return Vector3.Distance(pos1, pos2);
    }
    /// <summary>
    /// 获取在X轴平面上两点之间的距离
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="pos2"></param>
    /// <returns></returns>
    public static float GetDistanceYZ(Vector3 pos1, Vector3 pos2)
    {
        pos1.x = 0;
        pos2.x = 0;
        return Vector3.Distance(pos1, pos2);
    }
    /// <summary>
    /// 判断Y轴平面上两点之间的距离是否小于等于给定距离
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="pos2"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static bool CheckDistanceXZ(Vector3 pos1, Vector3 pos2, float distance)
    {
        return GetDistanceXZ(pos1, pos2) <= distance;
    }
    /// <summary>
    /// 判断Z轴平面上两点之间的距离是否小于等于给定距离
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="pos2"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static bool CheckDistanceXY(Vector3 pos1, Vector3 pos2, float distance)
    {
        return GetDistanceXY(pos1, pos2) <= distance;
    }
    /// <summary>
    /// 判断X轴平面上两点之间的距离是否小于等于给定距离
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="pos2"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static bool CheckDistanceYZ(Vector3 pos1, Vector3 pos2, float distance)
    {
        return GetDistanceYZ(pos1, pos2) <= distance;
    }
    /// <summary>
    /// 判断世界坐标点是否在屏幕内
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static bool IsWorldPosOutScreen(Vector3 pos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
        if (screenPos.x >= 0 && screenPos.x <= Screen.width && screenPos.y >= 0 && screenPos.y <= Screen.height)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    /// <summary>
    /// 判断在Y轴平面上世界坐标点是否在扇形内
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="forward"></param>
    /// <param name="target"></param>
    /// <param name="radius"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static bool IsInSectorRangeXZ(Vector3 pos, Vector3 forward, Vector3 target, float radius, float angle)
    {
        pos.y = 0;
        target.y = 0;
        forward.y = 0;
        return Vector3.Distance(pos, target) <= radius && Vector3.Angle(forward, target - pos) <= angle / 2f;
    }
    /// <summary>
    /// 射线检测（Collider）
    /// </summary>
    /// <param name="ray"></param>
    /// <param name="callback"></param>
    /// <param name="MaxDistance"></param>
    /// <param name="LayerMask"></param>
    public void RayCast(Ray ray, UnityAction<RaycastHit> callback, float MaxDistance, int LayerMask)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MaxDistance, LayerMask))
        {
            callback?.Invoke(hit);
        }
    }
    /// <summary>
    /// 射线检测（GameObject）
    /// </summary>
    /// <param name="ray"></param>
    /// <param name="callback"></param>
    /// <param name="MaxDistance"></param>
    /// <param name="LayerMask"></param>
    public void RayCast(Ray ray, UnityAction<GameObject> callback, float MaxDistance, int LayerMask)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MaxDistance, LayerMask))
        {
            callback?.Invoke(hit.collider.gameObject);
        }
    }
    /// <summary>
    /// 射线检测（Component）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ray"></param>
    /// <param name="callback"></param>
    /// <param name="MaxDistance"></param>
    /// <param name="LayerMask"></param>
    public void RayCast<T>(Ray ray, UnityAction<T> callback, float MaxDistance, int LayerMask)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MaxDistance, LayerMask))
        {
            callback?.Invoke(hit.collider.gameObject.GetComponent<T>());
        }
    }
    /// <summary>
    /// 射线检测（所有Collider）
    /// </summary>
    /// <param name="ray"></param>
    /// <param name="callback"></param>
    /// <param name="MaxDistance"></param>
    /// <param name="LayerMask"></param>
    public void RayCastAll(Ray ray, UnityAction<RaycastHit> callback, float MaxDistance, int LayerMask)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray, MaxDistance, LayerMask);
        for (int i = 0; i < hits.Length; i++)
        {
            callback?.Invoke(hits[i]);
        }
    }
    /// <summary>
    /// 射线检测（所有GameObject）
    /// </summary>
    /// <param name="ray"></param>
    /// <param name="callback"></param>
    /// <param name="MaxDistance"></param>
    /// <param name="LayerMask"></param>
    public void RayCastAll(Ray ray, UnityAction<GameObject> callback, float MaxDistance, int LayerMask)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray, MaxDistance, LayerMask);
        for (int i = 0; i < hits.Length; i++)
        {
            callback?.Invoke(hits[i].collider.gameObject);
        }
    }
    /// <summary>
    /// 射线检测（所有Component）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ray"></param>
    /// <param name="callback"></param>
    /// <param name="MaxDistance"></param>
    /// <param name="LayerMask"></param>
    public void RayCastAll<T>(Ray ray, UnityAction<T> callback, float MaxDistance, int LayerMask)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray, MaxDistance, LayerMask);
        for (int i = 0; i < hits.Length; i++)
        {
            callback?.Invoke(hits[i].collider.gameObject.GetComponent<T>());
        }
    }
    /// <summary>
    /// 矩形盒检测
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pos"></param>
    /// <param name="rotation"></param>
    /// <param name="halfExtents"></param>
    /// <param name="LayerMask"></param>
    /// <param name="callback"></param>
    public void OverLapBox<T>(Vector3 pos, Quaternion rotation, Vector3 halfExtents, int LayerMask, UnityAction<T> callback) where T : class
    {
        Type type = typeof(T);
        Collider[] colliders = Physics.OverlapBox(pos, halfExtents, rotation, LayerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (type == typeof(Collider))
            {
                callback?.Invoke(colliders[i] as T);
            }
            else if (type == typeof(GameObject))
            {
                callback?.Invoke(colliders[i].gameObject as T);
            }
            else
            {
                callback?.Invoke(colliders[i].GetComponent<T>());
            }
        }
    }
    /// <summary>
    /// 球体检测
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pos"></param>
    /// <param name="radius"></param>
    /// <param name="LayerMask"></param>
    /// <param name="callback"></param>
    public void OverLapSphere<T>(Vector3 pos, float radius, int LayerMask, UnityAction<T> callback) where T : class
    {
        Type type = typeof(T);
        Collider[] colliders = Physics.OverlapSphere(pos, radius, LayerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (type == typeof(Collider))
            {
                callback?.Invoke(colliders[i] as T);
            }
            else if (type == typeof(GameObject))
            {
                callback?.Invoke(colliders[i].gameObject as T);
            }
            else
            {
                callback?.Invoke(colliders[i].GetComponent<T>());
            }
        }
    }
}
