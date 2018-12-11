using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour {
    private const string PLAYER_TAG = "Player";
    [SerializeField]
    public ItemState state;

    public void GetItem() {
        ItemBagController.instance.PutInItemBag(this);
        ChildEffect child = GetComponent<ChildEffect>();
        if (child != null) { child.RemoveParent(); }
        Destroy(gameObject);
    }
}
