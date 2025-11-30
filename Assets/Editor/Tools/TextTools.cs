using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
namespace YoyoCore
{
    public class TextTools
    {
        public static void InitXml(Dictionary<string, string> Dic)
        {
            string XmlPath = Application.dataPath + "/Resources/MenuTitle.xml";
            XmlDocument MenuTitleXml = new XmlDocument();
            MenuTitleXml.Load(XmlPath);
            XmlNode XmlRoot = MenuTitleXml.SelectSingleNode("messages");
            XmlNodeList MainNodes = XmlRoot.SelectNodes("enum");
            foreach (XmlNode MinNodes in MainNodes)
            {
                XmlNodeList Nodes = MinNodes.SelectNodes("field");
                foreach (XmlNode Node in Nodes)
                {
                    if (!Dic.ContainsKey(Node.Attributes["name"].Value))
                    {
                        Dic.Add(Node.Attributes["name"].Value, MinNodes.Attributes["name"].Value);
                    }
                }
            }
        }
        public static string InitMarkDown(string name)
        {
            string content = string.Empty;
            if(Resources.Load(name) != null)
            {
                TextAsset textAsset = Resources.Load<TextAsset>(name);
                content = textAsset.text;
            }
            return content;
        }
        public static GUIStyle InitMarkDownStyle()
        {
            GUIStyle style = new GUIStyle();
            style.richText = true;
            style.wordWrap = true;
            style.normal.textColor = Color.white;
            return style;
        }
    }
}
