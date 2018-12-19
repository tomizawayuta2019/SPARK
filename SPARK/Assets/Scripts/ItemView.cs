using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテム情報表示用クラス
/// </summary>
public class ItemView : SingletonMonoBehaviour<ItemView> {
    public itemController target;
    private bool isItemView = false;
    public bool IsItemView { get { return isItemView; } }

    [SerializeField]
    Image image;
    [SerializeField]
    Text text;
    [SerializeField]
    GameObject[] buttons;
    [SerializeField]
    int currenttextNum = 0;
    [SerializeField]
    Piano piano;

    bool isDialy;

    /// <summary>
    /// ウィンドウ開く
    /// </summary>
    /// <param name="target">対象のアイテム</param>
    public void Open(itemController target) {
        this.target = target;
        Close();
        isItemView = true;
        if (target.state.sprite != null) {
            image.sprite = target.state.sprite;
            image.gameObject.SetActive(true);
        }
        if (target.state.itemText.Length <= 1 && target.state.itemText[0] != "")
        {
            text.text = target.state.itemText[0];
            text.gameObject.SetActive(true);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
        }
        else if(target.state.itemText.Length > 1){
            text.gameObject.SetActive(true);
            for (int i = 0; i < buttons.Length; i++) {
                if (i == 0) { buttons[i].SetActive(true); }
            }
            currenttextNum = 0;
            text.text = target.state.itemText[currenttextNum];
        }

        gameObject.SetActive(true);
        ItemBagController.instance.itemBagActive = false;
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
        gameObject.SetActive(false);
        ItemBagController.instance.itemBagActive = true;

        if (target.state.itemType == ItemType.diary_open && !isDialy)
        {
            ShowScript.instance.EventStart(ShowScript.ADVType.Item_Dialy);
        }
    }

    /// <summary>
    /// アイテムが変化する際の処理
    /// </summary>
    public void TransformItem(itemController newItem) {
        //見た目上はすぐに変わらないが、内部では即変更
        target = newItem;
        //フェードアウトを待機してから画像切り替え、その後フェードイン
        FadeManager.instance.FadeStart(new FadeManager.FadeState() { fadeTime = FadeManager.instance.defaultFadeTime, outComp = () => { Open(newItem); } });
    }

    public void PageButton(int pageMove) {
        currenttextNum += pageMove;
        currenttextNum = Mathf.Clamp(currenttextNum, 0, target.state.itemText.Length - 1);
        text.text = target.state.itemText[currenttextNum];
        SEController.instance.PlaySE(SEController.SEType.message);

        buttons[0].SetActive(currenttextNum < target.state.itemText.Length - 1);
        buttons[1].SetActive(currenttextNum != 0);
    }

    public void Click() {
        switch (target.state.itemType) {
            case ItemType.diary:
                target.ExChange();
                break;
            case ItemType.piano:
                piano.Click();
                break;
        }
    }
}
