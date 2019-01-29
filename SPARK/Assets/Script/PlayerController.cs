using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>,IItemUse {
    [SerializeField] float PlayerSpeed;
    public float MoveSpeed {
        get
        {
            float delta = targetPosition.x - transform.position.x;
            return moveSpeedCurve.Evaluate(Mathf.Abs(delta) / maxDistance) * PlayerSpeed;
        }
    }

    [SerializeField] AnimationCurve moveSpeedCurve;
    float maxDistance = 20;

    public int PlayerMoveFlag = 1;
    [SerializeField]
    private bool PlayerActive;
    public bool PlayerInputActive = true;

    [HideInInspector] GameObject NowItem;
    [HideInInspector] Vector2 mousePosition;
    [HideInInspector] public Vector2 targetPosition;
    [SerializeField]
    ItemBagController itemBagController;
    [SerializeField]
    Rigidbody2D rig;
    public bool isHaveLight = false;
    [SerializeField]
    HandLight handLight;
    [SerializeField]
    PlayerAnimController playerAnimController;
    [SerializeField]
    SpriteRotationOffset spriteOffset;
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
            ItemObject item = NowItem.GetComponent<ItemObject>();
            if (item != null)
            {
                item.GetItem();
            }
            else {
                Destroy(NowItem);
            }
        }
    }

    //調査終了によるプレイヤー移動可能になる
    public void PlayerSearchMouseOver()
    {
        SetPlayerActive(true);
    }
    //マウス移動
    void PlayerMoveMouse(float moveSpeed)
    {
        //Vector3 Position = this.GetComponent<Transform>().position;
        if (PlayerActive && UIController.instance.isCanInput)
        {
            if (PlayerInputActive && Input.GetMouseButton(0))
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
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * TimeManager.DeltaTime);
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
        return Mathf.Abs(targetPosition.x - transform.position.x) < 0.01f;
    }

    private void PlayerRotationUpdata() {
        if (IsEnterTargetPosition()) { return; }
        //進行方向に向く
        if (targetPosition.x > transform.position.x)
        {
            spriteOffset.LookToRight();
        }
        else if (targetPosition.x < transform.position.x)
        {
            spriteOffset.LookToLeft();
        }
        LookToBack(false);
    }

    public void PlayerUpdata()
    {
        PlayerMoveMouse(MoveSpeed);
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

    public void LookToBack(bool value)
    {
        playerAnimController.SetBool("IsLookToBack", value);
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
                ItemView.instance.Close();
                break;
            default:
                return false;
        }

        return true;
    }

    public void MonsterDestroyEvent() {
        //playerAnimController.SetTrigger("UseLightTrigger");
        playerAnimController.SetBool("isCanUseLight", true);
        handLight.EventStart();
    }

    public void ClickLight() {
        playerAnimController.SetTrigger("UseLightTrigger");
        spriteOffset.LookToLeft();
        SetPlayerActive(false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Transform tran = collision.transform;
            //プレイヤーが壁の方向に動こうとしている場合
            if ((tran.position.x - transform.position.x)* (targetPosition.x - transform.position.x) > 0)
            {
                //プレイヤーの移動停止
                targetPosition = transform.position;
            }
        }
    }

    private bool IsRun()
    {
        return Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(targetPosition.x)) > runDelta;
    }
}
