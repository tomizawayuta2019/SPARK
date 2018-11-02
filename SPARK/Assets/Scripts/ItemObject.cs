using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ItemObject : MonoBehaviour {
    [SerializeField]
    Rigidbody2D rig;
    private const string PLAYER_TAG = "Player";
    [SerializeField]
    private ItemState state;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != PLAYER_TAG) { return; }
        GetItem();
    }

    private void GetItem() {
        ItemManager.instance.GetItem(state);
        Destroy(gameObject);
    }
}
