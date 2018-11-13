using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMessage : MonoBehaviour {
    [SerializeField]
    Noise noise;
    [SerializeField]
    RedMessage message;
    [SerializeField]
    Color[] colors = new Color[3];

    //private void Start()
    //{
    //    NoiseEventStart();
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") { return; }
        NoiseEventStart();
    }

    public void NoiseEventStart() {
        noise.gameObject.SetActive(true);
        message.gameObject.SetActive(true);
        StartCoroutine(Event());
    }

    IEnumerator Event() {
        yield return StartCoroutine(noise.ChangeColor(colors[0], colors[1], 2));
        yield return StartCoroutine(message.ObjDisp());
        yield return StartCoroutine(noise.ChangeColor(colors[1], colors[2], 2));
    }
}
