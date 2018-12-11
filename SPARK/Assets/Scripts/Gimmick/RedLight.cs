using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLight : MonoBehaviour, IItemUse
{
    [SerializeField]
    Light targetLight;

    [SerializeField]
    Color targetColor;

    [SerializeField]
    GameObject targetObj;

    bool isEventEnd = false;

    public bool IsCanUseItem(ItemState item)
    {
        return true;
        return !isEventEnd && item.itemType == ItemType.red_lighting;
    }

    public bool ItemUse(ItemState item)
    {
        targetLight.color = targetColor;
        targetObj.SetActive(true);
        return true;
    }
}
