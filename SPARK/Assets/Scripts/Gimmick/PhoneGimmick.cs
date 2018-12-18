using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneGimmick : GimmickKind {
    bool isPlayer = false;
    [SerializeField]
    GameObject ADV;
    [SerializeField]
    ShowScript show;

    public override void Click()
    {
        if (!isPlayer) { return; }
        base.Click();

        UIController.instance.list.Add(gameObject);
    }

    IEnumerator Event() {
        ADV.SetActive(true);
        show.Restart();

        while (ADV.activeSelf)
        {
            yield return null;
        }

        GameObject se = SEController.instance.PlaySE(SEController.SEType.doll).gameObject;

        while (se != null) {
            yield return null;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { isPlayer = true; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") { isPlayer = false; }
    }
}
