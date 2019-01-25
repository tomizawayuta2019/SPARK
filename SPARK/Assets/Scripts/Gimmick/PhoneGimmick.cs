using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneGimmick : GimmickKind {
    bool isPlayer = false;
    [SerializeField]
    NoiseMessage noise;

    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite changeSprite;

    [SerializeField]
    AudioClip phoneSE;

    [SerializeField]
    AudioSource audio;

    [SerializeField]
    MessageDrop messageDrop;

    [SerializeField]
    GameObject returnPos;

    bool isEvent = false;

    private void Start()
    {
        audio.clip = phoneSE;
        audio.Play();
    }

    public override void Click()
    {
        if (!isPlayer || isEvent) { return; }
        isEvent = true;
        base.Click();

        UIController.instance.list.Add(gameObject);
        StartCoroutine(Event());
    }

    IEnumerator Event() {
        audio.clip = null;
        SEController.instance.PlaySE(SEController.SEType.phone_get);
        ShowScript.instance.EventStart(ShowScript.ADVType.Phone_Start);

        while (ShowScript.instance.GetIsShow()) {
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
            PlayerController.instance.StartCoroutine(NextEvent());
        });
    }

    IEnumerator NextEvent() {
        yield return new WaitForSeconds(1.0f);
        returnPos.SetActive(false); Destroy(returnPos);
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

        if (returnPos != null)
        {
            returnPos.SetActive(false);
            Destroy(returnPos);
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
