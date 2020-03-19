using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLight : GimmickKind {
    [SerializeField]
    public bool isEvent = false;
    [SerializeField]
    float animTime;
    [SerializeField]
    Animator anim;
    [SerializeField] GameObject Image;

    public void EventStart() {
        isEvent = true;
        Image.SetActive(true);
        anim.SetBool("isFlash", true);
    }

    public override void Click()
    {
        if (isEvent) {
            Image.SetActive(false);
            UIController.instance.list.Add(gameObject);
            PlayerController.instance.ClickLight();
            anim.SetTrigger("Flash");
            anim.SetBool("isFlash", false);
            GimmickMonster.Instance.Stop();
            StartCoroutine(IEnumratorExt.Wait(animTime, () =>
            {
                UIController.instance.list.Remove(gameObject);
                StartCoroutine(GimmickMonster.Instance.DeadMonster(2.0f));
                PlayerController.instance.SetPlayerActive(true);
            }));
            isEvent = false;

        }
        base.Click();
    }

}
