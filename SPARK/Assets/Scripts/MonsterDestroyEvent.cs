using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDestroyEvent : MonoBehaviour {

    private bool isEnterd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isEnterd && collision.gameObject.tag == "Player") {
            PlayerController.instance.MonsterDestroyEvent();
            isEnterd = true;
        }
    }
}
