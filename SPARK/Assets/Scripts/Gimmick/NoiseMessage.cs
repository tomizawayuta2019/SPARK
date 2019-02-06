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

    System.Action compCallback = null;

    public void NoiseEventStart() {
        noise.gameObject.SetActive(true);
        message.gameObject.SetActive(true);
        StartCoroutine(Event());
    }

    public void NoiseEventStart(System.Action comp)
    {
        compCallback = comp;
        NoiseEventStart();
    }

    IEnumerator Event() {
        yield return StartCoroutine(noise.ChangeColor(colors[0], colors[1], 2));
        yield return StartCoroutine(message.ObjDisp());
        yield return StartCoroutine(noise.ChangeColor(colors[1], colors[2], 2));
        yield return StartCoroutine(noise.ChangeColor(colors[2], colors[0], 2));

        if (compCallback != null) {
            compCallback();
            compCallback = null;
        }

        noise.gameObject.SetActive(false);
        message.gameObject.SetActive(false);
    }
}
