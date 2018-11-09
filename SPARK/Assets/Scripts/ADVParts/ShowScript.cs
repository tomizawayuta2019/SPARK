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

    /*----------総合(リストとか)----------*/

    //リスト
    private List<string> personList = null;
    private List<string> positionList = null;
    private List<string> contentsList = null;

    //コルーチン
    private Coroutine updateCor;
    private Coroutine textCor;

    //id
    private int id;
    private int length;

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
    public enum Potision
    {
        Left = 0,
        Bottom,
        Right
    }
    public Potision charaPos;

    //キャラ立ち絵を入れておく
    [NamedArrayAttribute(new string[] { "dummy_Bird", "dummy_Penguin", "dummy_Neko" })]
    [SerializeField]
    private Sprite[] charaSprite;
    //いーなむ
    public enum Chara
    {
        Bird = 0,
        Penguin,
        Neko
    }
    public Chara charaSpr;

    //出ているキャラクターを配列で保持しておきたい
    private GameObject[] stageChara = new GameObject[3];

    /*----------テキスト関連----------*/

    [SerializeField]
    private CSVLoadScript csvLoadScript;
    [SerializeField]
    private Text textBox;
    
    //

    //キャラの立ち絵を表示する
    public void ShowChara(int charapos, int charaspr)
    {
        //基本構成
        GameObject chara = Instantiate(charaPrefab) as GameObject;
        chara.transform.SetParent(canvas.transform);
        chara.transform.position = charaPotision[charapos].position;
        chara.GetComponent<Image>().sprite = charaSprite[charaspr];
        //左右反転させる
        if(/*左右反転するかどうか*/false)
        {
            Vector3 scale = chara.transform.localScale;
            scale.x *= -1;
            chara.transform.localScale = scale;
        }
        stageChara[charapos] = chara;
    }

    //メインキャラの切り替え
    public void TalkingChara (int activeCharaNum)
    {
        for(int i = 0; i < stageChara.Length; i++)
        {
            CanvasGroup canvasgroup = null;
            if (stageChara[i] != null)
            {
                canvasgroup = stageChara[i].GetComponent<CanvasGroup>();
                if (i == activeCharaNum)
                {
                    canvasgroup.alpha = 1f;
                }
                else
                {
                    canvasgroup.alpha = 0.5f;
                }
            }
        }
    }

    //そのポジション(配列)に同じキャラがいるか(仮版でいるかどうかだけ取得)
    private bool Is_StayChara(int potision)
    {
        if(stageChara[potision] != null)
        {
            return true;
        }
        return false;
    }

    //基本ここで切り替え・表示を指示したい(今は矢印キーで切り替え)
    public void ChangeChara()
    {
        updateCor = StartCoroutine(update());
    }

    //テキストを表示(更新)したい
    public void ShowText(List<string> text)
    {
        if(contentsList == null)
        {
            contentsList = text;
        }
        length = text.Count;
        if(textCor !=null)
        {
            StopCoroutine(textCor);
        }
        textCor = StartCoroutine(TextLoad(text[id]));
    }

    //テキスト更新
    IEnumerator TextLoad(string text)
    {
        string str = "";
        string tgt = "";
        int strLength = 0;
        
        while (true)
        {
            while (!Input.GetKeyDown(KeyCode.DownArrow))
            {
                yield return null;
            }
            while (true)
            {
                tgt = text.Substring(strLength, 1);
                str = str + tgt;
                textBox.text = str;
                strLength++;
                if (tgt == "\n")
                {
                    Debug.Log("break");
                    break;
                }
                if (strLength >= text.Length)
                {
                    yield break;
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    //矢印キーを扱う
    IEnumerator update()
    {
        while(true)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow) && id >= 1)
            {
                id--;
                StopCoroutine(textCor);
                textCor = StartCoroutine(TextLoad(contentsList[id]));
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && id < length - 1)
            {
                id++;
                StopCoroutine(textCor);
                textCor = StartCoroutine(TextLoad(contentsList[id]));
            }
            yield return null;
        }
    }
}
