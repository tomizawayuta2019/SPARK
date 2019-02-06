using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * ギミックのピアノのスクリプト。GimmickKindを継承しています。
 */
public class Piano : GimmickKind {
    [SerializeField]
    private Sprite[] pianoSprite = new Sprite[11];// ピアノのスプライト
    [SerializeField]
    private Image pianoImage;// ピアノのイメージ pianoSpriteで絵を変える
    public GameObject pianoForm;// Canvasを参照しています
    [SerializeField]
    private GameObject pianoWindow;// 鍵盤を押したときのwindow
    [SerializeField]
    private GameObject keys;// windowが出る方の鍵盤のボタンの親を参照
    [SerializeField]
    private GameObject soundKeys;// 音を出す鍵盤のボタンの親を参照
    [SerializeField]
    private char[] answerPianoKey = new char[8];//答えの鍵盤（順番も反映）実際変えたらエラー出ます
    private int answerInt= 0;// answerPianoKeyを全部足す用の値
    private int yesInt = 0;// Yesを押したときの鍵盤の位置を取得
    [SerializeField]
    private AudioClip[] pianosSound = new AudioClip[8];// ピアノの音階
    [SerializeField]
    private AudioClip letterSound;// 手紙の時のSE
    [SerializeField]
    private AudioClip pianoOpenSound;// ピアノが開くときのSE

    private AudioSource AS;// 音を出すためのAudioSource
    List<GameObject> keysObj = new List<GameObject>();// keysの子供たち
    List<GameObject> soundKeysObj = new List<GameObject>();// soundKeysの子供たち
    bool checkKey;// 指定した鍵盤かどうかの判定用
    [SerializeField]
    private GameObject latterButton;// 手紙をとるボタン。ピアノの状態を確認して消す
    int[] pushCount = new int[8];

    [SerializeField]
    ItemObject item;

    // pianoの状態
    private enum PianoState
    {
        b0000 = 0, // piano 1 
        b0001 = 1, //       2 A
        b0010 = 2, //       3 B
        b0011 = 3, //       4 AB
        b0100 = 4, //       5 F
        b0101 = 5, //       6 AF
        b0110 = 6, //       7 BF
        b0111 = 7, //       8 ABFNormal
        b1000 = 8, //       9 ABFOpen
        b1001 = 9, //       10OpenLetter
        b1010 = 10, //      11OpenEnd
    }
    PianoState pianoImageState = PianoState.b0000;// ピアノの現在の状態を保存しておく用の変数
    // ここに鍵盤の再配置などのリセットも
    void SetUp()
    {
        latterButton.SetActive(false);
        //pianoForm.gameObject.SetActive(true);
        checkKey = false;
        AS = GetComponent<AudioSource>();
        answerInt = 0;
        foreach(Transform child in keys.transform)
        {
            keysObj.Add(child.gameObject);
        }
        foreach (Transform child in soundKeys.transform)
        {
            soundKeysObj.Add(child.gameObject);
        }
        for (int i = 0;i<answerPianoKey.Length;i++)
        { 
            answerInt += answerPianoKey[i] - '0';
        }
    }

    private void Start()
    {
        SetUp();
    }

    /// <summary>
    /// 対応の鍵盤をクリックした際にウィンドウを出す(ABF)
    /// </summary>
    public void PopUpWindow(int i)
    {
        latterButton.SetActive(false);
        yesInt = i;
        checkKey = true;
        pianoWindow.SetActive(true);
    }
    /// <summary>
    /// 対応以外の鍵盤をクリックした際にウィンドウを出す
    /// </summary>
    public void NonPopUpWindow()
    {
        latterButton.SetActive(false);
        checkKey = false;
        pianoWindow.SetActive(true);
    }
    public void SoundButton(int i)
    {
        if (pushCount[i] > 3 && (i == 1 || i == 2 || i == 5)) { return; }
        pushCount[i]++;
        AS.PlayOneShot(pianosSound[i]);
    }
    /// <summary>
    /// ウィンドウのYesを押したら
    /// </summary>
    public void YesPush()
    {
        latterButton.SetActive(true);
        //対応の鍵盤のボタン押したら動く
        if (checkKey)
        {
            answerInt -= yesInt;
            //押した鍵盤を外す
            keysObj[yesInt].gameObject.SetActive(false);
            soundKeysObj[yesInt].gameObject.SetActive(false);
            // yesIntを2進数計算用に6から4に
            yesInt = (yesInt == 6) ? 4 : yesInt;
            // ピアノのイメージを変更
            pianoImageState = (PianoState)System.Enum.ToObject(typeof(PianoState), (int)pianoImageState+yesInt); ;
            pianoImage.sprite = pianoSprite[(int)pianoImageState];
            SEController.instance.PlaySE(SEController.SEType.piano_key);
        }
        pianoWindow.SetActive(false);
        
        // 正解だったら
        if (answerInt <= 0&&(int)pianoImageState<9)
        {
            // ADVとピアノぱかー
            pianoImage.sprite = pianoSprite[(int)pianoImageState];
            StartCoroutine(PianoAnimation(0.5f));
            SEController.instance.PlaySE(SEController.SEType.piano_open);
        }
    }
    /// <summary>
    /// ウィンドウのNoをおしたら
    /// </summary>
    public void NoPush()
    {
        latterButton.SetActive(true);
        //window閉じる　+ ADVパート
        pianoWindow.SetActive(false);
    }

    /// <summary>
    /// ピアノの後ろにあるパネルを押したら閉じる
    /// </summary>
    public void ExitPianoForm()
    {
        // formを閉じる
        pianoForm.SetActive(false);
    }

    /// <summary>
    /// ピアノを時間によって見た目を変える
    /// </summary>
    /// <param name="wait">この時間待つ</param>
    private IEnumerator PianoAnimation(float wait)
    {
        yield return new WaitForSeconds(wait);
        AS.PlayOneShot(pianoOpenSound);
        pianoImage.sprite = pianoSprite[(int)++pianoImageState];
        yield return new WaitForSeconds(wait);
        pianoImage.sprite = pianoSprite[(int)++pianoImageState];
    }
    /// <summary>
    /// ボタン用の手紙をゲットするやつ
    /// </summary>
    public void GetLetter()
    {
        // アイテムの取得的なのここに
        if (item != null) { item.GetItem(); }

        // ピアノの絵を変更
        if (pianoImageState == PianoState.b1001) {
            
            pianoImageState = PianoState.b1010;
            pianoImage.sprite = pianoSprite[(int)pianoImageState];
            // 手紙の取得した時のSE
            //AS.PlayOneShot(letterSound);
        }
    }
    
}

