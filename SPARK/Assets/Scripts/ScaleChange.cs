using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChange : DelayedChange<Vector3> {

    protected override Vector3 UpdateValue(Vector3 value)
    {
        transform.localScale = value;
        return transform.localScale;
    }

    protected override Vector3 LerpValue(Vector3 start, Vector3 end, float per)
    {
        return Vector3.Lerp(start, end, per);
    }

}
