using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour {
    private const string PLAYER_TAG = "Player";
    [SerializeField]
    public ItemState state;

    public virtual void GetItem() {
        SEController.instance.PlaySE(SEController.SEType.get_item);
        ItemBagController.instance.PutInItemBag(this);
        ChildEffect child = GetComponent<ChildEffect>();
        if (child != null) { child.RemoveParent(); }
        Destroy(gameObject);
    }
}
