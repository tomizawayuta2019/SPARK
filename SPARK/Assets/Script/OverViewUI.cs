using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 表示中はプレイヤーの移動を制限するUI
/// </summary>
public class OverViewUI : MonoBehaviour {

    protected virtual void OnEnable()
    {
        UIController.instance.list.Add(gameObject);
    }

    protected virtual void OnDisable()
    {
        //終了時にエラー吐くのでinstance確認
        if (UIController.instance) {
            UIController.instance.list.Remove(gameObject);
        }
    }
}
