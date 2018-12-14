using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorController : MonoBehaviour {

    //[SerializeField]
    //CursorEditorWindow cursorEditorWindow;
    public Texture2D normalCursor, itemCursor, itemGetCursor, itemUseCursor, gimmickCursor, nextADVCursor;
    private static CursorController instance = null;
    private Dictionary<CursorType, Texture2D> cursorImageDict = null;

    public enum CursorType
    {
        normal,//通常（移動可能）
        item,//アイテムリスト内のアイテム選択
        itemGet,//アイテムを拾える場合
        itemUse,//アイテムを使用できる場合
        gimmick,//ギミックをクリック
        nextADV,//ADVパートのページ送り
    }

    public static CursorController Instance { get { return instance; } }

    //カーソルを指定したものに変化
    public void SetCursorImage(CursorType cursorType)
    {
        var tex = cursorImageDict[cursorType];
        Cursor.SetCursor(tex, Vector2.zero, CursorMode.Auto);
    }

    private void Awake()
    {
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

    public void CheckUI(Vector2 pos)
    {
        List<GameObject> objList = new List<GameObject>();
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.pressPosition = pos;
        eventData.position = pos;

        List<RaycastResult> list = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, list);
        if (list.Count > 0)
        {

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].gameObject.tag == "Item")
                {
                    Debug.Log(list[i].gameObject.tag);
                    SetCursorImage(CursorType.item);
                    break;
                }
                else
                {
                    SetCursorImage(CursorType.normal);
                }

                //Debug.Log(list[i].gameObject.name);
                
            }
        }
        else
        {
            Debug.Log("没有UI");
        }
    }

    // Use this for initialization
    void Start () {
        SetCursorImage(CursorType.normal);
	}
	
	// Update is called once per frame
	void Update () {
        CheckUI(Input.mousePosition);
    }
}
