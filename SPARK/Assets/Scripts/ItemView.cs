using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテム情報表示用クラス
/// </summary>
public class ItemView : MonoBehaviour {
    public ItemState target;
    private bool isItemView = false;
    public bool IsItemView { get { return isItemView; } }

    [SerializeField]
    Image image;
    [SerializeField]
    Text text;

    /// <summary>
    /// ウィンドウ開く
    /// </summary>
    /// <param name="target">対象のアイテム</param>
    public void Open(ItemState target) {
        Close();
        isItemView = true;
        if (target.sprite != null) {
            image.sprite = target.sprite;
            image.gameObject.SetActive(true);
        }
        if (target.itemText != "") {
            text.text = target.itemText;
            text.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// ウィンドウ閉じる
    /// </summary>
    public void Close() {
        image.sprite = null;
        image.gameObject.SetActive(false);
        text.text = "";
        text.gameObject.SetActive(false);
        isItemView = false;
    }

    /// <summary>
    /// アイテムが変化する際の処理
    /// </summary>
    public void TransformItem(ItemState newItem) {
        //見た目上はすぐに変わらないが、内部では即変更
        target = newItem;
        //フェードアウトを待機してから画像切り替え、その後フェードイン
        FadeManager.instance.FadeStart(new FadeManager.FadeState() { outComp = () => { Open(newItem); } });
    }
}
