using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movie : GimmickKind
{
    [SerializeField]
    private GameObject Noise;
    [SerializeField]
    private SpriteRenderer MovieSP;
    bool check = false;
    [SerializeField]
    private Sprite[] MovieSprites = new Sprite[2];
    public override void Click()
    {
        Debug.Log("ここでADVパートはいるよ！");
        check = (check) ? false:true;
        MovieSP.sprite = (check) ? MovieSprites[1] : MovieSprites[0];
        Noise.SetActive(check);

    }
}
