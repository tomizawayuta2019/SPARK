using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*
 *  テキストや立ち絵を表示・変更するスクリプト
 */

/// <summary>
/// ShowScriptに渡すAction
/// </summary>
/// <returns></returns>
public delegate IEnumerator ShowTextAction();

public class ShowScript : MonoBehaviour
{
    //変数エリア

    /*----------総合(リストとか)----------*/

    //リスト
    private List<Chara> personList = new List<Chara>();
    private List<Potision> positionList = new List<Potision>();
    private List<string> contentsList = new List<string>();
    private List<string> commandList = new List<string>();
    private List<ShowTextAction> actions = new List<ShowTextAction>();

    //コルーチン
    private Coroutine updateCor;
    private Coroutine textCor;

    //id
    private int id;
    private int length;
    private int actionCount;//何番目のActionを実行するか

    //テキストを進める用
    private bool toTheNext;

    private XMLLoad xml;

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
        Right,
        empty
    }

    //キャラ立ち絵を入れておく
    [NamedArrayAttribute(new string[] { "Yaku", "Kiriya"})]
    [SerializeField]
    private Sprite[] charaSprite;
    //いーなむ
    public enum Chara
    {
        Yaku = 0,
        Kiriya,
        empty
    }

    //出ているキャラクターを配列で保持しておきたい
    private GameObject[] stageChara = new GameObject[3];

    /*----------テキスト関連----------*/
    
    [SerializeField]
    private Text textBox;

    //テキストの速さ
    [SerializeField]
    private float textSpeed = 0.05f;

    //

    //ほぼコルーチン起動用
    private void Start()
    {
        xml = GetComponent<XMLLoad>();

        positionList = xml.GetPotisionList();
        personList = xml.GetPersonList();
        contentsList = xml.GetContentsList();
        commandList = xml.GetCommandList();

        actionCount = 0;

        updateCor = StartCoroutine(update());

        ShowText();
    }

    public void SetAction(List<ShowTextAction> value) {
        actions = value;
    }

    private void OnEnable()
    {
        if (id != 0) {
            Restart();
        }
    }

    public void Restart() {
        id = 0;
        Start();
    }

    //起動でテキストが動く
    private void ToTheNext()
    {
        toTheNext = true;
    }

    //キャラの立ち絵を表示する
    public void ShowChara(int charapos, int charaspr)
    {
        //基本構成
        GameObject chara = Instantiate(charaPrefab) as GameObject;
        //位置を代入するとキャラの位置がずれるバグがあったので子要素でlocalpositionを0に
        chara.transform.SetParent(charaPotision[charapos].transform);
        chara.transform.SetAsFirstSibling();
        chara.transform.localPosition = Vector3.zero;
        chara.GetComponent<Image>().sprite = charaSprite[charaspr];
        //左右反転させる
        //if(/*左右反転するかどうか*/false)
        //{
        //    Vector3 scale = chara.transform.localScale;
        //    scale.x *= -1;
        //    chara.transform.localScale = scale;
        //}
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

    //そのポジション(配列)に同じキャラがいるか
    private bool Is_StayChara(int potision)
    {
        if(stageChara[potision] != null)
        {
            return true;
        }
        return false;
    }

    //テキストを表示(更新)したい
    public void ShowText()
    {
        length = contentsList.Count;
        if(textCor != null)
        {
            StopCoroutine(textCor);
        }
        toTheNext = true;

        if (!gameObject.activeSelf) { gameObject.SetActive(true); }
        textCor = StartCoroutine(TextLoad(contentsList[id]));
    }

    //キャラを切り替え
    private void CharaChange(int id)
    {
        //キャラがまだ表示されてなかったら開く
        if (positionList[id] != Potision.empty && 
            Is_StayChara((int)positionList[id]) == false)
        {
            ShowChara((int)positionList[id], (int)personList[id]);
        }
        TalkingChara((int)positionList[id]);
    }

    //テキスト更新
    IEnumerator TextLoad(string text)
    {
        string str = "";
        string tgt = "";
        int strLength = 0;

        CharaChange(id);

        while (true)
        {
            while (!toTheNext)
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
                    break;
                }
                if (strLength >= text.Length)
                {
                    StopCoroutine(textCor);
                    textCor = null;
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
            toTheNext = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetMouseButtonDown(0);
            if (toTheNext)
            {
                //Debug.Log((textCor == null) + ":" + id.ToString());
                //ここのタイミングでキャラ更新も
                if (textCor == null && id < length - 1)
                {
                    id++;
                    //CharaChange(id);

                    //Actionが指定されていたら実行し、終了まで待機する
                    if (commandList[id] != "empty") {
                        if (actions[actionCount] == null) { actionCount++; }
                        else { yield return StartCoroutine(actions[actionCount++]()); }
                    }

                    textCor = StartCoroutine(TextLoad(contentsList[id]));
                } else if (textCor == null) {
                    //全てのテキストを見終わったら終了
                    transform.parent.gameObject.SetActive(false);
                    yield break;
                }
            }
            yield return null;
            toTheNext = false;
        }
    }
}
