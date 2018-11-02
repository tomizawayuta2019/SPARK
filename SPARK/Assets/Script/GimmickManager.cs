using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickManager : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        ClickInput();	
	}
    private void ClickInput()
    {
        //マウスクリックの判定
        if (!Input.GetMouseButtonDown(0)) return;
        //クリックされた位置を取得
        var tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Collider2D上クリックの判定
        if (!Physics2D.OverlapPoint(tapPoint)) return;
        //クリックされた位置のオブジェクトを取得
        var hitObject = Physics2D.Raycast(tapPoint, -Vector3.up);
        if (!hitObject) return;
        //クリックされたギミックスクリプトを取得
        var gimmick = hitObject.collider.gameObject.GetComponent<GimmickKind>();
        if (!gimmick) return;
        Debug.Log(gimmick);
        //クリックされたギミックの判定をする
        //switch (gimmick.gimmickKind)
        //{
        //    case GimmickKind.Kind.password:
        //        gimmick.ActiveSelfObject();
        //        break;
        //    case GimmickKind.Kind.door:

        //        break;
        //    case GimmickKind.Kind.item: break;
        //    default: break;
        //}
        gimmick.Click();
    }
}
