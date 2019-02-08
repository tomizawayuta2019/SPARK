using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDestroyEvent : MonoBehaviour {

    public enum Type {
        Start,
        Dest,
        Enter
    }

    private bool isEnterd;
    [SerializeField]
    private Type type;
    [SerializeField] bool isLoop;
    private bool IsEnter { get { return isLoop || !isEnterd; } }//イベントが発生するか

    private void Start()
    {
        if (type != Type.Start) { return; }
        Enter();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type != Type.Enter) { return; }

        if (IsEnter && collision.gameObject.tag == "Player") {
            Enter();
        }
    }

    private void OnDestroy()
    {
        if (type != Type.Dest) { return; }
        Enter();
    }

    private void Enter() {
        if (!IsEnter) { return; }
        if (PlayerController.instance == null) { return; }
        PlayerController.instance.MonsterDestroyEvent();
        isEnterd = true;
    }
}
