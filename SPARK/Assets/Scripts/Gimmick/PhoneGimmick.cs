using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneGimmick : GimmickKind {
    bool isPlayer = false;
    [SerializeField]
    GameObject ADV;
    [SerializeField]
    ShowScript show;
    [SerializeField]
    NoiseMessage noise;

    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite changeSprite;

    [SerializeField]
    AudioClip phoneSE;

    [SerializeField]
    MessageDrop messageDrop;

    [SerializeField]
    GameObject returnPos;

    bool isEvent = false;

    public override void Click()
    {
        if (!isPlayer || isEvent) { return; }
        isEvent = true;
        base.Click();

        UIController.instance.list.Add(gameObject);
        StartCoroutine(Event());
    }

    IEnumerator Event() {
        ADV.SetActive(true);
        show.Restart();

        while (ADV.activeSelf) {
            yield return null;
        }

        GameObject se = SEController.instance.PlaySE(SEController.SEType.button).gameObject;

        while (se != null) {
            yield return null;
        }

        noise.NoiseEventStart(() => {
            SEController.instance.PlaySE(SEController.SEType.button);
            UIController.instance.list.Remove(gameObject);
            var p = PlayerController.instance;
            spriteRenderer.sprite = changeSprite;
            p.targetPosition = p.transform.position + new Vector3(-2, 0, 0);
            StartCoroutine(NextEvent());
        });
    }

    IEnumerator NextEvent() {
        yield return new WaitForSeconds(1.0f);
        messageDrop.gameObject.SetActive(true);

        SpriteRenderer sr = messageDrop.GetComponent<SpriteRenderer>();
        Color color = sr.color;
        color.a = 0;
        sr.color = color;

        float time = 0;
        while (time < 1) {
            time += Time.deltaTime;
            color.a = time;
            sr.color = color;
            yield return null;
        }

        color.a = 1;
        sr.color = color;

        messageDrop.MoveStart();

        returnPos.SetActive(false);
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
