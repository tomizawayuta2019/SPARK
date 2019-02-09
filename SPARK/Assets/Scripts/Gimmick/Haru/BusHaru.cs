using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusHaru : MonoBehaviour {
    [SerializeField]
    AlphaChange anim;

    [SerializeField]
    Animator animator;

    [SerializeField] ShowScript.ADVType ADVType;

    private bool isEventEnd = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEventEnd || collision.tag != "Player") { return; }
        anim.Play(() => { EventEnd(); });
        isEventEnd = true;
    }

    public void EventEnd()
    {
        ShowScript.instance.EventStart(ADVType);
    }
}
