using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クリックされたらUIを表示するUI
/// </summary>
public class GimmickUIView : GimmickKind {
    public GameObject target;

    public override void Click()
    {
        base.Click();
        target.SetActive(true);
    }
}
