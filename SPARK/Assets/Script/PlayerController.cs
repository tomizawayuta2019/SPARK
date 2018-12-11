using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>,IItemUse {
    public float PlayerSpeed;
    public int PlayerMoveFlag = 1;
    public bool PlayerActive;
    
    public GameObject NowItem;
    public GameObject mainCamera;
    public Vector2 mousePosition;
    public Vector2 targetPosition;
    [SerializeField]
    ItemBagController itemBagController;
    [SerializeField]
    Rigidbody2D rig;
    public bool isHaveLight = false;
    [SerializeField]
    HandLight handLight;
    [SerializeField]
    PlayerAnimController playerAnimController;
    bool isWait = false;
    public void SetPlayerActive(bool condition)
    {
        PlayerActive = condition;
    }
    //当たり判定によるアイテム調査
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item") {
            NowItem = other.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == NowItem) {
            NowItem = null;
        }
    }

    private void Start()
    {
        targetPosition = transform.position;
    }

    void PlayerSearchMouse()
    {
        if (NowItem != null)
        {
            SetPlayerActive(false);
            /*アイテム調査動作をここに入れる

            */
            ItemObject item = NowItem.GetComponent<ItemObject>();
            if (item != null)
            {
                itemBagController.PutInItemBag(item);
                item.GetItem();
            }
            else {
                Destroy(NowItem);
            }
            
            PlayerActive = true;
        }
    }

    //調査終了によるプレイヤー移動可能になる
    public void PlayerSearchMouseOver()
    {
        SetPlayerActive(true);
    }
    //マオス移動
    void PlayerMoveMouse(float moveSpeed)
    {
        //Vector3 Position = this.GetComponent<Transform>().position;
        if (PlayerActive)
        {
            if (Input.GetMouseButton(0))
            {
                mousePosition = Input.mousePosition;
                if (mousePosition.x >= 0.0f && mousePosition.x <= 1920.0f && mousePosition.y >= 0.0f && mousePosition.y <= 880.0f)
                {
                    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    targetPosition = new Vector2(mousePosition.x, transform.position.y);
                }
            }

            PlayerRotationUpdata();
            Vector3 defPos = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, PlayerSpeed * TimeManager.DeltaTime);
            playerAnimController.SetBool("IsWalk", Vector3.Distance(defPos, transform.position) > 0.001f);
            PlayerSearchMouse();
        }
        else {
            playerAnimController.SetBool("IsWalk", false);
        }
    }

    /// <summary>
    /// プレイヤーが目標地点に到達したか
    /// </summary>
    /// <returns></returns>
    public bool IsEnterTargetPosition() {
        return Mathf.Abs(targetPosition.x - transform.position.x) < 0.001f;
    }

    private void PlayerRotationUpdata() {
        if (IsEnterTargetPosition()) { return; }
        //進行方向に向く
        Vector3 scale = transform.localScale;
        if (targetPosition.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
        }
        else if (targetPosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
        }
    }

    public void PlayerUpdata()
    {
        PlayerMoveMouse(PlayerSpeed);
        if (Input.GetKey(KeyCode.Space))
        {
            SetPlayerActive(true);
        }

    }

    /// <summary>
    /// プレイヤーの移動を待機する処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator WaitForMove(System.Action comp) {
        if (isWait) { yield break; }
        isWait = true;
        yield return null;

        while (!IsEnterTargetPosition()) {
            yield return null;
            if (Input.GetMouseButtonDown(0)) {
                isWait = false;
                yield break;
            }
        }

        comp();
        isWait = false;
    }

    public bool IsCanUseItem(ItemState item)
    {
        return item.itemType == ItemType.lighting;
    }

    public bool ItemUse(ItemState item)
    {
        switch (item.itemType) {
            case ItemType.lighting:
                //ライトを持つ
                isHaveLight = true;
                handLight.gameObject.SetActive(true);
                playerAnimController.SetTrigger("HaveLight");
                break;
            default:
                return false;
        }

        return true;
    }

    public void MonsterDestroyEvent() {
        GetComponent<Animator>().SetBool("isLight",true);
        handLight.EventStart();
    }
}
