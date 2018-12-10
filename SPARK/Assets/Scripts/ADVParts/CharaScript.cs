using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaScript : SingletonMonoBehaviour<CharaScript>
{
    //キャラ表示用のプレハブ
    [SerializeField]
    private GameObject charaPrefab;
    
    //キャラの立ち絵を表示する
    private void ShowChara(Sprite spr, Position charapos, Vector2 scale)
    {
        //基本構成
        GameObject chara = Instantiate(charaPrefab) as GameObject;
        //位置を代入するとキャラの位置がずれるバグがあったので子要素でlocalpositionを0に
        chara.transform.SetParent(ShowScript.instance.charaPotision[(int)charapos].transform);
        chara.transform.SetAsFirstSibling();
        chara.transform.localPosition = Vector3.zero;
        chara.GetComponent<Image>().sprite = spr;
        chara.transform.localScale = scale;
        //左右反転させる
        if (/*左右反転するかどうか*/false)
        {
            //Vector3 scale = chara.transform.localScale;
            //scale.x *= -1;
        }
        ShowScript.instance.stageChara[(int)charapos] = chara;
    }

    //メインキャラの切り替え
    private void TalkingChara(int activeCharaNum)
    {
        for (int i = 0; i < ShowScript.instance.stageChara.Length; i++)
        {
            CanvasGroup canvasgroup = null;
            if (ShowScript.instance.stageChara[i] != null)
            {
                canvasgroup = ShowScript.instance.stageChara[i].GetComponent<CanvasGroup>();
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
        if (ShowScript.instance.stageChara[potision] != null)
        {
            return true;
        }
        return false;
    }

    //キャラを切り替え
    public void CharaChange(int id, Sprite spr, Vector2 scale)
    {
        //キャラがまだ表示されてなかったら開く
        if (ShowScript.instance.positionList[id] != Position.empty &&
            Is_StayChara((int)ShowScript.instance.positionList[id]) == false)
        {
            ShowChara(spr ,ShowScript.instance.positionList[id], scale);
        }
        TalkingChara((int)ShowScript.instance.positionList[id]);
    }
}
