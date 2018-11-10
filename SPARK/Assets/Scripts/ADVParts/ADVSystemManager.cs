using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/*
 *  ADVパートの管理役(...のはずだった)
 */

public class ADVSystemManager : MonoBehaviour
{
    //xmlファイルの読み込み
    private XMLLoad xmlLoad;

    //キャラとテキスト表示
    private ShowScript showScript;

	// Use this for initialization
	void Start ()
    {
        showScript = this.GetComponent<ShowScript>();
        xmlLoad = this.GetComponent<XMLLoad>();
    }
}
