﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustMounten : GimmickKind ,IItemUse {

    [SerializeField]
    GameObject item;

    public override void Click()
    {
        if (item == null) { return; }
        base.Click();

        ShowScript.instance.EventStart(ShowScript.ADVType.ItemCanUse_Knife);
    }

    public bool IsCanUseItem(ItemState item)
    {
        return this.item != null && item.itemType == ItemType.knife;
    }

    public bool ItemUse(ItemState item)
    {
        if (this.item == null) { return false; }
        ShowScript.instance.EventStart(ShowScript.ADVType.ItemUse_Knife);
        SEController.instance.PlaySE(SEController.SEType.knife_cut);
        this.item.gameObject.SetActive(true);
        this.item.gameObject.transform.SetParent(null);
        gameObject.SetActive(false);
        return true;
    }

}
