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
        GameObject defImage = ShowScript.instance.stageChara[(int)charapos];
        float alpha = 0f;
        if (defImage != null)
        {
            if (defImage.GetComponent<Image>().sprite == spr) { return; }
            else
            {
                Destroy(defImage);
                alpha = 1;
            }
        }

        //基本構成
        GameObject chara = Instantiate(charaPrefab) as GameObject;
        //位置を代入するとキャラの位置がずれるバグがあったので子要素でlocalpositionを0に
        chara.transform.SetParent(ShowScript.instance.charaPotision[(int)charapos].transform);
        chara.transform.SetAsFirstSibling();
        chara.transform.localPosition = Vector3.zero;
        chara.GetComponent<Image>().sprite = spr;
        chara.transform.localScale = scale;

        chara.GetComponent<CanvasGroup>().alpha = alpha;
        
        ShowScript.instance.stageChara[(int)charapos] = chara;
        //Debug.Log(spr);
    }

    //メインキャラの切り替え
    private IEnumerator TalkingChara(int activeCharaNum)
    {
        int time = 20;

        List<CanvasGroup> canvasGroup = new List<CanvasGroup>();
        List<float> alpha = new List<float>();
        List<float> diffAlpha = new List<float>();

        for (int i = 0; i < ShowScript.instance.stageChara.Length; i++)
        {
            if (ShowScript.instance.stageChara[i] != null)
            {
                canvasGroup.Add(ShowScript.instance.stageChara[i].GetComponent<CanvasGroup>());
                alpha.Add(canvasGroup[canvasGroup.Count - 1].alpha);

                if (i == activeCharaNum)
                {
                    diffAlpha.Add((1f - alpha[alpha.Count - 1]) / time);
                }
                else
                {
                    diffAlpha.Add((0.5f - alpha[alpha.Count - 1]) / time);
                }
            }
        }

        while (time > 0)
        {
            time--;
            for(int i = 0; i < canvasGroup.Count; i++)
            {
                alpha[i] += diffAlpha[i];
                canvasGroup[i].alpha = alpha[i];
            }
            yield return null;
        }
        yield break;
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
    public IEnumerator CharaChange(int id, Sprite spr, Vector2 scale)
    {
        //キャラがまだ表示されてなかったら開く
        if (ShowScript.instance.positionList[id] != Position.empty
            // && Is_StayChara((int)ShowScript.instance.positionList[id]) == false
            )
        {
            ShowChara(spr, ShowScript.instance.positionList[id], scale);
        }
        yield return TalkingChara((int)ShowScript.instance.positionList[id]);
        yield break;
    }
}
