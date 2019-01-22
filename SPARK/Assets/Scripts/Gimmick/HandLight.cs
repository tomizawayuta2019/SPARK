using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLight : GimmickKind {
    [SerializeField]
    bool isEvent = false;
    [SerializeField]
    GameObject monster;
    [SerializeField]
    float animTime;
    [SerializeField]
    Animator anim;

    public void EventStart() {
        isEvent = true;
        anim.SetBool("isFlash", true);
    }

    public override void Click()
    {
        if (isEvent) {
            UIController.instance.list.Add(gameObject);
            PlayerController.instance.ClickLight();
            anim.SetTrigger("Flash");
            anim.SetBool("isFlash", false);
            StartCoroutine(IEnumratorExt.Wait(animTime, () =>
            {
                UIController.instance.list.Remove(gameObject);
                StartCoroutine(GimmickMonster.MonsterInstance.DeadMonster(2.0f));
                PlayerController.instance.SetPlayerActive(true);
            }));
            isEvent = false;
        }
        base.Click();
    }

}
