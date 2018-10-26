using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*
 *  テキストや立ち絵を表示・変更するスクリプト
 */

public class ShowScript : MonoBehaviour
{
    //変数エリア

    /*----------立ち絵関連----------*/
    
    //Canvasを親にしたい
    [SerializeField]
    private GameObject canvas;

    //キャラ表示用のプレハブ
    [SerializeField]
    private GameObject charaPrefab;

    //立ち絵のポジション
    [NamedArrayAttribute(new string[] { "Left", "Bottom", "Right" })]
    [SerializeField]
    private Transform[] charaPotision = new Transform[3];
    //いーなむ
    private enum Potision
    {
        Left = 0,
        Bottom,
        Right
    }
    private Potision charaPos;

    //キャラ立ち絵を入れておく
    [NamedArrayAttribute(new string[] { "dummy_Bird", "dummy_Penguin", "dummy_Neko" })]
    [SerializeField]
    private Sprite[] charaSprite;
    //いーなむ
    private enum Chara
    {
        Bird = 0,
        Penguin,
        Neko
    }
    private Chara charaspr;

    /*----------テキスト関連----------*/

    [SerializeField]
    private Text textBox;
    
    //

    //キャラの立ち絵を表示する(引数を追加したい)
    public void ShowChara()
    {
        //基本構成
        GameObject chara = Instantiate(charaPrefab) as GameObject;
        chara.transform.SetParent(canvas.transform);
        chara.transform.position = charaPotision[(int)Potision.Left].position;
        chara.GetComponent<Image>().sprite = charaSprite[(int)Chara.Neko];
        //左右反転させる
        if(/*左右反転するかどうか*/true)
        {
            Vector3 scale = chara.transform.localScale;
            scale.x *= -1;
            chara.transform.localScale = scale;
        }
        //薄くするやつ(仮)
        if(/*薄くするかどうか*/true)
        {
            chara.GetComponent<CanvasGroup>().alpha = 0.5f;
        }
    }

    //テキストを表示(更新)したい
    public void ShowText()
    {

    }
}
