using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : GimmickKind
{
    //外から読み込み用
    [SerializeField]
    GameObject player;
    private bool checkPlayer = false;


    //クリックされたら
    public override void Click()
    {
        base.Click();
        //ワープさせる位置を取得
        Vector3 posX = new Vector3(gameObject.transform.position.x + 2.0f, gameObject.transform.position.y, player.transform.position.z);
        Vector3 posFripX = new Vector3(gameObject.transform.position.x - 2.0f, gameObject.transform.position.y, player.transform.position.z);
        //プレイヤーが範囲内だったら進む
        if (!checkPlayer) { return; }
        //プレイヤーが扉のどっち側にいるか判定
        player.transform.position = (transform.position.x >= player.transform.position.x) ? posX:posFripX;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            checkPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            checkPlayer = false;
        }
    }
}