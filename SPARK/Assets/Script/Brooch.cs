using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brooch : GimmickKind , IItemUse
{
    private Vector2 offset; //　初期位置
    // timeSpeedは速さ、ratioは割合（どこで落ちるか）、angleは基準の角度
    public float timeSpeed, ratio, angle;　// おすすめ(0.1,0.5,30)
    [SerializeField]
    GameObject Clow;
    [SerializeField]
    GameObject throwTarget;
    [SerializeField]
    GameObject effect;

    bool isEnd;

    /// <summary>
    /// プレイヤーの位置からブローチを投げる
    /// </summary>
    /// <param name="vec">playerの位置</param>
    public void ThrowBrooch(Vector2 vec)
    {
        offset = vec;
        // ブローチの位置をプレイヤーの位置に
        transform.position = vec;

        StartCoroutine(PosMove(throwTarget.transform.position));

    }

    /// <summary>
    /// ブローチを目的の位置に移動させる
    /// </summary>
    /// <param name="vec">目的の位置</param>
    /// <returns></returns>
    IEnumerator PosMove(Vector2 vec)
    {
        // 0が初期値　1が終点　全体の割合   
        float t = 0f;
        //目的地から現在地の差分
        Vector2 P2 = vec - offset;
        // 総移動距離を求める（直線）
        float distance = Vector2.Distance(offset, new Vector2(vec.x,vec.y));
        // 最大角度
        float max_angle = 50f;
        // 基準の距離
        float base_range = 5f;
        // 実際の角度 = 基準の角度 * 実際の距離 / 基準の距離
        angle = angle * distance / base_range;
        if (angle > max_angle)
        {
            angle = max_angle;
        }
        float P1x = P2.x * ratio;
        //angle * Mathf.Deg2Rad 角度からラジアンへ変換
        float P1y = Mathf.Sin(angle * Mathf.Deg2Rad) * Mathf.Abs(P1x) / Mathf.Cos(angle * Mathf.Deg2Rad);
        Vector2 P1 = new Vector2(P1x, P1y);
        // 終点についたらおしまい
        while (t <= 1)
        {
            // [x,y]=(1–t)2P0+2(1–t)tP1+t2P2
            float Vx = 2 * (1f - t) * t * P1.x + Mathf.Pow(t, 2) * P2.x + offset.x;
            float Vy = 2 * (1f - t) * t * P1.y + Mathf.Pow(t, 2) * P2.y + offset.y;
            transform.position = new Vector2(Vx, Vy);
            // １秒あたりのtの変化量 *1フレーム
            t += 1 / distance / timeSpeed * Time.deltaTime;

            yield return null;
        }

        transform.position = vec;

        effect.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        SEController.instance.PlaySE(SEController.SEType.drop_brooch);

        //カラスを飛ばすよ
        Clow.GetComponent<Clows>().FlyAway();
        //gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        effect.SetActive(false);
    }

    public bool IsCanUseItem(ItemState item)
    {
        return !isEnd && item.itemType == ItemType.brooch;
    }

    public bool ItemUse(ItemState item)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        ThrowBrooch(PlayerController.instance.transform.position);
        isEnd = true;
        return true;
    }
}
