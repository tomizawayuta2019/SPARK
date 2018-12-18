﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDrop : Haru {
    [SerializeField]
    GameObject cameraTarget;
    [SerializeField]
    GameObject target;

    public override void MoveStart() {
        if (isMoveStart) { return; }
        System.Action comp = () => {
            haruADV.SetActive(true);
            target.SetActive(true);
            Vector3 pos = target.transform.position;
            target.transform.SetParent(null);
            target.transform.position = pos;
        };
        //EventCamera.instance.StartEventCamera(cameraTarget, () => isMoveStart = true);
        if (cameraTarget != null) { EventCamera.instance.StartEventCamera(cameraTarget, comp); }
        else { comp(); }
    }

    protected override void MoveEnd()
    {
        base.MoveEnd();
        EventCamera.instance.EndEventCamera();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            MoveStart();
        }
    }
}
