using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Xml;
using UnityEngine;

/*
 *  XMLをロードしてシナリオデータを保持しておくスクリプト
 */

public enum XMLIndex
{
    Id = 0,
    Person,
    Position,
    Contents,
    Command
}

public class XMLLoad:MonoBehaviour
{
    //xmlファイルを入れる場所
    [SerializeField]
    private TextAsset[] xml;

    //xmlから読み込まれたデータを入れる場所
    public List<ScenarioData> data = new List<ScenarioData>();

    //早めに読まないとNull吐かれそうなのでAwakeで
    //あとはstatic化
    private void Awake()
    {
        DontDestroyOnLoad(this);
        StartLoad();
    }

    //他のスクリプトにデータを渡す用
    //ここの戻り値に複数の型は入るのか
    //public 

    //読み込み開始
    private void StartLoad()
    {
        for (int i = 0; i < xml.Length; i++)
        {
            LoadXml(xml[i].text);
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
                result.Add((Chara)Enum.Parse(typeof(Chara), str[i], true));
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