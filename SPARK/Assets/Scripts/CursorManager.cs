using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct TextureSet
{
    public string name;
    public Texture2D[] sprites;

    public Texture2D this[int index] {
        set { sprites[index] = value; }
        get { return sprites[index]; }
    }
}

public enum CursorType
{
    normal,//通常（移動可能）
    item,//アイテムリスト内のアイテム選択
    itemGet,//アイテムを拾える場合
    itemUse,//アイテムを使用できる場合
    gimmick,//ギミックをクリック
    nextADV,//ADVパートのページ送り
}

public struct CursorState {
    public Texture2D texture;
    public Vector2 hostpost;
    public CursorMode cursorMode;

    public static CursorState GetDefaultValue() {
        return new CursorState() { texture = null,hostpost = Vector2.zero,cursorMode = CursorMode.Auto };
    }

    public void SetCorsor() {
        Cursor.SetCursor(texture, hostpost, cursorMode);
    }
}

/// <summary>
/// マウスカーソルの表示管理クラス
/// </summary>
public class CursorManager : SingletonMonoBehaviour<CursorManager> {
    private bool isMouseDown;
    public bool IsMouseDown { get { return isMouseDown; } }
    private CursorType state;
    public CursorType State {
        get { return state; }
        set {
            if (state == value) { return; }
            state = value;
            UpdateCursor();
        }
    }

    [SerializeField]
    CursorScriptableObject cursorScriptableObject;

    protected override void Awake()
    {
        CursorState state = CursorState.GetDefaultValue();
        state.texture = GetCursorSprite(CursorType.normal, isMouseDown);
        state.SetCorsor();
    }

    private Texture2D GetCursorSprite(CursorType type,bool isMouseDown) {
        return cursorScriptableObject.spriteSets[(int)type][isMouseDown ? 1 : 0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            UpdateCursor();
        }

        if (Input.GetMouseButtonUp(0)) {
            isMouseDown = false;
            //MouseEnter();
            UpdateCursor();
        }
    }

    private void UpdateCursor() {
        CursorState state = CursorState.GetDefaultValue();
        state.texture = GetCursorSprite(State, IsMouseDown);
        state.SetCorsor();
        Debug.Log(state.texture);
    }

    //private void MouseEnter() {
    //    switch (state)
    //    {
    //        case CursorState.
    //    }
    //}
}
