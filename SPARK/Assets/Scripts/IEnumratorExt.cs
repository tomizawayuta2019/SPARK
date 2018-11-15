using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IEnumratorExt{

    public static IEnumerator Wait(float time,System.Action comp) {
        yield return new WaitForSeconds(time);
        comp();
    }
}
