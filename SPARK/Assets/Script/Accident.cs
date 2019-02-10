using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accident : GimmickKind
{
    [SerializeField]
    private GameObject Flower;
    [SerializeField] float flowerPosY;
    [SerializeField]
    private GameObject WaterSp;
    [SerializeField]
    private Sprite[] waterSprites = new Sprite[16];
    [SerializeField]
    private GameObject monster;

    [SerializeField] ShowScript.ADVType StartADV;

    bool AccidentC = false;

    private void Start()
    {
        WaterSp.SetActive(false);
    }

    [ContextMenu("Click")]
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
            yield return null;
            Flower.transform.Translate( 0f, -0.1f - ((gt += TimeManager.DeltaTime) * 0.98f), 0);
            if (flowerPosY >= Flower.transform.localPosition.y)
            {
                check = true;
            }
        }
        Flower.transform.localPosition = new Vector3(Flower.transform.localPosition.x, flowerPosY, Flower.transform.localPosition.z);


        // ここにADV


        
        monster.gameObject.SetActive(true);
        UIController.instance.list.Remove(gameObject);
    }
    //　SE
    IEnumerator AccidentSE()
    {
        ShowScript.instance.EventStart(StartADV);
        yield return null;
        while (ShowScript.instance.GetIsShow()) { yield return null; }

        yield return new WaitForSeconds(0.1f);
        SEController.instance.PlaySE(SEController.SEType.car);
        yield return new WaitForSeconds(0.3f);
        SEController.instance.PlaySE(SEController.SEType.car_break);
        yield return new WaitForSeconds(0.1f);
        // 黒いドロドロ
        StartCoroutine(BlackWater());
    }
}
