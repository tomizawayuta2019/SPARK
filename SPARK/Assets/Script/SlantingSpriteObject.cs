using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlantingSpriteObject : MonoBehaviour {
    [SerializeField]
    int leftSide;//プレイヤーが右側にいる場合のレイヤー
    [SerializeField]
    int rightSide;//プレイヤーが左側にいる場合のレイヤー

    SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void Update () {
        rend.sortingOrder = transform.position.x < PlayerController.instance.transform.position.x ? leftSide : rightSide;
	}
}
