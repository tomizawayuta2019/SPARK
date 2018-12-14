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
        if(Input.GetKeyDown(KeyCode.A))
        {
            EventStart(0);
        }
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
        stageChara = new GameObject[3];
    }

    /// <summary>
    /// ADVイベントの呼び出し
    /// </summary>
    /// <param name="eventNum">イベントの番号</param>
    public void EventStart(int eventNum)
    {
        isShow = true;
        InitADV();
        activeData = XMLLoad.instance.data[eventNum];
        personList = activeData.Get_PersonList();
        positionList = activeData.Get_PositionList();
        contentsList = activeData.Get_ContentsList();
        commandList = activeData.Get_CommandList();

        StartCoroutine(Show());
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
    IEnumerator update()
    {
        while(true)
        {
            _input = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space);
            
            if(_input && !TextBoxWrite.instance.textWriting)
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
