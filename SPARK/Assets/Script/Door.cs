using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : GimmickKind ,ISwitchObject
{
    //外から読み込み用
    [SerializeField]
    GameObject player;
    private bool checkPlayer = false;
    [SerializeField]
    float range = 2.0f;

    [SerializeField]
    bool isOpen = false;
    public bool IsOpen { get { return isOpen; } }

    [SerializeField]
    GameObject doorWorningADV;

    //クリックされたら
    public override void Click()
    {
        if (!isOpen) {
            //ADV表示
            doorWorningADV.gameObject.SetActive(true);
            return;
        }
        //プレイヤーが範囲内だったら進む
        if (!checkPlayer) { return; }
        base.Click();
        //ワープさせる位置を取得
        Vector3 posX = new Vector3(gameObject.transform.position.x + range, gameObject.transform.position.y, player.transform.position.z);
        Vector3 posFripX = new Vector3(gameObject.transform.position.x - range, gameObject.transform.position.y, player.transform.position.z);
        //プレイヤーが扉のどっち側にいるか判定
        player.transform.position = (transform.position.x >= player.transform.position.x) ? posX:posFripX;
        PlayerController p = player.GetComponent<PlayerController>();
        p.targetPosition = player.transform.position + new Vector3((player.transform.position.x - transform.position.x) / 2, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            checkPlayer = true;
            player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            checkPlayer = false;
            player = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            //プレイヤーの移動停止
            player.GetComponent<PlayerController>().targetPosition = player.transform.position;
        }
    }

    public void SetValue(bool value)
    {
        isOpen = value;
    }
}