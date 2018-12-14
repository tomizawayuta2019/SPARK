using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラを追従するクラス
/// </summary>
public class CompMainCamera : MonoBehaviour {
    private Vector3 defPos;

    private void Awake()
    {
        defPos = transform.localPosition;
        transform.SetParent(null);
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (Camera.main == null) { return; }

        transform.position = Camera.main.transform.position + defPos;
	}
}