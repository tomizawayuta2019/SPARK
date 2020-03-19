using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetailADV : GimmickKind {
    [SerializeField] ShowScript.ADVType ADVType;

    private void Start()
    {
        //tag = "Detail";
    }

    public override void Click()
    {
        base.Click();
        ShowScript.instance.EventStart(ADVType);
    }
}
