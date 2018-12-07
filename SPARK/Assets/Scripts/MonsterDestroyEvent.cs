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

    private void Start()
    {
        if (type != Type.Start) { return; }
        Enter();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type != Type.Enter) { return; }

        if (!isEnterd && collision.gameObject.tag == "Player") {
            Enter();
        }
    }

    private void OnDestroy()
    {
        if (type != Type.Dest) { return; }
        Enter();
    }

    private void Enter() {
        if (isEnterd) { return; }
        PlayerController.instance.MonsterDestroyEvent();
        isEnterd = true;
    }
}
