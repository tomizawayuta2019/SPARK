using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class ItemObject : MonoBehaviour {
    [SerializeField]
    Rigidbody2D rig;
    private const string PLAYER_TAG = "Player";
    [SerializeField]
    public ItemState state;

    private void Awake()
    {
        if (rig == null) {
            rig = GetComponent<Rigidbody2D>();
        }
    }
}
