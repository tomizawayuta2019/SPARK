using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaChange : DelayedChange<float> {
    [SerializeField]
    Image image;
    [SerializeField]
    SpriteRenderer sr;

    protected override void Awake()
    {
        image = GetComponent<Image>();
        sr = GetComponent<SpriteRenderer>();
        base.Awake();
    }

    protected override float LerpValue(float start, float end, float per)
    {
        return Mathf.Lerp(start, end, per);
    }

    protected override float UpdateValue(float value)
    {
        Color color;
        if (image != null)
        {
            color = image.color;
            color.a = value;
            image.color = color;
        }
        else {
            color = sr.color;
            color.a = value;
            sr.color = color;
        }
        return color.a;
    }

}
