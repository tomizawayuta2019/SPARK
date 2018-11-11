using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Xml;
using UnityEngine;

/*
 *  XMLをロードする
 */

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
    
    [SerializeField]
    private List<string> personList_Before = new List<string>();
    private List<string> positionList_Before = new List<string>();
    private List<string> contentsList = new List<string>();

    private List<ShowScript.Chara> personList = new List<ShowScript.Chara>();
    private List<ShowScript.Potision> positionList = new List<ShowScript.Potision>();

    XmlElement element;

    private void Awake()
    {
        LoadXml(xml.text);
        Person_ListChange();
        Potision_ListChange();
    }

    public List<ShowScript.Chara> GetPersonList()
    {
        return personList;
    }

    public List<ShowScript.Potision> GetPotisionList()
    {
        return positionList;
    }

    public List<string> GetContentsList()
    {
        return contentsList;
    }

    private void LoadXml(string xmlText)
    {
        var xml = new XmlDocument();
        xml.LoadXml(xmlText);

        element = xml.DocumentElement;
        
        //var filter = element.GetElementsByTagName("test2");

        personList_Before = SetList(XMLIndex.Person);
        positionList_Before = SetList(XMLIndex.Position);
        contentsList = SetList(XMLIndex.Contents);

        Person_ListChange();
    }

    //リストを返す
    private List<string> SetList(XMLIndex index)
    {
        List<string> list = new List<string>();
        var contents = element.GetElementsByTagName(index.ToString());
        if (contents != null)
        {
            foreach (var cont in contents)
            {
                var contelement = cont as XmlElement;
                if (contelement != null)
                {
                    string name = contelement.Name;
                    string value = contelement.FirstChild != null ? contelement.FirstChild.Value : "";
                    list.Add(value);
                }
            }
        }
        return list;
    }

    //リストの変換
    private void Person_ListChange()
    {
        for(int i = 0; i < personList_Before.Count; i++)
        {
            //ShowScript.Chara val = (ShowScript.Chara)Enum.Parse()
            try
            {
                personList.Add((ShowScript.Chara)Enum.Parse(typeof(ShowScript.Chara),
                personList_Before[i], true));
            }
            catch {
                Debug.LogAssertion("キャラ画像の名前が正しくありません");
            }
        }
    }

    private void Potision_ListChange()
    {
        for (int i = 0; i < positionList_Before.Count; i++)
        {
            //ShowScript.Chara val = (ShowScript.Chara)Enum.Parse()
            positionList.Add((ShowScript.Potision)Enum.Parse(typeof(ShowScript.Potision),
                positionList_Before[i], true));
        }
    }


    //デバッグ用で置いてる
    private void ShowData()
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
    }
}