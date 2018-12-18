using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustMounten : GimmickKind ,IItemUse {
    [SerializeField]
    GameObject ADV;
    [SerializeField]
    ShowScript show;

    [SerializeField]
    ItemObject item;

    public override void Click()
    {
        if (item == null) { return; }
        base.Click();

        ADV.SetActive(true);
        show.Restart();
    }

    public bool IsCanUseItem(ItemState item)
    {
        return this.item != null && item.itemType == ItemType.knife;
    }

    public bool ItemUse(ItemState item)
    {
        if (this.item == null) { return false; }
        SEController.instance.PlaySE(SEController.SEType.button);
        this.item.gameObject.SetActive(true);
        this.item.gameObject.transform.SetParent(null);
        gameObject.SetActive(false);
        return true;
    }

}
