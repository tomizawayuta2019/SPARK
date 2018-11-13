using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaChange : MonoBehaviour {
    [SerializeField]
    private bool isPlayOnAwake;
    [SerializeField]
    float start;
    [SerializeField]
    float end;
    [SerializeField]
    float time;
    [SerializeField]
    bool isLoop;
    [SerializeField]
    float loopDelay;

    private void Awake()
    {
        if (!isPlayOnAwake) { return; }
        Play();
    }

    private void OnEnable()
    {
        if (!isPlayOnAwake) { return; }
        Play();
    }

    public void Play() {
        Debug.Log("play");
        StartCoroutine(AlphaChangeDelay(start,end));
    }

    IEnumerator AlphaChangeDelay(float start,float end) {
        Image image = GetComponent<Image>();
        Color color = image.color;
        color.a = start;
        image.color = color;

        float nowTime = Time.deltaTime;
        while (nowTime < time)
        {
            color.a = Mathf.Lerp(start, end, nowTime / time);
            image.color = color;
            yield return null;
            nowTime += Time.deltaTime;
            Debug.Log("delay");
        }

        color.a = end;
        image.color = color;
        Debug.Log("end");

        if (isLoop) {
            yield return new WaitForSeconds(loopDelay);
            StartCoroutine(AlphaChangeDelay(end, start));
        }
    }
}
