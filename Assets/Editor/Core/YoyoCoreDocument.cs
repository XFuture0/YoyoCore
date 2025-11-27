using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class YoyoCoreDocument : EditorWindow
{
    private int BUTTON_HEIGHT = 30;
    private string CONTENT = string.Empty;
    private Color BACKCOLOR = new Color(0, 0, 0, 0.5f);
    [MenuItem("Yoyo/Document")]
    static void OpenDocument()
    {
        YoyoCoreDocument window = GetWindow<YoyoCoreDocument>();
        window.titleContent = new GUIContent("Yoyo");
        window.Show();
    }
    private void OnGUI()
    {
        int ButtonIndex = 0;
        GUI.color = BACKCOLOR;
        GUI.Box(new Rect(position.width * 0.25f,0,position.width * 0.75f,position.height),"");
        GUI.color = Color.white;
        if (GUI.Button(new Rect(0, 10 + BUTTON_HEIGHT * ButtonIndex++, position.width * 0.25f, BUTTON_HEIGHT),"Ö÷ÎÄµµ"))
        {
            CONTENT = GetContent("111");
        }
        GUI.Label(new Rect(position.width * 0.25f,0,position.width * 0.75f,position.height),CONTENT,YoyoSimpleTools.AdjustFont(new GUIStyle()));
    }
    private string GetContent(string filename)
    {
        string content = string.Empty;
        TextAsset textAsset = Resources.Load<TextAsset>(filename);
        content = textAsset.text;
        return content;
    }
}
