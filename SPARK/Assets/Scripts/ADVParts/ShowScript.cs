using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*
 *  テキストや立ち絵を表示・変更の管理をするスクリプト
 */

/// <summary>
/// ShowScriptに渡すAction
/// </summary>
/// <returns></returns>
public delegate IEnumerator ShowTextAction();

public class ShowScript : SingletonMonoBehaviour<ShowScript>
{
    /// <summary>
    /// ADVの種類
    /// </summary>
    public enum ADVType
    {
        Haru_GameStart = 1,
        Haru_MessageDrop = 2,

        Phone_Start = 40,//電話に出る
        Phone_Message,
        RedMessage_Read = 45,//赤文字読む
        Ticket,
        Movie_Start = 50,//映画見始める
        Movie_Enter = 51,//映画館調べる


        Monster_Enter = 100,//化け物初登場
        Monster_Destroy = 101,//化け物倒した
        Monster2_Enter,

        ItemGet_Light = 200,//灯篭持った
        ItemCanUse_Knife = 219,//ナイフ使ってほしい
        ItemUse_Knife = 220,//ナイフ使う
        ItemCanUse_Light = 221,


        Item_Dialy = 250,//手紙読んだ


        Return_Light = 300,//光がないので進めない
        Return_Erectric = 301,//電気びりびり
        Return_Phone = 302,//電話が気になる
        Return_Bird = 303,//カラスうるさい1
        Return_Monster = 304,//もう戻りたくない


        test = 0,
        None = -1,
    }

    //変数エリア

    /*----------総合(リストとか)----------*/

    //リスト
    //ここにデータ色々ぶちこんでなんやかんや
    private ScenarioData activeData;
    [System.NonSerialized]
    public List<Chara> personList = new List<Chara>();
    [System.NonSerialized]
    public List<Position> positionList = new List<Position>();
    [System.NonSerialized]
    public List<string> contentsList = new List<string>();
    [System.NonSerialized]
    public List<string> commandList = new List<string>();
    [System.NonSerialized]
    public List<ShowTextAction> actions = new List<ShowTextAction>();

    public bool isShow = false;
    private int id;
    private int actionCount; //何番目のActionを実行するか

    //テキストを進める用
    [System.NonSerialized]
    public bool _input;

    private Coroutine cor_Update;

    /*----------オブジェクト----------*/

    //テキストボックス
    [SerializeField]
    private GameObject textBoxPrefab;
    [System.NonSerialized]
    public GameObject textBox;
    [System.NonSerialized]
    public Text charaText;
    [System.NonSerialized]
    public Text mainText;

    /*----------立ち絵関連----------*/

    [SerializeField]
    private CharaTable charaTable;

    //立ち絵のポジション
    [NamedArrayAttribute(new string[] { "Left", "Bottom", "Right" })]
    public Transform[] charaPotision = new Transform[3];

    //出ているキャラクターを配列で保持しておきたい
    [System.NonSerialized]
    public GameObject[] stageChara = new GameObject[3];

    private void Start()
    {
        //コルーチンを起動
        cor_Update = StartCoroutine(update());
        XMLLoad.instance.StartLoad();
    }

    //デバッグ
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    EventStart(0);
        //}
    }

    public bool GetIsShow() {
        return isShow;
    }

    private void SetAction(List<ShowTextAction> value) {
        actions = value;
    }

    private void OnEnable()
    {
        if (id != 0) {
            Restart();
        }
    }

    public void Restart()
    {
        id = 0;
        actionCount = 0;
        Start();
    }

    //初期化
    private void InitADV()
    {
        textBox = Instantiate(textBoxPrefab);
        charaText = textBox.transform.Find("TextBox/CharaText").GetComponent<Text>();
        mainText = textBox.transform.Find("TextBox/MainText").GetComponent<Text>();
        charaPotision[0] = textBox.transform.Find("Potisions/pos_Left");
        charaPotision[1] = textBox.transform.Find("Potisions/pos_Bottom");
        charaPotision[2] = textBox.transform.Find("Potisions/pos_Right");
        textBox.SetActive(false);
    
        id = 0;
        actionCount = 0;
        stageChara = new GameObject[3];
    }

    /// <summary>
    /// ADVイベントの呼び出し
    /// </summary>
    /// <param name="eventNum">イベントの番号</param>
    public void EventStart(int eventNum)
    {
        if(isShow)
        {
            return;
        }
        isShow = true;

        InitADV();
        activeData = XMLLoad.instance.data[eventNum];
        personList = activeData.Get_PersonList();
        positionList = activeData.Get_PositionList();
        contentsList = activeData.Get_ContentsList();
        commandList = activeData.Get_CommandList();

        StartCoroutine(Show());
    }

    public void EventStart(ADVType eventType,List<ShowTextAction> value = null)
    {
        if (eventType == ADVType.None) { return; }

        if (isShow)
        {
            return;
        }
        isShow = true;

        SetAction(value);
        int num = 0,len = XMLLoad.instance.data.Count;
        for (int i = 0; i < len; i++) {
            if (XMLLoad.instance.advTypes[i] == eventType) {
                num = i;
                break;
            }
        }
        EventStart(num);
    }

    private IEnumerator Show()
    {
        yield return TextBoxWrite.instance.TextBoxAnim();
        yield return CharaScript.instance.CharaChange(id, GetCharaImage(id), charaTable.Scale(personList[id]));
        TextBoxWrite.instance.UpdateTexts(id);
        yield break;
    }

    private Sprite GetCharaImage(int id)
    {
        Sprite result = null;
        result = charaTable.GetCharaImage(personList[id], 0);
        return result;
    }

    private IEnumerator CustomEvent()
    {
        //Actionが指定されていたら実行し、終了まで待機する
        if (commandList[id] != "empty")
        {
            if (actions[actionCount] == null)
            {
                actionCount++;
            }
            else
            {
                yield return StartCoroutine(actions[actionCount++]());
            }
        }
    }
    
    /// <summary>
    /// ADVパートでの、入力を扱う
    /// 通常はマウスのクリックだが、デバッグ用でスペースキーでも
    /// </summary>
    /// <returns></returns>
    private IEnumerator update()
    {
        while(true)
        {
            _input = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space);

            if (_input && TextBoxWrite.instance.textWriting)
            {
                //テキスト飛ばし処理
            }

            if (_input && !TextBoxWrite.instance.textWriting)
            {
                TextBoxWrite.instance.BreakPageIcon();
            }

            if (_input && TextBoxWrite.instance.textBreaing && isShow)
            {
                id++;
                if(id == contentsList.Count)
                {
                    StartCoroutine(Destroy_TextBox());
                    isShow = false;
                }
                else
                {
                    StartCoroutine(CustomEvent());
                    yield return CharaScript.instance.CharaChange(id, GetCharaImage(id), charaTable.Scale(personList[id]));
                    TextBoxWrite.instance.UpdateTexts(id);
                }
            }
            yield return null;
        }
    }

    private IEnumerator Destroy_TextBox()
    {
        float alpha = 1f;
        while (alpha >= 0f)
        {
            alpha -= 0.04f;
            textBox.GetComponent<CanvasGroup>().alpha = alpha;
            yield return null;
        }
        Destroy(textBox);
        yield break;
    }
}
