using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
namespace YoyoCore
{
    public class YoyoCoreDocument : EditorWindow
    {
        private Dictionary<string, string> MenuItemText = new Dictionary<string, string>();
        private string MenuValue = "”√ªß ÷≤·";
        private string CONTENT = string.Empty;
        private bool OpenMaxMenuTitle = true;
        private bool OpenMinMenuTitle = true;
        private const int BUTTON_HEIGHT = 20;
        Vector2 Allvector2;
        [MenuItem(YoyoCoreMenuItem.Preferences)]
        static void OpenDocument()
        {
            YoyoCoreDocument window = GetWindow<YoyoCoreDocument>();
            window.titleContent = new GUIContent("Yoyo");
            window.minSize = new Vector2(1280, 720);
            window.Show();
        }
        private void OnGUI()
        {
            InitMenu();
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(210));
            CreateMaxTitleButton();
            GUILayout.EndVertical();
            GUILayout.BeginVertical(GUILayout.Width(210));
            CreateMinTitleButton(MenuValue);
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUI.Label(new Rect(420, 0, position.width - 420,position.height),CONTENT,TextTools.InitMarkDownStyle());
        }
        private void InitMenu()
        {
            TextTools.InitXml(MenuItemText);
            GUI.color = new Color(0, 0, 0, 0.5f);
            GUI.Box(new Rect(0, 0, 200, BUTTON_HEIGHT), "");
            GUI.Box(new Rect(210, 0, 200, BUTTON_HEIGHT), "");
            GUI.Box(new Rect(420, 0, position.width - 420, position.height), "");
            GUI.color = new Color(0, 0, 0, 1);
            GUI.Box(new Rect(200, 0, 10, Screen.height), "");
            GUI.Box(new Rect(410, 0, 10, Screen.height), "");
            GUI.color = Color.white;
        }
        private void CreateMaxTitleButton()
        {
            OpenMaxMenuTitle = EditorGUILayout.Foldout(OpenMaxMenuTitle, "YoyoCore", false);
            if (OpenMaxMenuTitle)
            {
                int keyindex = 1;
                string lastvalue = string.Empty;
                foreach (string value in MenuItemText.Values)
                {
                    if (lastvalue != value)
                    {
                        if (GUI.Button(new Rect(0, 5 + (BUTTON_HEIGHT * keyindex), 200, BUTTON_HEIGHT), value))
                        {
                            MenuValue = value;
                        }
                        keyindex++;
                    }
                    lastvalue = value;
                }
            }
        }
        private void CreateMinTitleButton(string value)
        {
            OpenMinMenuTitle = EditorGUILayout.Foldout(OpenMinMenuTitle, value, false);
            if (OpenMinMenuTitle)
            {
                int keyindex = 1;
                foreach (string key in MenuItemText.Keys)
                {
                    if (MenuItemText[key] == value)
                    {
                        if (GUI.Button(new Rect(210, 5 + (BUTTON_HEIGHT * keyindex), 200, BUTTON_HEIGHT), key))
                        {
                            CONTENT = TextTools.InitMarkDown(key);
                        }
                        keyindex++;
                    }
                }
            }
        }
    }
}
