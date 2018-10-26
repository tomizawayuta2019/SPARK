using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/*
 *  ADVパートの管理役
 */

public class ADVSystemManager : MonoBehaviour
{
    //CSVの読み込み
    private CSVLoadScript csvLoadScript;
    //CSVのenum
    private enum CSVIndex
    {
        ID = 0,
        Person,
        PersonPos,
        Text
    }
    private CSVIndex _index;

    //キャラとテキスト表示
    private ShowScript showScript;

    //コルーチンを開始する
    public void Show(int eventNum)
    {

    }

	// Use this for initialization
	void Start ()
    {
        csvLoadScript = this.GetComponent<CSVLoadScript>();
        csvLoadScript.Make_textSources();
        showScript = this.GetComponent<ShowScript>();
        showScript.ShowChara();
	}
}
