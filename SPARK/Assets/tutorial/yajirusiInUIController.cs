using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yajirusiInUIController : SingletonMonoBehaviour<yajirusiInUIController>
{
    [SerializeField]
    GameObject pointer;
    [SerializeField]
    itemController target;
    [SerializeField]
    ItemBagController ItemBagController;
    [SerializeField]
    GameObject itemBag;
    [SerializeField]
    GameObject yajirushiItemView;
    [SerializeField]
    GameObject yajirushiToPlayer;
    [SerializeField]
    GameObject itemView;
    [SerializeField]
    GameObject itemViewImage;

    [SerializeField]
    GameObject Player;  
    [SerializeField]
    GameObject PlayerLight;
    [SerializeField]
    GameObject monster;
    [SerializeField]
    GameObject yajirushiToRight;
    [SerializeField]
    GameObject yajirushiToLeft;
    [SerializeField]
    GameObject tourouGif;
    [SerializeField]
    GameObject mouseGif;
    [SerializeField]
    GameObject mainCamera;
    CanvasGroup group;


    public string yajirushiItemViewTargert = "";
    public string yajirushiToPlayerTargert = "";
    Vector3 gob1Position = Vector3.zero;
    Vector3 itemPosition;
    int step = 0;
    GameObject diary_close;
    //int YajirusiFlag = 0;

    bool isStart;

    public void StartTutorial()
    {
        isStart = true;
    }

    public void yajirushiItemViewController()
    {
        this.target = itemView.GetComponent<ItemView>().target;
        if (step == 0)
        {
            yajirushiToRight.SetActive(true);
            if(ItemBagController.itemsContains("灯篭") != null && ItemBagController.itemsContains("マッチ") != null && ItemBagController.itemsContains("ロウソク") != null)
            {
                yajirushiToRight.SetActive(false);
                step = 1;
            }
        }

        if (step == 1)
        {
            if (PlayerLight.activeSelf == true)
            {
                step = 2;
            }
            if (ItemBagController.transform.position.y>=1080)
            {
                mouseGif.SetActive(true);
            }
            else
            {
                mouseGif.SetActive(false);
            }
        }
        //アイテム欄
        if (itemView.activeSelf == true)
        {
            ///ui
            if (target != null)
            {
                if (target.state.itemName == "ロウソク" && ItemBagController.itemsContains("マッチ") != null)
                {
                    yajirushiItemView.SetActive(true);

                    if (yajirushiItemViewTargert != "マッチ")
                    {
                        gob1Position = ItemBagController.itemsContains("マッチ").transform.position;
                        setYajirusi(yajirushiItemView, gob1Position, itemViewImage.transform.position, 500.0f, 200.0f);
                        yajirushiItemViewTargert = "マッチ";
                    }
                }
                else if (target.state.itemName == "ロウソク" && ItemBagController.itemsContains("マッチ") == null)
                {
                    yajirushiItemView.SetActive(false);
                }
                else if (target.state.itemName != "ロウソク" && ItemBagController.itemsContains("マッチ") != null)
                {
                    yajirushiItemView.SetActive(false);
                }

                if (target.state.itemName == "灯篭" && ItemBagController.itemsContains("ロウソク") != null && ItemBagController.itemsContains("マッチ") == null)
                {
                    yajirushiItemView.SetActive(true);

                    if (yajirushiItemViewTargert != "ロウソク")
                    {
                        gob1Position = ItemBagController.itemsContains("ロウソク").transform.position;
                        setYajirusi(yajirushiItemView, gob1Position, itemViewImage.transform.position, 500.0f, 200.0f);
                        yajirushiItemViewTargert = "ロウソク";
                    }
                }
                else if (yajirushiItemViewTargert == "ロウソク" && ItemBagController.itemsContains("マッチ") == null && ItemBagController.itemsContains("ロウソク") == null)
                {
                    yajirushiItemView.SetActive(false);
                }
                else if (target.state.itemName != "灯篭" && ItemBagController.itemsContains("ロウソク") != null && ItemBagController.itemsContains("マッチ") == null)
                {
                    yajirushiItemView.SetActive(false);

                }
            }
        }
        else
        {
            yajirushiItemView.SetActive(false);
        }

        //pointer
        if (ItemBagController.itemsContains("ロウソク") != null && ItemBagController.itemsContains("マッチ") != null)
        {
            pointer.SetActive(true);
            pointer.transform.position = ItemBagController.itemsContains("ロウソク").transform.position;
            if (target != null)
            {
                if (itemView.activeSelf == true && target.state.itemName == "ロウソク")
                {
                    pointer.SetActive(false);
                }
                else
                {
                    pointer.SetActive(true);
                }
            }
        }

        if (ItemBagController.itemsContains("灯篭") != null && ItemBagController.itemsContains("ロウソク") != null && ItemBagController.itemsContains("マッチ") == null)
        {
            pointer.SetActive(true);
            pointer.transform.position = ItemBagController.itemsContains("灯篭").transform.position;
            if (target != null)
            {
                if (itemView.activeSelf == true && target.state.itemName == "灯篭")
                {
                    pointer.SetActive(false);
                }
                else
                {
                    pointer.SetActive(true);
                }
            }
        }


        //プレイヤーに指し矢印
        if (ItemBagController.itemsContains("灯篭") != null && ItemBagController.itemsContains("マッチ") == null && ItemBagController.itemsContains("ロウソク") == null)
        {
            yajirushiToPlayer.SetActive(true);
            if (yajirushiToPlayerTargert != "player")
            {
                setYajirusi(yajirushiToPlayer, ItemBagController.itemsContains("灯篭").transform.position, Camera.main.WorldToScreenPoint(Player.transform.position), 500.0f, 200.0f);
                yajirushiToPlayerTargert = "player";
            }
        }
        else if (ItemBagController.itemsContains("灯篭") == null)
        {
            yajirushiToPlayer.SetActive(false);
        }

        if (step == 2&& mainCamera.activeSelf==true)
        {
            mouseGif.SetActive(false);
            //toright
            if (ItemBagController.itemsContains("マッチ") != null && ItemBagController.itemsContains("ロウソク") != null && ItemBagController.itemsContains("灯篭") != null)
            {
                yajirushiToRight.SetActive(false);
            }
            if (PlayerLight.activeSelf == true && PlayerLight.GetComponent<HandLight>().isEvent == false)
            {
                yajirushiToRight.SetActive(true);
            }
            if (PlayerLight.GetComponent<HandLight>().isEvent == true)
            {
                if (PlayerLight.activeSelf == true && monster.activeSelf == true)
                {
                    yajirushiToRight.SetActive(false);
                    tourouGif.SetActive(true);
                    step = 3;
                }
            }
        }else if(step == 2 && mainCamera.activeSelf == false)
        {
            yajirushiToRight.SetActive(false);
            tourouGif.SetActive(false);
        }
        if (step == 3)
        {
            if(PlayerLight.GetComponent<HandLight>().isEvent == false)
            {
                tourouGif.SetActive(false);
                step = 4;
            }
        }

        if (step == 4||step==5)
        {
            if ((diary_close = GameObject.Find("diary_close(Clone)")) != null)
            {
                yajirushiToPlayer.SetActive(true);
                Vector3 move = new Vector3(0, 100.0f, 0);
                Vector3 yajirushiToPlayerPosition = Camera.main.WorldToScreenPoint(diary_close.transform.position) + move;
                yajirushiToPlayer.transform.position = yajirushiToPlayerPosition;
                yajirushiToPlayer.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(200.0f, 50.0f);
                yajirushiToPlayer.transform.right = Camera.main.WorldToScreenPoint(diary_close.transform.position) - yajirushiToPlayerPosition;
                step = 5;
            }else if (diary_close == null&&step==5)
            {
                yajirushiToPlayer.SetActive(false);
                mouseGif.SetActive(true);
                step = 6;
            }
        }

        if (step == 6)
        {
            if (!ItemBagController.IsItemBagView)
            {
                mouseGif.SetActive(true);
                pointer.SetActive(false);
            }
            else
            {
                mouseGif.SetActive(false);
                pointer.SetActive(true);
            }
            //pointer.SetActive(true);
            pointer.transform.position = ItemBagController.itemsContains("手帳").transform.position;

            if(target.state.itemName== "手帳")
            {
                step = 7;
                mouseGif.SetActive(false);
                pointer.SetActive(false);
            }

        }

        //アイテムバッグが非アクティブなら、ポインターは必ず非表示
        if (pointer.activeSelf && !(ItemBagController.itemBagActive && ItemBagController.IsItemBagView))
        {
            pointer.SetActive(false);
        }
    }


    //positionにある、targetを指し、widthは横、heightは高さ
    public void setYajirusi(GameObject yajirushi, GameObject target, Vector3 position, float width, float height)
    {
        yajirushi.transform.position = position;
        yajirushi.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        yajirushi.transform.right = target.transform.position - yajirushi.transform.position;
    }

    //gob1とgob2の真ん中にある、gob2に指し、widthは横、heightは高さ
    /*例:
     * GameObject yaji = Instantiate(yajirusi,canvas.transform) as GameObject;
     * yaji.GetComponent<yajirusiController>().setYajirusi(Gob1, Gob2, 400.0f, 100.0f);
     */
    public void setYajirusi(GameObject yajirushi,GameObject gob1, GameObject gob2, float width, float height)
    {
        yajirushi.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        yajirushi.transform.position = (gob1.transform.position + gob2.transform.position) / 2;
        yajirushi.transform.right = gob2.transform.position - yajirushi.transform.position;
    }
    public void setYajirusi(GameObject yajirushi,Vector3 gob1Position, Vector3 gob2Position, float width, float height)
    {
        yajirushi.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        yajirushi.transform.position = (gob1Position + gob2Position) / 2;
        yajirushi.transform.right = gob2Position - gob1Position;
    }

    //

    private void Start()
    {
        pointer.SetActive(false);
        yajirushiToPlayer.SetActive(false);
        yajirushiToRight.SetActive(false);
        yajirushiToLeft.SetActive(false);
        tourouGif.SetActive(false);
        mouseGif.SetActive(false);

        group = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (isStart) { yajirushiItemViewController(); } 
        group.alpha = UIController.instance.isCanInput || (ItemBagController.IsItemBagView) ? 1 : 0;
    }
}
