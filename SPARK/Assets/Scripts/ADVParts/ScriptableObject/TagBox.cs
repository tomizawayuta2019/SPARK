using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TAGMODE
{
    NONE = 0,
    COLOR
}

[CreateAssetMenu(menuName ="SPARK/Create TagBox", fileName ="TagBox")]
public class TagBox : ScriptableObject
{
    [Header("終点タグの設定")]
    [Header("簡略化タグ")]
    public string endTag_Simple;

    [Header("色指定タグの設定")]
    public List<ColorTag> colorTag = new List<ColorTag>();
    [Header("色の終点タグ")]
    public string ColorEndTag;

    private TAGMODE tagMode = TAGMODE.NONE;

    public string TagCheck(string tag)
    {
        //ENDタグだったらENDを素材そのまま返す
        if(endTag_Simple == tag)
        {
            return endTag_Simple;
        }

        for(int i = 0; i < colorTag.Count; i++)
        {
            if(colorTag[i].simpleTag == tag)
            {
                tagMode = TAGMODE.COLOR;
                return "<color=#" + ColorUtility.ToHtmlStringRGB(colorTag[i].color) + ">";
                //return colorTag[i].regularTag;
            }
        }
        
        return null;
    }
}

[System.Serializable]
public class TagSet
{
    [Header("簡略化タグ")]
    public string simpleTag;
    [Header("正規タグ")]
    public string regularTag;
}

[System.Serializable]
public class ColorTag : TagSet
{
    [Header("色")]
    public Color color;
}