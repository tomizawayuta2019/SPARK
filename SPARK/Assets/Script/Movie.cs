﻿using System.Collections;
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
    GameObject ADV, movieADV;
    [SerializeField]
    ShowScript show, movieShow;

    [SerializeField]
    GameObject finalEvent,monster;

    public override void Click()
    {
        //ADV.SetActive(true);
        //show.Restart();
        ItemUse(new ItemState());
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

        movieADV.SetActive(true);
        movieShow.Restart();

        StartCoroutine(Event());

        return true;
    }

    IEnumerator Event() {
        while (movieADV.activeSelf) {
            yield return null;
        }

        finalEvent.SetActive(true);

        yield return new WaitForSeconds(6.0f);
        finalEvent.SetActive(false);

        monster.SetActive(true);
    }
}
