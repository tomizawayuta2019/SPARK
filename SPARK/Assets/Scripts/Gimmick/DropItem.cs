using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 落下した際のSE設定
/// </summary>
public class DropItem : MonoBehaviour {

    [SerializeField]
    SEController.SEType seType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Floor") { return; }

        SEController.instance.PlaySE(seType);
    }

}
