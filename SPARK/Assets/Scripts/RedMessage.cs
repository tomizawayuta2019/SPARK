﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMessage : MonoBehaviour {
    [System.Serializable]
    struct OBjAndFloat
    {
        public GameObject obj;
        public float time;//前回の表示から何秒で対象のオブジェクトを表示するか
    }
    [SerializeField]
    OBjAndFloat[] textObjects;
    [SerializeField]
    CanvasGroup group;

    private void Start()
    {
        foreach (var item in textObjects) {
            item.obj.SetActive(false);
        }
    }

    public IEnumerator ObjDisp() {
        float time = 0;
        foreach (var item in textObjects) {
            while (time < item.time) {
                yield return null;
                time += TimeManager.DeltaTime;
            }
            item.obj.SetActive(true);
            time -= item.time;
        }
    }

    public void SetAlpha(float value)
    {
        group.alpha = value;
    }
}
