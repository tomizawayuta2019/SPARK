using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationChange : DelayedChange<Vector3> {

    protected override Vector3 LerpValue(Vector3 start, Vector3 end, float per)
    {
        return Vector3.MoveTowards(start, end, per);
    }

    protected override Vector3 UpdateValue(Vector3 value)
    {
        transform.localEulerAngles = value;
        return transform.localEulerAngles;
    }

}
