using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movie : GimmickKind ,IItemUse
{
    [SerializeField]
    private GameObject Noise;
    [SerializeField]
    private SpriteRenderer MovieSP;
    bool check = false;
    [SerializeField]
    private Sprite[] MovieSprites = new Sprite[2];

    [SerializeField]
    GameObject finalEvent,monster;

    public override void Click()
    {
        if (check) { return; }
        ShowScript.instance.EventStart(ShowScript.ADVType.Movie_Enter);
        PlayerController.instance.LookToBack(true);
        //ItemUse(new ItemState());
    }

    public bool IsCanUseItem(ItemState item)
    {
        return !check && item.itemType == ItemType.ticket;
    }

    public bool ItemUse(ItemState item)
    {
        MovieSP.sprite = MovieSprites[1];
        Noise.SetActive(true);
        check = true;

        ShowScript.instance.EventStart(ShowScript.ADVType.Movie_Start);
        PlayerController.instance.LookToBack(true);

        StartCoroutine(Event());

        return true;
    }

    IEnumerator Event() {
        while (ShowScript.instance.GetIsShow()) {
            yield return null;
        }

        finalEvent.SetActive(true);

        yield return new WaitForSeconds(6.0f);
        finalEvent.SetActive(false);

        monster.SetActive(true);
    }
}
