using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Xml;
using UnityEngine;

/*
 *  XMLをロードしてシナリオデータを保持しておくスクリプト
 *  現段階ではシングルトンだが、将来的にScriptableObjectに移行する予定
 */

public enum XMLIndex
{
    Id = 0,
    Person,
    Position,
    Contents,
    Command
}

public enum Position
{
    Left = 0,
    Bottom,
    Right,
    empty,
}

public enum Chara
{
    Yaku = 0,
    Ai_urei,
    Ai_normal,
    Ai_namidame,
    Kiriya,
    empty,
    Ai_movie,
    Ai_Smile,
    Haru_Movie,
    Kiriya_Dummy,
    Ai_Gaman,
    Ai_NoLight_urei,
    Ai_NoLight_namidame,
    Ai_NoLight_normal,
    Ai_NoLight_Smile,
    NoName,
}

public static class CharaExt
{
    public static string GetCharaSpriteName(this Chara type)
    {
        switch (type)
        {

            default:
                return type.ToString();
        }
    }
}

[DefaultExecutionOrder(-1)]
public class XMLLoad:SingletonMonoBehaviour<XMLLoad>
{
    public XmlSettings setting;
    
    private TextAsset[] xml;
    [System.NonSerialized]
    public ShowScript.ADVType[] advTypes;

    //xmlから読み込まれたデータを入れる場所
    public List<ScenarioData> data = new List<ScenarioData>();

    private void Start()
    {
        xml = setting.xml;
        advTypes = setting.advTypes;
    }

    //読み込み開始
    public void StartLoad()
    {
        for (int i = 0; i < xml.Length; i++)
        {
            if(xml[i] != null)
            {
                LoadXml(xml[i].text);
            }
            else
            {
                Debug.LogAssertion("XMLLoadのXml入れがnullです");
            }
        }
    }

    //xmlを読み込む本体
    private void LoadXml(string xmlText)
    {
        List<Chara> personList = new List<Chara>();
        List<Position> positionList = new List<Position>();
        List<string> contentsList = new List<string>();
        List<string> commandList = new List<string>();

        var xml = new XmlDocument();
        //ここのLoadXmlは別物
        xml.LoadXml(xmlText);

        XmlElement element = xml.DocumentElement;

        personList = Person_ListChange(SetList(XMLIndex.Person, element));
        positionList = Position_ListChange(SetList(XMLIndex.Position, element));
        contentsList = SetList(XMLIndex.Contents, element);
        commandList = SetList(XMLIndex.Command, element);

        ScenarioData result = new ScenarioData();

        result.Set_XmlElement(element);
        result.Set_PersonList(personList);
        result.Set_PositionList(positionList);
        result.Set_ContentsList(contentsList);
        result.Set_CommandList(commandList);

        data.Add(result);
    }

    //リストを返す
    private List<string> SetList(XMLIndex index, XmlElement element)
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

        //カラータグを変換する
        if(index == XMLIndex.Contents)
        {

        }
        return list;
    }

    //リストの変換
    //Parseに失敗したら、emptyを代わりに代入する
    private List<Chara> Person_ListChange(List<string> str)
    {
        List<Chara> result = new List<Chara>();
        for(int i = 0; i < str.Count; i++)
        {
            try
            {
                switch (str[i])
                {
                    case "藍":
                    case "Ai_urei":
                        result.Add(Chara.Ai_urei);
                        break;
                    case "Ai_normal":
                        result.Add(Chara.Ai_normal);
                        break;
                    case "Ai_namidame":
                        result.Add(Chara.Ai_namidame);
                        break;
                    case "？？？":
                        result.Add(Chara.empty);
                        break;
                    case "映像の中の藍":
                        result.Add(Chara.Ai_movie);
                        break;
                    case "Ai_Smile":
                        result.Add(Chara.Ai_Smile);
                        break;
                    case "映像の中の晴":
                        result.Add(Chara.Haru_Movie);
                        break;
                    case "霧谷柊晴":
                        result.Add(Chara.Kiriya);
                        break;
                    case "霧谷柊晴_Dummy":
                        result.Add(Chara.Kiriya_Dummy);
                        break;
                    case "Ai_gaman":
                        result.Add(Chara.Ai_Gaman);
                        break;
                    case "Ai_NoLight_urei":
                        result.Add(Chara.Ai_NoLight_urei);
                        break;
                    case "Ai_NoLight_namidame":
                        result.Add(Chara.Ai_NoLight_namidame);
                        break;
                    case "Ai_NoLight_normal":
                        result.Add(Chara.Ai_NoLight_normal);
                        break;
                    case "Ai_NoLight_smile":
                        result.Add(Chara.Ai_NoLight_Smile);
                        break;
                    case "Void":
                        result.Add(Chara.NoName);
                        break;
                    default:
                        if (Enum.IsDefined(typeof(Chara), str[i]))
                        {
                            result.Add((Chara)Enum.Parse(typeof(Chara), str[i], true));
                        }
                        else
                        {
                            result.Add(Chara.Kiriya);
                        }//？？？
                        break;
                }
            }
            catch
            {
                Debug.LogAssertion("キャラ画像の名前が正しくありません");
                result.Add(Chara.empty);
            }
        }
        return result;
    }

    private List<Position> Position_ListChange(List<string> str)
    {
        List<Position> result = new List<Position>();
        for (int i = 0; i < str.Count; i++)
        {
            try
            {
                result.Add((Position)Enum.Parse(typeof(Position), str[i], true));
            }
            catch
            {
                Debug.LogAssertion("ポジションの名前が正しくありません");
                result.Add(Position.empty);
            }
        }
        return result;
    }
    
    //デバッグ用で置いてる(けど、もしかして動かなくなった？)
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
    }
}