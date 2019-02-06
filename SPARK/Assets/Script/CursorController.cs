using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorController : SingletonMonoBehaviour<CursorController> {

    //[SerializeField]
    //CursorEditorWindow cursorEditorWindow;
    public Texture2D normalCursor, itemCursor, itemGetCursor, itemUseCursor, gimmickCursor, nextADVCursor;
    private Dictionary<CursorType, Texture2D> cursorImageDict = null;
    private bool isUI;

    public enum CursorType
    {
        normal,//通常（移動可能）
        item,//アイテムリスト内のアイテム選択
        itemGet,//アイテムを拾える場合
        itemUse,//アイテムを使用できる場合
        gimmick,//ギミックをクリック
        nextADV,//ADVパートのページ送り
    }

    //カーソルを指定したものに変化
    public void SetCursorImage(CursorType cursorType)
    {
        var tex = cursorImageDict[cursorType];
        Cursor.SetCursor(tex, Vector2.zero, CursorMode.Auto);
    }

    protected override void Awake()
    {
        base.Awake();
        cursorImageDict = new Dictionary<CursorType, Texture2D>();

        cursorImageDict.Add(CursorType.normal, normalCursor);
        cursorImageDict.Add(CursorType.item, itemCursor);
        cursorImageDict.Add(CursorType.itemGet, itemGetCursor);
        cursorImageDict.Add(CursorType.itemUse, itemUseCursor);
        cursorImageDict.Add(CursorType.gimmick, gimmickCursor);
        cursorImageDict.Add(CursorType.nextADV, nextADVCursor);

        instance = this;
    }

    public GraphicRaycaster m_CanvasUI;
    public EventSystem eventSystem;

    private void CheckUI(Vector2 pos)
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.pressPosition = pos;
        eventData.position = pos;

        List<RaycastResult> list = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, list);
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].gameObject.tag == "Item")
            {
                SetCursorImage(CursorType.item);
                break;
            }
            else
            {
                SetCursorImage(CursorType.normal);
            }
        }
        if (list.Count == 0) { SetCursorImage(CursorType.normal); }
    }

    private void CheckGameObject() {
        GameObject target = MouseExt.GetMousePosGameObject((obj) => {
            if (obj.tag == "Item" || obj.tag == "Gimick" || obj.tag == "Detail") { return true; }
            return false;
        });

        if (target == null)
        {
            CheckUI(Input.mousePosition);
            return;
        }

        switch (target.tag)
        {
            case "Item":
            case "Detail":
                SetCursorImage(CursorType.item);
                break;
            case "Gimick":
                SetCursorImage(CursorType.gimmick);
                break;
        }
    }

    // Use this for initialization
    void Start () {
        SetCursorImage(CursorType.normal);
	}
	
	// Update is called once per frame
	void Update () {
        isUI = !UIController.instance.isCanInput;

        if (isUI)
        {
            CheckUI(Input.mousePosition);
        }
        else
        {
            CheckGameObject();
        }
    }
}
