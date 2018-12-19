using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ScriptableObjectでキャラの情報と画像を入れておく

[CreateAssetMenu(menuName = "SPARK/Create CharaTable", fileName = "CharaTable")]
public class CharaTable : ScriptableObject
{
    public List<CharaInfo> characters = new List<CharaInfo>();

    public string GetChara(Chara chara)
    {
        string name = "";
        for(int i = 0; i < characters.Count; i++)
        {
            if(characters[i].callName == chara)
            {
                name = characters[i].name;
                break;
            }
        }
        return name;
    }

    public Sprite GetCharaImage(Chara chara, int id)
    {
        Sprite result = null;
        if(chara == Chara.empty)
        {
            return null;
        }
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].callName == chara)
            {
                result = characters[i].charaImages[id];
            }
        }
        return result;
    }

    public Vector2 Scale(Chara chara)
    {
        Vector2 result = new Vector2();
        if (chara == Chara.empty)
        {
            return result;
        }
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].callName == chara)
            {
                result = characters[i].scale;
            }
        }
        return result;
    }
}

[System.Serializable]
public class CharaInfo
{
    [Header("名前")]
    public string name;
    [Header("呼び出し名")]
    public Chara callName;
    [Header("キャラ表情差分")]
    public Sprite[] charaImages;
    [Header("Scale調整")]
    public Vector2 scale;
}
