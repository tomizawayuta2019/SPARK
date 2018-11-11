using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScript : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        transform.SetParent(Camera.main.transform);
        transform.localPosition = Vector2.zero;
	}
}
