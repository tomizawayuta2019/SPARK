using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * ギミックの種類
 */
public class GimmickKind : MonoBehaviour {

    bool pass = false;
    [SerializeField]
    private Sprite[] passSP = new Sprite[10];
    private Image[] image = new Image[4];
    private int[] imageNum = new int[4];

    public enum Kind{
        monster,
        item,
        stage,
        password,
        master,
        door,
    }
    public Kind gimmickKind;

    private void Start()
    {
        FindImage();
    }
    /// <summary>
    /// Canvasの中のパネルコントロールを検索しImageを配列にいれる
    /// </summary>
    private void FindImage()
    {
        if (gimmickKind == Kind.password)
        {
            for (int i = 0; i < image.Length; i++)
            {
                imageNum[i] = 0;
                GameObject obj;
                obj = GameObject.Find("Canvas/PanelControl/Panel");
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
    }
    private void Update()
    {
        // モンスター召喚？
        //ActiveSelfObject();
    }

    /// <summary>
    /// UIのActiveをつけたり消したり
    /// </summary>
    public void ActiveSelfObject()
    {
        if (gimmickKind == Kind.master&&Input.GetMouseButtonDown(0))
        {
            if (gimmickKind == Kind.password)
            {
                pass = !gameObject.transform.GetChild(0).gameObject.activeSelf;
                gameObject.transform.GetChild(0).gameObject.SetActive(pass);
            }
        }
    }

    /// <summary>
    /// 特定の位置に来たらモンスター召喚
    /// </summary>
    public void SetMonster()
    {

    }

    /// <summary>
    /// 奇数が上偶数が下のボタン
    /// </summary>
    public void PassWordInput(int button)
    {
        if (button % 2 == 0)
        {
            imageNum[button / 2] = (imageNum[button/2]>=9)?0:imageNum[button/2]+1;
            
            image[button / 2].sprite = passSP[imageNum[button / 2]];
        }
        else
        {
            imageNum[button / 2] = (imageNum[button / 2] <= 0) ? 9 : imageNum[button / 2] - 1;
            image[button / 2].sprite = passSP[imageNum[button / 2]];
        }
    }
    /// <summary>
    /// ボタンが押されたらパスワードを出力する
    /// </summary>
    public void EnterPassword()
    {
        int password = 0;
        password = imageNum[0] * 1000 + imageNum[1] * 100 + imageNum[2] * 10 + imageNum[3];
        Debug.Log(password);
        gameObject.SetActive(false);
    }
    

}
