using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WritingManager : MonoBehaviour {

    //全体的に作り直そうな

    [SerializeField]
    private GameObject Text; //テキストボックス
    private Text _text; //テキストボックスのテキスト
    [SerializeField]
    private List<string> Writing = new List<string>(); //文章(行区切り)
    [SerializeField]
    private List<string> End_Writing = new List<string>(); //文章(タグ区切り)
    TextAsset Sheet = null; //読み込む文章のファイル
    private string[] hoge; //区切った後の文章を入れる所
    private List<string> Tags = new List<string>(); //タグを登録する
    private List<int[]> TagList = new List<int[]>(); //タグがあった場所とタグナンバー


    // Use this for initialization
    void Start ()
    {
        _text = Text.GetComponent<Text>();
        _Make();
	}

    int PageNum = 0;
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            WritingChange(End_Writing[PageNum]);
            PageNum++;
            if(PageNum == End_Writing.Count)
            {
                SceneManager.LoadScene("Main");
            }
        }
	}

    //文章を作る関数まとめ
    private void _Make()
    {
        AddList();
        CheckTags();
        MakeWriting();
    }


    //文章を行で区切る
    //SheetPosに参照場所を指定
    private void AddList(string SheetPos = "Sheet/Text01")
    {
        //文章ファイルを読み込む
        Sheet = Resources.Load(SheetPos, typeof(TextAsset)) as TextAsset;
        //文章を読む
        string str = Sheet.text;
        //改行で区切る
        hoge = str.Split('\n');
        for(int i = 0; i < hoge.Length; i++)
        {
            //リストにぶっこむ
            Writing.Add(hoge[i]);
        }
    }

    //タグを調べる
    private void CheckTags()
    {
        TagReference();
        for (int i = 0; i < Writing.Count; i++)
        {
            for(int Tagnum = 0; Tagnum < Tags.Count; Tagnum++)
            {
                //登録されているタグかどうか調べる
                if (Writing[i] == Tags[Tagnum])
                {
                    //タグがあった場所とタグの種類を入れる
                    TagList.Add(new int[] { i, Tagnum });
                    //デバッグ用
                    switch(Tagnum)
                    {
                        case 0:
                            Debug.Log("改ページタグ");
                            break;
                        case 1:
                            Debug.Log("太文字タグ");
                            break;
                        case 2:
                            Debug.Log("EOFタグ");
                            break;
                        default:
                            Debug.Log("何だそれ");
                            break;
                    }
                    break;
                }
            }
        }
    }

    //登録したタグを読み込む
    private void TagReference()
    {
        //タグ登録を書いたファイルを読み込む
        TextAsset Tag = Resources.Load("Definition/Tag", typeof(TextAsset)) as TextAsset;
        //タグを入れる(ここはAddListとだいたい一緒)
        string str = Tag.text;
        hoge = str.Split('\n');
        for(int i = 0; i < hoge.Length; i++)
        {
            Tags.Add(hoge[i]);
        }
    }

    //タグ区切りで文章を作る
    private void MakeWriting()
    {
        //今の行数
        int nowRownum = 0;
        //次に来るタグの配列番号
        int nextTagnum = 0;
        //次に来るタグの行数
        int nextTagrownum = TagList[nextTagnum][0];
        //次に来るタグの種類
        int nextTagType = TagList[nextTagnum][1];
        //EOFタグのbool
        bool Tag_EOF = false;

        string str = "";
        while(true)
        {
            Debug.Log(Writing[nowRownum]);
            if(nowRownum == nextTagrownum)
            {
                switch(nextTagType)
                {
                    case 0:
                        End_Writing.Add(str);
                        break;
                    case 1:
                        Debug.Log("太文字になる予定");
                        break;
                    case 2:
                        Tag_EOF = true;
                        Debug.Log("EOF");
                        //ここで引いとかないとエラーになる(他の方法を考え中)
                        nextTagnum = 0;
                        break;
                }
                nextTagnum++;
                Debug.Log(TagList[nextTagnum][0] + "," + TagList[nextTagnum][1]);
                nextTagrownum = TagList[nextTagnum][0];
                nextTagType = TagList[nextTagnum][1];
                str = "";
            }
            else
            {
                if(str != "")
                {
                    str = str + "\n";
                }
                str = str + Writing[nowRownum];
            }
            if (Tag_EOF)
            {
                End_Writing.Add(str);
                break;
            }
            nowRownum++;
        }
    }

    //文章を表示
    private void WritingChange(string show)
    {
        _text.text = show;
        StartCoroutine("_WritingChange");
    }

    IEnumerator _WritingChange()
    {
        int Mojisuu = End_Writing[PageNum].Length;
        Debug.Log(End_Writing[PageNum]);
        Debug.Log(Mojisuu);
        yield return null;
    }
}
