using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class XMLLoad:MonoBehaviour
{
    [SerializeField]
    private TextAsset xml;
    private enum XMLIndex
    {
        Id = 0,
        Person,
        Position,
        Contents,
        Command
    }

    private List<string> personList = new List<string>();
    private List<string> positionList = new List<string>();
    private List<string> contentsList = new List<string>();

    private void Awake()
    {
        LoadXml(xml.text);
    }

    public List<string> GetContentsList()
    {
        return contentsList;
    }

    private void LoadXml(string xmlText)
    {
        var xml = new XmlDocument();
        xml.LoadXml(xmlText);

        XmlElement element = xml.DocumentElement;

        //ShowData(element);

        var contents = element.GetElementsByTagName(XMLIndex.Contents.ToString());
        if(contents != null)
        {
            Debug.Log("<color=red>hoge</color>");
            foreach(var cont in contents)
            {
                var contelement = cont as XmlElement;
                if(contelement != null)
                {
                    string name = contelement.Name;
                    string value = contelement.FirstChild != null ? contelement.FirstChild.Value : "";
                    contentsList.Add(value);
                    Debug.LogFormat("name:{0}, value:{1}", name, value);
                }
            }
        }
    }

    private void ShowData(XmlElement element)
    {
        string name = element.Name;
        string value = element.FirstChild != null ? element.FirstChild.Value : "";
        string atr = string.Empty;

        if(element.HasAttributes)
        {
            foreach(var attribute in element.Attributes)
            {
                var xmlAttribute = attribute as XmlAttribute;
                if(xmlAttribute != null)
                {
                    atr += string.Format("\t\t{0}={1}\n", xmlAttribute.Name, xmlAttribute.Value);
                }
            }
        }
        Debug.LogFormat("name:{0}\n\tvalue:{1}\n\tattribute:\n{2}", name, value, atr);

        foreach(var child in element.ChildNodes)
        {
            var childElement = child as XmlElement;
            if(childElement != null)
            {
                ShowData(childElement);
            }
        }
    }
}