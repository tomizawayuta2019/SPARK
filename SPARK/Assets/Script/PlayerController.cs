using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float PlayerSpeed;
    public int PlayerMoveFlag;
    public bool PlayerActive;
    public GameObject NowItem;
    public Vector2 mousePosition;

    void SetPlayerActive(bool condition)
    {
        PlayerActive = condition;
    }
    //当たり判定によるアイテム調査
    void OnTriggerEnter2D(Collider2D other)
    {
        NowItem = other.gameObject;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        NowItem = null;
    }
   void PlayerSearch()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (NowItem != null)
            {
                print(NowItem.name);
                SetPlayerActive(false);
            }
            else
            {
                print("null!");
            }
        }
    }

    //キーボード移動
    void PlayerMoveKeyboard(float moveSpeed)
    {
        Vector3 Position = this.GetComponent<Transform>().position;
        if (PlayerMoveFlag == 1)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(moveSpeed, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate((-1) * moveSpeed, 0, 0);
            }
        }
    }
    void PlayerMoveMouse(float moveSpeed)
    {
        Vector3 Position = this.GetComponent<Transform>().position;
        if (PlayerMoveFlag == 1)
        {
            //マオス移動
            if (Input.GetMouseButton(0))
            {
                mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                transform.Translate(moveSpeed*((mousePosition.x-Position.x)/Mathf.Abs(mousePosition.x - Position.x)),0,0);
            }
        }

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerActive == true)
        {
            PlayerMoveKeyboard(PlayerSpeed);
            PlayerMoveMouse(PlayerSpeed);
            PlayerSearch();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetPlayerActive(true);
        }
    }
}
