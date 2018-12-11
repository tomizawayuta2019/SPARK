using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LightStatus {
    public float range;
    public float intensity;
    public Color color;
}

public class LightChange : DelayedChange<LightStatus> {
    Light li;

    protected override void Awake()
    {
        li = GetComponent<Light>();
        base.Awake();
    }

    protected override LightStatus LerpValue(LightStatus start, LightStatus end, float per)
    {
        LightStatus value = new LightStatus();
        value.range = Mathf.Lerp(start.range, end.range, per);
        value.intensity = Mathf.Lerp(start.intensity, end.intensity, per);
        value.color = Color.Lerp(start.color, end.color, per);
        return value;
    }

    protected override LightStatus UpdateValue(LightStatus value)
    {
        if (li == null) { li = GetComponent<Light>(); }

        li.range = value.range;
        li.intensity = value.intensity;
        li.color = value.color;
        return value;
    }

}
