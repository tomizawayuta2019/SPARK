﻿using System.Collections;
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
    [SerializeField]
    float itemPosY;

    // Use this for initialization

    public bool itemBagActive = true;
    public bool IsItemBagView;//現在アイテムバッグを表示中か


    public GameObject itemsContains(string itemName)
    {
        bool flag = false;
        int num = 0;
        for (int i = 0; i < 8; i++)
        {
            if (items[i] != null)   
            {
                if (items[i].name == itemName)
                {
                    flag = true;
                    num = i; ;
                }
            }
        }
        if (flag == false)
        {

            return null;
        }
        else
        {
            return items[num].gameObject;
        }

    }



    public void PutInItemBag(ItemObject nowItem)
    {
        if (nowItem.state.itemType == ItemType.diary && items[0] != null)
        {
            itemController icon = items[0].gameObject.GetComponent<itemController>();
            icon.state.AddItemText(nowItem.state.itemText[0]);
            return;
        }

        for(int i=0; i < 8; i++)
        {
            if (items[i] == null)
            {
                //Vector3
                //nowItem.transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.1f);

                items[i] = Instantiate(prefabImage, transform.position, transform.rotation, transform) as Image;
                itemController item = items[i].gameObject.AddComponent<itemController>();
                ItemImage image = items[i].gameObject.AddComponent<ItemImage>();
                image.item = nowItem.state;
                item.state = nowItem.state;
                items[i].transform.Translate(-800 + 200 * i, itemPosY, 0);
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

        if (nowItem.state.type != ShowScript.ADVType.None && nowItem.state.type != ShowScript.ADVType.test)
        {
            //Debug.Log(nowItem.state.type);
            ShowScript.instance.EventStart(nowItem.state.type);
        }
    }
    void SetInitializationPosition()//ポジション初期化
    {
        this.gameObject.transform.position = GameObject.Find("Canvas").transform.position;
        this.gameObject.transform.Translate(0, 640, 0);
    }
    
    void PositonChange()//マオスポジションによるアイテム欄ポジション変化
    {
        //if (!itemBagActive) { return; }
        mousePosition = Input.mousePosition;
        //IsItemBagView = itemView.IsItemView || (mousePosition.y >= Screen.height * 0.8f && mousePosition.y <= Screen.height);
        float delta = GetComponent<RectTransform>().sizeDelta.y / 2;
        if (Input.GetMouseButtonDown(1)) { IsItemBagView = !IsItemBagView; }
        if (IsItemBagView)
        {
            if (this.gameObject.transform.position.y > Screen.height - delta)
            {
                this.transform.Translate(0,-itemBagSpeed,0);
            }
        }
        else
        {
            if (this.gameObject.transform.position.y < Screen.height + delta)
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
