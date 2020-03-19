using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiItemObject : ItemObject {
    [SerializeField]
    ItemState secondItem;

    public override void GetItem()
    {
        base.GetItem();

        ItemState swap = state;
        state = secondItem;
        base.GetItem();
        state = swap;
    }
}
