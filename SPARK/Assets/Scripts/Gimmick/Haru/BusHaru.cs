using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusHaru : MonoBehaviour {
    [SerializeField]
    AlphaChange anim;

    private bool isEventEnd = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEventEnd || collision.tag != "Player") { return; }
        anim.Play();
        isEventEnd = true;
    }
}
