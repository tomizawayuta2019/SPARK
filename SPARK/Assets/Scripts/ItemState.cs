using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムのイベントリスト（仮）
/// </summary>
public enum ItemEvent
{
    TransformItem,//アイテムが変化する
    GimmickEnter,//ギミックを作動させる
}

//仮のアイテム構造体
public struct ItemState
{
    public string itemName;
    public int itemID;

    public Sprite sprite;
    public string itemText;

    /// <summary>
    /// このアイテムに対して使用出来るアイテムID
    /// </summary>
    public int needItemID;

    /// <summary>
    /// 引数のアイテムが使用可能か否か
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    public bool IsCanUseItem(int itemID)
    {
        return needItemID == itemID;
    }
}