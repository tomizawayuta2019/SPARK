using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLight : GimmickKind {
    [SerializeField]
    bool isEvent = false;

    public void EventStart() {
        isEvent = true;
        UIController.instance.list.Add(gameObject);
    }

    public override void Click()
    {
        Debug.Log("click");
        if (isEvent) {
            StartCoroutine(GimmickMonster.MonsterInstance.DeadMonster(0.1f));
            isEvent = false;
            UIController.instance.list.Remove(gameObject);
        }
        base.Click();
    }

}
