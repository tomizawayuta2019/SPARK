using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBagControllr : MonoBehaviour {
    float itemBagSpeed;//移動スピード
    Vector3 mousePosition;//マオス今のポジション
    Vector3 itemBagPosition;//アイテム欄今のポジション
    // Use this for initialization

    void SetInitializationPosition()//ポジション初期化
    {
        this.gameObject.transform.position = GameObject.Find("Canvas").transform.position;
        this.gameObject.transform.Translate(0, 640, 0);
    }

    void PositonChange()//マオスポジションによるアイテム欄ポジション変化
    {
        mousePosition = Input.mousePosition;
        if (mousePosition.y >= 880.0f)
        {
            if (this.gameObject.transform.position.y > 980)
            {
                this.transform.Translate(0,-itemBagSpeed,0);
            }
        }
        else
        {
            if (this.gameObject.transform.position.y < 1180)
            {
                this.transform.Translate(0, itemBagSpeed, 0);
            }
        }
    }
    void Start () {
        itemBagSpeed = 4.0f;
        SetInitializationPosition();
	}
	
	// Update is called once per frame
	void Update () {
        itemBagPosition = this.gameObject.transform.position;
        PositonChange();
    }
}
