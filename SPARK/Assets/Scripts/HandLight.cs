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

    public void EventStart() {
        isEvent = true;
    }

    public override void Click()
    {
        if (isEvent) {
            UIController.instance.list.Add(gameObject);
            Animator anim = PlayerController.instance.GetComponent<Animator>();
            anim.SetTrigger("LightAttack");
            anim.SetBool("isLight", false);
            StartCoroutine(IEnumratorExt.Wait(animTime, () =>
            {
                UIController.instance.list.Remove(gameObject);
                StartCoroutine(GimmickMonster.MonsterInstance.DeadMonster(0.1f));
            }));
            isEvent = false;
        }
        base.Click();
    }

}
