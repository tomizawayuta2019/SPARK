using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムを使用する対象が継承するinterface
/// </summary>
public interface IItemUse {

    /// <summary>
    /// アイテムが使用できるか確認する処理
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    bool IsCanUseItem(ItemState item);

    /// <summary>
    /// アイテムを使用する処理
    /// </summary>
    /// <param name="item"></param>
    /// <returns>使用したアイテムが消費されるか否か</returns>
    bool ItemUse(ItemState item);
}

public class ItemManager : SingletonMonoBehaviour<ItemManager> , IItemUse{
    [SerializeField]
    private List<ItemState> items = new List<ItemState>();
    UIItem dragItem;
    IItemUse targetObject;

    public void GetItem(ItemState item)
    {
        items.Add(item);
    }

    public void UseItem(ItemState item)
    {
        items.Remove(item);
    }

    public void ItemDragStart(UIItem item) {
        dragItem = item;
    }

    public void ItemDragEnd(UIItem item) {
        if (dragItem != item) { return; }

        //アイテムが使用できるか確認する
        if (targetObject == null || targetObject.IsCanUseItem(item.item)) {
            dragItem = null;
            return;
        }

        if (targetObject.ItemUse(item.item)) {
            Destroy(dragItem.gameObject);
        }

        dragItem = null;
    }

    private void Update()
    {
        if (dragItem != null) {
            dragItem.Drag();
        }
    }

    public bool IsCanUseItem(ItemState item)
    {
        return false;
    }

    public bool ItemUse(ItemState item) {
        return false;
    }
}
