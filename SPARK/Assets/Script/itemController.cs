using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof (Image))]
public class itemController : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler,IDropHandler,IPointerClickHandler,IPointerDownHandler {
    [SerializeField]
    PlayerController playerController ;
    [SerializeField]
    ItemBagController itemBagController;
    [SerializeField]
    public ItemState state;
    bool isClick = false;//ドラッグではなくクリックでの操作か
    Image image;


    public void ItemClickUse()
    {
        //EventTrigger trigger = ClickImage.GetComponent<EventTrigger>();
        //EventTrigger.Entry entry=new event
    }

    private Vector2 prevPos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ前の位置を記憶しておく
        prevPos = transform.position;
        playerController.PlayerActive = false;
        itemBagController.itemBagActive = false;
        UIController.instance.list.Add(gameObject);

        isClick = false;
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ドラッグ中は位置を更新する
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        System.Action returnPos = () =>
        {
            // ドラッグ前の位置に戻す
            transform.position = prevPos;
            playerController.PlayerActive = true;
            itemBagController.itemBagActive = true;
            UIController.instance.list.Remove(gameObject);
        };
        image.raycastTarget = true;

        if (ItemImage.currentTargetImage != null && state.IsCanUseItem((int)itemBagController.itemView.target.state.itemType))
        {
            itemBagController.itemView.target.ExChange();
            Destroy(gameObject);
            return;
        }

        var tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!Physics2D.OverlapPoint(tapPoint)) {
            returnPos();
            return;
        }

        IItemUse targetItem = null;
        GameObject hitObject = null;
        List<GameObject> hitObjects = new List<GameObject>();
        do {
            var hit = Physics2D.Raycast(tapPoint, -Vector3.up);
            if (hit.collider == null) { break; }
            hitObject = hit.collider.gameObject;
            if (hitObject == null) { break; }
            hitObjects.Add(hitObject);
            hitObject.SetActive(false);
            targetItem = hitObject.GetComponent<IItemUse>();

        } while (targetItem == null && hitObject != null);//レイの当たるオブジェクトが無くなるか、対象となるオブジェクトが取得できるまで
        
        foreach (GameObject hit in hitObjects) {
            hit.SetActive(true);
        }

        if (targetItem == null || !targetItem.IsCanUseItem(state)) {
            returnPos();
            return;
        }

        if (!targetItem.ItemUse(state)) {
            returnPos();
            return;
        }

        //正常に使用完了したのでアイテム消費
        Destroy(gameObject);
    }

    public void OnDrop(PointerEventData eventData)
    {
        //    Ray ray = new Ray();
        //    RaycastHit hit = new RaycastHit();
        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        //    {
        //        if (hit.collider.gameObject.CompareTag("Key"))
        //        {
        //            Debug.Log("Hit by mouse!");
        //            hit.collider.transform.gameObject.GetComponent<DoorController>().DestroySara();
        //            Destroy(this.gameObject);

        //        }
        //    }
    }


    // Use this for initialization
    void Start () {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        itemBagController = GameObject.Find("ItemBag").GetComponent<ItemBagController>();
        image = GetComponent<Image>();
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isClick)
        {
            itemBagController.ItemView(this);
        }
        isClick = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClick = true;
        Debug.Log(isClick);
    }

    public void ExChange() {
        state.Exchange();
        GetComponent<Image>().sprite = state.sprite;
        itemBagController.itemView.TransformItem(this);
    }
}
