using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickManager : SingletonMonoBehaviour<GimmickManager> {
    
	// Update is called once per frame
	void Update () {
        ClickInput();
	}

    private void ClickInput()
    {
        if (UIController.instance && !UIController.instance.isCanInput || ShowScript.instance.GetIsShow()) { return; }
        //マウスクリックの判定
        if (!Input.GetMouseButtonDown(0)) return;

        GimmickKind gimmick = MouseExt.GetMousePosGimmick<GimmickKind>();
        if (gimmick == null) { return; }

        if (PlayerController.instance == null || gimmick.isClickOnly)
        {
            gimmick.Click();
        }
        else {
            StartCoroutine(PlayerController.instance.WaitForMove(() => gimmick.Click()));
        }
    }

    //public static GimmickKind GetMousePosGimmick()
    //{
    //    //クリックされた位置を取得
    //    var tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    GimmickKind gimmick = null;
    //    RaycastHit2D hit;
    //    List<GameObject> hitObjects = new List<GameObject>();

    //    do
    //    {
    //        hit = Physics2D.Raycast(tapPoint, -Vector3.up);
    //        if (hit.collider == null) { break; }
    //        gimmick = hit.collider.GetComponent<GimmickKind>();
    //        hit.collider.gameObject.SetActive(false);
    //        hitObjects.Add(hit.collider.gameObject);
    //    } while (gimmick == null);

    //    foreach (var item in hitObjects) { item.SetActive(true); }

    //    return gimmick;
    //}
}
