using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof (Image))]
public class itemController : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler,IDropHandler {
    [SerializeField]
    PlayerController playerController ;
    [SerializeField]
    ItemBagController itemBagController;
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
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ドラッグ中は位置を更新する
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Ray ray = new Ray();
        //int layerMask = LayerMaskNo.DEFAULT;
        //float maxDistance = 10;

        //RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, maxDistance, layerMask);
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity))
        //{
        //    print(hit.collider.gameObject.name);
        //    if (hit.collider.gameObject.CompareTag("DroppoableField"))
        //    {
        //        Debug.Log("Hit by mouse!");
        //        hit.collider.transform.gameObject.GetComponent<DoorController>().DestroySara();
        //        //Destroy(this.gameObject);

        //    }
        //}
        // ドラッグ前の位置に戻す
        transform.position = prevPos;
        playerController.PlayerActive = true;
        itemBagController.itemBagActive = true;

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
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
