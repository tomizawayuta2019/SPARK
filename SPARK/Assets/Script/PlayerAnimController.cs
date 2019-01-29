using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour {
    [SerializeField]
    Animator anim;

    [SerializeField]
    List<SpriteSet> sprites;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    SpriteRenderer lightLayer;

    [System.Serializable]
    struct SpriteSet {
        public Sprite[] sprites;
    }

    public void SetBool(string name,bool value) {
        anim.SetBool(name, value);
    }

    public void SetTrigger(string name) {
        anim.SetTrigger(name);
    }

    private void LateUpdate()
    {
        //光の影響を受ける用レイヤーの画像更新
        lightLayer.sprite = spriteRenderer.sprite;
    }

    public void SetSpeed(float value)
    {
        anim.speed = value;
    }
}
