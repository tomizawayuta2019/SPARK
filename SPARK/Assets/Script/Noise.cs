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

    [SerializeField]
    RedMessage message;

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
            message.SetAlpha(alpha);
            yield return null;
            nowTime += TimeManager.DeltaTime;
        }

        color = end;
        renderer.material.color = color;
    }
}
