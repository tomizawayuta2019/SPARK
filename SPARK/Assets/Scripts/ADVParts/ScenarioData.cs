using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

using UnityEngine;

/*
 *   複数生成できるやつ
 */

public class ScenarioData:MonoBehaviour
{
    private XmlElement element;

    private List<Chara> personList = new List<Chara>();
    private List<Position> positionList = new List<Position>();
    private List<string> contentsList = new List<string>();
    private List<string> commandList = new List<string>();

    public XmlElement Get_XmlElement()
    {
        return element;
    }

    public List<Chara> Get_PersonList()
    {
        return personList;
    }

    public List<Position> Get_PositionList()
    {
        return positionList;
    }

    public List<string> Get_ContentsList()
    {
        return contentsList;
    }

    public List<string> Get_CommandList()
    {
        return commandList;
    }

    public void Set_XmlElement(XmlElement x)
    {
        element = x;
    }

    public void Set_PersonList(List<Chara> x)
    {
        personList = x;
    }

    public void Set_PositionList(List<Position> x)
    {
        positionList = x;
    }

    public void Set_ContentsList(List<string> x)
    {
        contentsList = x;
    }

    public void Set_CommandList(List<string> x)
    {
        commandList = x;
    }

    //個々の情報を表示する
    public void ShowItems(XMLIndex index)
    {
        int length = 0;
        switch (index)
        {
            case XMLIndex.Person:
                length = Get_PersonList().Count;
                break;
            case XMLIndex.Position:
                length = Get_PositionList().Count;
                break;
            case XMLIndex.Contents:
                length = Get_ContentsList().Count;
                break;
            case XMLIndex.Command:
                length = Get_CommandList().Count;
                break;
            default:
                Debug.Log("indexがおかしいです");
                break;
        }
        for (int i = 0; i < length; i++)
        {
            switch (index)
            {
                case XMLIndex.Person:
                    Debug.Log(Get_PersonList()[i]);
                    break;
                case XMLIndex.Position:
                    Debug.Log(Get_PositionList()[i]);
                    break;
                case XMLIndex.Contents:
                    Debug.Log(Get_ContentsList()[i]);
                    break;
                case XMLIndex.Command:
                    Debug.Log(Get_CommandList()[i]);
                    break;
                default:
                    break;
            }
        }
    }
}