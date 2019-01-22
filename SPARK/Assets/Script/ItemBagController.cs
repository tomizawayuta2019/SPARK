using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBagController : SingletonMonoBehaviour<ItemBagController> {
    [SerializeField]
    float itemBagSpeed;//移動スピード
    Vector3 mousePosition;//マオス今のポジション
    Vector3 itemBagPosition;//アイテム欄今のポジション
    Image[] items = new Image[8];
    public ItemView itemView;

    [SerializeField]
    Image prefabImage;
    [SerializeField]
    ItemObject diaryItem;

    itemController dialy;

    // Use this for initialization

    public bool itemBagActive = true;

    public void PutInItemBag(ItemObject nowItem)
    {
        for(int i=0; i < 8; i++)
        {
            if (items[i] == null)
            {
                //Vector3
                //nowItem.transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.1f);

                items[i] = Instantiate(prefabImage, transform.position, transform.rotation, transform) as Image;
                itemController item = items[i].gameObject.AddComponent<itemController>();
                item.state = nowItem.state;
                items[i].transform.Translate(-800 + 200 * i, 0, 0);
                items[i].name = nowItem.state.itemName;
                string itemSpriteName = nowItem.state.sprite.name;
                //items[i].GetComponent<Image>().sprite = Resources.Load("GameSprite/" + itemSpriteName, typeof(Sprite)) as Sprite;
                items[i].sprite = nowItem.state.sprite;
                break;
            }
        }

        if (nowItem.state.getADVObj != null) {
            nowItem.state.getADVObj.SetActive(true);
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
        if (mousePosition.y >= Screen.height * 0.8f && mousePosition.y <= Screen.height)
        {
            if (this.gameObject.transform.position.y > Screen.height * 0.9f)
            {
                this.transform.Translate(0,-itemBagSpeed,0);
            }
        }
        else
        {
            if (this.gameObject.transform.position.y < Screen.height * 1.1f)
            {
                this.transform.Translate(0, itemBagSpeed, 0);
            }
        }
    }
    void Start () {
        SetInitializationPosition();
        PutInItemBag(diaryItem);
        dialy = items[0].GetComponent<itemController>();
	}
	
	// Update is called once per frame
	public void ItemBagUpdate () {
        itemBagPosition = this.gameObject.transform.position;
        PositonChange();
    }

    public void ItemView(itemController item) {
        itemView.Open(item);
    }
}
