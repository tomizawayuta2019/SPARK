using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBagController : MonoBehaviour {
    float itemBagSpeed;//移動スピード
    Vector3 mousePosition;//マオス今のポジション
    Vector3 itemBagPosition;//アイテム欄今のポジション
    Image[] items = new Image[8];

    [SerializeField]
    Image prefabImage;
    // Use this for initialization

    public bool itemBagActive = true;

    public void PutInItemBag(GameObject nowItem)
    {
        for(int i=0; i < 8; i++)
        {
            if (items[i] == null)
            {
                //Vector3
                //nowItem.transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.1f);

                items[i] = Instantiate(prefabImage, transform.position, transform.rotation, transform) as Image;
                items[i].transform.Translate(-800+200 * i, 0, 0);
                items[i].name = nowItem.name;
                string itemSpriteName = nowItem.name;
                items[i].GetComponent<Image>().sprite = Resources.Load("GameSprite/" + itemSpriteName, typeof(Sprite)) as Sprite;
                break;
            }
        }
    }
    void SetInitializationPosition()//ポジション初期化
    {
        this.gameObject.transform.position = GameObject.Find("Canvas").transform.position;
        this.gameObject.transform.Translate(0, 640, 0);
    }
    
    void PositonChange()//マオスポジションによるアイテム欄ポジション変化
    {
        if (!itemBagActive) { return; }
        mousePosition = Input.mousePosition;
        if (mousePosition.y >= 880.0f&&mousePosition.y<=1080.0f)
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
	public void ItemBagUpdate () {
        itemBagPosition = this.gameObject.transform.position;
        PositonChange();
    }
}
