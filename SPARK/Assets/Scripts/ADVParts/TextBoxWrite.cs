using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/******************************
 * テキストボックスを更新する *
 ******************************/

public class TextBoxWrite : SingletonMonoBehaviour<TextBoxWrite>
{
    //scriptableobjectを入れる
    [SerializeField]
    private CharaTable charaTable;

    //scriptableobjectにしたい(暫定版としてここから呼び出す)
    private XMLLoad xml;

    //読み込んだデータ入れ
    private ScenarioData activeData;

    //
    [System.NonSerialized]
    public bool textBreaing = false;
    [System.NonSerialized]
    public bool textWriting = false;

    //テキストの速さ
    [SerializeField]
    private float textSpeed = 0.05f;

    //コルーチン
    private Coroutine textCor;
    private Coroutine PageIconCor;

    //アイコン
    [SerializeField]
    private GameObject textIcon;
    private Transform textIconPos;
    private GameObject icon;
    
    [SerializeField]
    private bool isLineBreakIgnore;

    /// <summary>
    /// テキストボックスの表示
    /// </summary>
    /// <param name="textBoxAnim">表示時のアニメーション</param>
    public IEnumerator TextBoxAnim(int textBoxAnim = 1)
    {
        GameObject obj = ShowScript.instance.textBox.transform.Find("TextBox").gameObject;
        ShowScript.instance.textBox.SetActive(true);
        if (textBoxAnim == 0)
        {
            obj.GetComponent<CanvasGroup>().alpha = 1f;
            yield break;
        }
        else
        {
            float alpha = 0f;
            while (alpha <= 1f)
            {
                alpha += 0.04f;
                obj.GetComponent<CanvasGroup>().alpha = alpha;
                yield return null;
            }
            yield break;
        }
    }

    public void UpdateTexts(int id)
    {
        //キャラの名前を表示
        ShowScript.instance.charaText.text = charaTable.GetChara(ShowScript.instance.personList[id]);
        textCor = StartCoroutine(textLoad(ShowScript.instance.contentsList[id]));
    }

    //テキストをリアルタイムで更新
    IEnumerator textLoad(string text)
    {
        string str = "";
        string tgt = "";
        int strLength = 0;
        textBreaing = false;

        while (true)
        {
            while (true)
            {
                textWriting = true;
                tgt = text.Substring(strLength, 1);
                str = str + tgt;
                ShowScript.instance.mainText.text = str;
                strLength++;
                if (tgt == "\n" && !isLineBreakIgnore)
                {
                    textWriting = false;
                    PageIconCor = StartCoroutine(PageIconMove());
                    break;
                }
                if (strLength >= text.Length)
                {
                    textWriting = false;
                    StopCoroutine(textCor);
                    textCor = null;
                    textBreaing = true;
                    PageIconCor = StartCoroutine(PageIconMove());
                    yield break;
                }
                yield return new WaitForSeconds(textSpeed / TimeManager.TimePer);
            }
            while (!ShowScript.instance._input)
            {

                yield return null;
            }
        }
    }

    //右下のアレ
    private IEnumerator PageIconMove()
    {
        icon = Instantiate(textIcon);
        textIconPos = ShowScript.instance.textBox.transform.Find("pos_Icon");
        icon.transform.SetParent(textIconPos);
        RectTransform rect = icon.GetComponent<RectTransform>();
        rect.localPosition = Vector3.zero;
        Vector3 rot = rect.eulerAngles;
        while (rect != null)
        {
            rot.x += 6;
            rot.y += 6;
            rot.z += 6;
            rect.eulerAngles = rot;
            yield return null;
        }
    }

    //右下のアレを止める
    public void BreakPageIcon()
    {
        if(PageIconCor != null)
        {
            StopCoroutine(PageIconCor);
            PageIconCor = null;
            Destroy(icon);
        }
    }
}
