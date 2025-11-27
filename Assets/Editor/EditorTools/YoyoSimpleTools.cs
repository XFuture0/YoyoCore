using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class YoyoSimpleTools
{
    //Editor规范字体调整
    public static GUIStyle AdjustFont(GUIStyle style)
    {
        style.alignment = TextAnchor.UpperCenter;
        style.normal.textColor = Color.white;
        style.fontSize = 15;
        return style;
    }
}
