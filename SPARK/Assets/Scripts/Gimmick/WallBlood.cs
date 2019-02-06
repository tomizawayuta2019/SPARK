using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlood : GimmickKind {

    [SerializeField]
    GameObject dropItem;

    private void Start()
    {
        CallEvent();
    }

    /// <summary>
    /// 文字を出す際のSE
    /// </summary>
    public void CallSE() {
        SEController.instance.PlaySE(SEController.SEType.button);
    }

    /// <summary>
    /// 文字表示イベントの開始
    /// </summary>
    private void CallEvent() {
        GetComponent<Animator>().SetTrigger("EventStart");
    }

    public override void Click()
    {
        base.Click();

        //文字の説明ADV
        ShowScript.instance.EventStart(ShowScript.ADVType.RedMessage_Read);

        DropItem();
    }

    public void DropItem() {
        dropItem.SetActive(true);
    }
}
