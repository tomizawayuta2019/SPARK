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

    //xmlファイルの読み込み
    private XMLLoad xmlLoad;

    //キャラとテキスト表示
    private ShowScript showScript;

	// Use this for initialization
	void Start ()
    {
        //csvLoadScript = this.GetComponent<CSVLoadScript>();
        //csvLoadScript.Make_textSources();
        showScript = this.GetComponent<ShowScript>();
        xmlLoad = this.GetComponent<XMLLoad>();

        showScript.ShowChara((int)ShowScript.Potision.Left, (int)ShowScript.Chara.Neko);
        showScript.ShowChara((int)ShowScript.Potision.Right, (int)ShowScript.Chara.Penguin);
        showScript.TalkingChara((int)ShowScript.Potision.Left);
        showScript.ShowText(xmlLoad.GetContentsList());
        showScript.ChangeChara();
    }
}
