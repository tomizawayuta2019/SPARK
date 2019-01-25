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
    candle,
    candle_lighting,
    light,
    lighting,
    diary,
    diary_open,
    red_lighting,
    knife,
    brooch,
    piano,
    ticket,
    message,
}

public enum GimmickType {
    Player
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
    public ItemType itemType;

    /// <summary>
    /// アイテム画像
    /// </summary>
    public Sprite sprite;

    /// <summary>
    /// アイテム説明文
    /// </summary>
    [TextArea]
    public string[] itemText;

    /// <summary>
    /// このアイテムが使用出来るアイテムID
    /// </summary
    [SerializeField]
    ItemType[] targetItemsID;

    /// <summary>
    /// このアイテムが使用出来るギミックID
    /// </summary>
    [SerializeField]
    GimmickType[] targetGimmick;

    [SerializeField]
    public ItemObject exchangeItem;

    [SerializeField]
    SEController.SEType SEType;

    [SerializeField]
    public GameObject getADVObj,viewADVObj;

    /// <summary>
    /// 対象のアイテムに使用可能か否か
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    public bool IsCanUseItem(int itemID)
    {
        foreach (int item in targetItemsID) {
            if (item == itemID) { return true; }
        }
        return false;
    }

    /// <summary>
    /// 対象のギミックに使用出来るか否か
    /// </summary>
    /// <param name="gimmickID"></param>
    /// <returns></returns>
    public bool IsTargetItem(int gimmickID) {
        foreach (int item in targetGimmick) {
            if (item == gimmickID) { return true; }
        }
        return false;
    }

    public void Exchange() {
        if (exchangeItem == null) { return; }
        this = exchangeItem.state;
    }

    public AudioSource Use() {
        if (SEType == SEController.SEType.none) { return null; }
        return SEController.instance.PlaySE(SEType);
    }

    public void AddItemText(string value)
    {
        string[] swap = new string[itemText.Length + 1];

        for (int i = 0; i < itemText.Length; i++)
        {
            swap[i] = itemText[i];
        }

        swap[itemText.Length] = value;
        itemText = swap;
    }

    /// <summary>
    /// Textの行数を取得する処理
    /// </summary>
    /// <param name="index"></param>
    public float GetItemTextHight(int index)
    {
        return 100;
    }
}