using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accident : GimmickKind
{
    [SerializeField]
    private GameObject Flower;
    [SerializeField]
    private GameObject WaterSp;
    [SerializeField]
    private Sprite[] waterSprites = new Sprite[16];
    [SerializeField]
    private GameObject monster;

    bool AccidentC = false;

    private void Start()
    {
        WaterSp.SetActive(false);
    }

    public override void Click()
    {
        base.Click();
        // 事故のSE 一度だけ
        if (AccidentC) return;
        StartCoroutine(AccidentSE());
        AccidentC = true;
    }

    // 黒いドロドロから花が落ちて
    IEnumerator BlackWater()
    {
        bool check = false;
        float gt = 0;
        SpriteRenderer BWSPR = WaterSp.GetComponent<SpriteRenderer>();
        WaterSp.SetActive(true);
        //黒いドロドロ
        while (true)
        {
            for(int i = 0; i < waterSprites.Length; i++)
            {

                BWSPR.sprite = waterSprites[i];
                yield return new WaitForSeconds(Time.deltaTime * 8);
            }
            break;
        }
        // 花が落ちるよ
        while (!check)
        {
            Flower.transform.Translate( 0f, -0.1f - ((gt += Time.deltaTime) * 0.98f), 0);
            if (transform.position.y+0.5f >= Flower.transform.position.y)
            {
                check = true;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }

        // ここにADV


        
        monster.gameObject.SetActive(true);

        yield return null;
    }
    //　SE
    IEnumerator AccidentSE()
    {
        //SEController.instance.PlaySE();//ここにブレーキ音
        yield return new WaitForSeconds(2.0f);
        //SEController.instance.PlaySE();//ここにドン音
        yield return new WaitForSeconds(1.0f);
        // 黒いドロドロ
        StartCoroutine(BlackWater());
    }
}
