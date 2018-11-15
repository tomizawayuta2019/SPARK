using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScript : MonoBehaviour {
    Vector3 defPos;

    private void Awake()
    {
        defPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update () {
        transform.SetParent(Camera.main.transform);
        transform.localPosition = defPos;
	}
}
