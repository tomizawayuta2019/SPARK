using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDrop : Haru {
    [SerializeField]
    GameObject cameraTarget;
    [SerializeField]
    GameObject target;

    public override void MoveStart() {
        if (isMoveStart) { return; }
        //EventCamera.instance.StartEventCamera(cameraTarget, () => isMoveStart = true);
        EventCamera.instance.StartEventCamera(cameraTarget, () => {
            haruADV.SetActive(true);
            target.SetActive(true);
            Vector3 pos = target.transform.position;
            target.transform.SetParent(null);
            target.transform.position = pos;
        } );
    }

    protected override void MoveEnd()
    {
        base.MoveEnd();
        EventCamera.instance.EndEventCamera();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            MoveStart();
        }
    }
}
