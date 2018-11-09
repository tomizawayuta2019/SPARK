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

public enum ItemType {
    match,
    diary,
}

//仮のアイテム構造体
[System.Serializable]
public struct ItemState
{
    /// <summary>
    /// アイテム名称
    /// </summary>
    public string itemName;

    /// <summary>
    /// アイテムID
    /// </summary>
    public int itemID;

    /// <summary>
    /// アイテム画像
    /// </summary>
    public Sprite sprite;

    /// <summary>
    /// アイテム説明文
    /// </summary>
    public string itemText;

    /// <summary>
    /// このアイテムに対して使用出来るアイテムID
    /// </summary
    [SerializeField]
    int[] needItemsID;

    /// <summary>
    /// このアイテムが使用出来るギミックID
    /// </summary>
    [SerializeField]
    int[] targetGimmick;

    /// <summary>
    /// 引数のアイテムが使用可能か否か
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    public bool IsCanUseItem(int itemID)
    {
        for (int i = 0; i < needItemsID.Length; i++)
        {
            if (needItemsID[i] == itemID) { return true; }
        }
        return false;
    }

    /// <summary>
    /// 対象のギミックに使用出来るか否か
    /// </summary>
    /// <param name="gimmickID"></param>
    /// <returns></returns>
    public bool IsTargetItem(int gimmickID) {
        for (int i = 0; i < targetGimmick.Length; i++) {
            if (targetGimmick[i] == gimmickID) { return true; }
        }
        return false;
    }
}