using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDestroyEvent : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            PlayerController.instance.MonsterDestroyEvent();
            Destroy(gameObject);
        }
    }
}
