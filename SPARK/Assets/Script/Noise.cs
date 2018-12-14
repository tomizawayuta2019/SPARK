using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * ノイズのスクリプト　Updateの必要性がなくなったためいつでもGimmickKind継承可能
 */
public class Noise : MonoBehaviour
{
    
    [SerializeField]
    float alpha;
    float speed = 0;
    bool flip = false;

    private void Update()
    {
        //透明度を参照しているalpha君に
        //ChangeAlpha(alpha);

        // 適当なムーブ
        //if (!flip&&transform.position.x < 3) {
        //    speed = 1f;
        //}else if (flip && transform.position.x > -3) {
        //    speed = -1f;
        //}
        //else {
        //    flip = (flip) ?false:true;
        //}
        //　動かしたらノイズ感が出る
        //transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    //呼び出しを受けたら動かしていきたいけどUpdate以外はよくわからない
    private void NoiseMove() {
        ChangeAlpha(alpha);
        
    }
    // 透明度を引数aで変える（0から1までで0が透明1が不透明）
    private void ChangeAlpha(float a) {
        GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 1, a));
    }

    public IEnumerator ChangeColor(Color start,Color end,float time) {
        Renderer renderer = GetComponent<Renderer>();
        Color color = renderer.material.color;
        color = start;
        alpha = start.a;
        renderer.material.color = color;

        float nowTime = TimeManager.DeltaTime;
        while (nowTime < time) {
            color = Color.Lerp(start, end, nowTime / time);
            alpha = color.a;
            renderer.material.color = color;
            yield return null;
            nowTime += TimeManager.DeltaTime;
        }

        color = end;
        renderer.material.color = color;
    }
}
