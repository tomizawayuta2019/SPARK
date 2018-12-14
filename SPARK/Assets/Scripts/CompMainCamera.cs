using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラを追従するクラス
/// </summary>
public class CompMainCamera : CameraViewChangeObject {
    private Vector3 defPos;

    protected override void Awake()
    {
        base.Awake();
        defPos = transform.localPosition;
        transform.SetParent(null);
    }

    // Update is called once per frame
    public override void LateUpdate () {
        if (Camera.main == null) { return; }

        transform.position = Camera.main.transform.position + defPos;
	}
}