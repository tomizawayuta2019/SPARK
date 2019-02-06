using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテム情報表示用クラス
/// </summary>
public class ItemView : SingletonMonoBehaviour<ItemView> {
    [HideInInspector] public itemController target;
    private bool isItemView = false;
    public bool IsItemView { get { return isItemView; } }

    [SerializeField]
    Image image;
    [SerializeField]
    GameObject textGroup;
    [SerializeField]
    Text text;
    [SerializeField]
    float textSize;
    [SerializeField]
    float maxTextHeight;
    [SerializeField]
    Vector3 textDefPos;
    [SerializeField]
    GameObject[] buttons;
    [SerializeField]
    int currenttextNum = 0;
    [SerializeField]
    Piano piano;
    [SerializeField]
    List<Sprite> dialySprites;

    bool isDialy;

    [SerializeField] float textHeight;

    /// <summary>
    /// ウィンドウ開く
    /// </summary>
    /// <param name="target">対象のアイテム</param>
    public void Open(itemController target) {
        this.target = target;
        Close();
        isItemView = true;



        if (target.state.itemType == ItemType.diary)
        {
            SetImage(GetDiarySprite(target.state.itemText.Length));
            image.gameObject.SetActive(true);
        }

        else if (target.state.sprite != null) {
            SetImage(target.state.sprite);
            image.gameObject.SetActive(true);
        }

        SetItemText(target.state);

        gameObject.SetActive(true);
        ItemBagController.instance.itemBagActive = false;
    }

    private Sprite GetDiarySprite(int index)
    {
        return dialySprites[index == 0 ? 0 : index == 1 ? 1 : 2];
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
            isDialy = true;
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
        FadeManager.instance.FadeStart(
            new FadeManager.FadeState()
            {
                fadeTime = 0.5f,
                outComp = () =>
                    {
                        SetText(target.state.itemText[currenttextNum]);
                        //image.sprite = dialySprites[1];
                    }
            });
        SEController.instance.PlaySE(SEController.SEType.message);
        StartCoroutine(DialyPageAnim(0.5f, pageMove < 0));

        buttons[0].SetActive(currenttextNum < target.state.itemText.Length - 1);
        buttons[1].SetActive(currenttextNum != 0);
    }

    private IEnumerator DialyPageAnim(float time, bool isReverse = false)
    {
        List<int> indexs = new List<int>() { 3, 4};
        if (isReverse) { indexs.Reverse(); }
        for (int i = 0; i < indexs.Count; i++)
        {
            SetImage(dialySprites[indexs[i]]);
            yield return new WaitForSeconds(time / 4);
        }
        SetImage(GetDiarySprite(2));
    }

    public void Click() {
        switch (target.state.itemType) {
            case ItemType.diary:
                //target.ExChange();
                break;
            case ItemType.piano:
                piano.Click();
                break;
        }
    }

    private void SetItemText(ItemState value)
    {
        text.transform.localPosition = textDefPos;
        currenttextNum = 0;

        if (value.itemText == null || value.itemText.Length == 0 || value.itemText[0] == "")
        {
            textGroup.SetActive(false);
            SetButtonActive(false);
            return;
        }

        textGroup.SetActive(true);
        if (value.itemText.Length <= 1)
        {
            SetText(value.itemText[0]);
            text.gameObject.SetActive(true);
            SetButtonActive(false);
        }
        else if (value.itemText.Length > 1)
        {
            text.gameObject.SetActive(true);
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == 0) { buttons[i].SetActive(true); }
            }
            currenttextNum = 0;
            SetText(value.itemText[currenttextNum]);
        }
    }

    private void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
        image.GetComponent<RectTransform>().sizeDelta = sprite.rect.size;
    }

    private void SetText(string value)
    {
        text.text = value;
        if (currenttextNum == 0) { buttons[1].SetActive(false); }
    }

    private void Update()
    {
        //表示してからでないと行数が計算されないのでUpdateに記述
        textHeight = text.cachedTextGenerator.lineCount * textSize;

        SetUpDownButtonActive(textHeight > maxTextHeight);
    }

    public void MoveText(float value)
    {
        if (value > 0)
        {
            if (text.transform.localPosition.y < textDefPos.y + textHeight - maxTextHeight)
            {
                text.transform.position = text.transform.position + new Vector3(0, value * TimeManager.DeltaTime, 0);
            }
        }
        else if(text.transform.localPosition.y > textDefPos.y)
        {
            text.transform.position = text.transform.position + new Vector3(0, value * TimeManager.DeltaTime, 0);
        }
    }

    private void SetButtonActive(bool value)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(value);
        }
    }

    private void SetUpDownButtonActive(bool value)
    {
        for (int i = 2; i < buttons.Length; i++)
        {
            buttons[i].SetActive(value);
        }
    }
}
