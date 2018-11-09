using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 表示中はプレイヤーの移動を制限するUI
/// </summary>
public class OverViewUI : MonoBehaviour {

    private void OnEnable()
    {
        UIController.instance.list.Add(this);
    }

    private void OnDisable()
    {
        UIController.instance.list.Remove(this);
    }
}
