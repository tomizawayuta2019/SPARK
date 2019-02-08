using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが近づいた時に流れるADV
/// </summary>
public class ApprochADV : MonoBehaviour {
    [SerializeField] ShowScript.ADVType ADVType;
    bool isShow = true;//既に起動されたか

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isShow) { return; }
        if (collision.tag != "Player") { return; }

        ShowScript.instance.EventStart(ADVType);
        isShow = false;
    }
}
