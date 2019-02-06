using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Password : GimmickKind
{
    [SerializeField]
    private Sprite[] passSP = new Sprite[10];
    [SerializeField]
    private Image[] image = new Image[4];
    [SerializeField]
    private Text[] texts = new Text[4];
    private int[] imageNum = new int[4];
    public GameObject passwordForm;
    [SerializeField]
    private int passwordNumber;
    [SerializeField]
    GameObject targetPanel;
    [SerializeField]
    GameObject onImage;
    private bool isOpen = false;
    private bool isPassword = false;
    private int count = 0;
    // オープンした画像
    [SerializeField]
    Sprite openLockImage;
    [SerializeField]
    GameObject doorImage;

    [SerializeField]
    GameObject targetgimmick;

    private void Start()
    {
        FindImage();
    }

    /// <summary>
    /// Canvasの中のパネルコントロールを検索しImageを配列にいれる
    /// </summary>
    private void FindImage()
    {
        for (int i = 0; i < image.Length; i++)
        {
            imageNum[i] = 0;
            GameObject obj = targetPanel;
            //obj = transform.Find("Canvas/PanelControl/Panel").gameObject;
            if (obj == null) return;
            foreach (Transform child in obj.transform)
            {
                if (child.name == (i + 1).ToString())
                {
                    image[i] = child.GetComponent<Image>();
                    image[i].sprite = passSP[0];
                }
            }
        }
    }
    /// <summary>
    /// UIのActiveをつけたり消したり
    /// </summary>
    private void ActiveSelfObject()
    {
        passwordForm.gameObject.SetActive(true);
    }

    public override void Click()
    {
        base.Click();
        if (!isOpen) { ActiveSelfObject(); }
        else { Open(); }
    }

    /// <summary>
    /// 奇数が上偶数が下のボタン
    /// </summary>
    public void PassWordInput(int button)
    {
        if (isOpen||isPassword) return;
        SEController.instance.PlaySE(SEController.SEType.button);
        if (button % 2 == 0)
        {
            imageNum[button / 2] = (imageNum[button / 2] >= 9) ? 0 : imageNum[button / 2] + 1;

            image[button / 2].sprite = passSP[imageNum[button / 2]];
            texts[button / 2].text = imageNum[button / 2].ToString();
        }
        else
        {
            imageNum[button / 2] = (imageNum[button / 2] <= 0) ? 9 : imageNum[button / 2] - 1;
            image[button / 2].sprite = passSP[imageNum[button / 2]];
            texts[button / 2].text = imageNum[button / 2].ToString();
        }
    }
    /// <summary>
    /// ボタンが押されたらパスワードを出力する
    /// </summary>
    public void EnterPassword()
    {
        int password = 0;
        password = imageNum[0] * 1000 + imageNum[1] * 100 + imageNum[2] * 10 + imageNum[3];
        isPassword = passwordNumber == password;
        passwordForm.gameObject.SetActive(false);
        
        if (isPassword) {
            SEController.instance.PlaySE(SEController.SEType.Unlock);
            // ターゲットパネルにあるButton(パスワードのエンターキー)の画像を取得、貼り付け
            targetPanel.transform.Find("Button").GetComponent<Image>().sprite = openLockImage;
            count = 1;
        }
        if(count >= 1)
        {

            SEController.instance.PlaySE(SEController.SEType.Unlock);
            isOpen = true;
        }
        
    }

    public void Open() {
        if (!isOpen) { return; }

        doorImage.gameObject.SetActive(false);
        SEController.instance.PlaySE(SEController.SEType.door_metal);
    }

    public void PowerOff() {
        onImage.gameObject.SetActive(false);
        targetgimmick.GetComponent<ISwitchObject>().SetValue(true);
        SEController.instance.PlaySE(SEController.SEType.engineLever);
    }

    public void PowerOn() {
        onImage.gameObject.SetActive(true);
        targetgimmick.GetComponent<ISwitchObject>().SetValue(false);
        SEController.instance.PlaySE(SEController.SEType.engineLever);
    }
}