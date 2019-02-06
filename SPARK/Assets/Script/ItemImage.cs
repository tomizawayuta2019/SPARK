using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    public static ItemImage currentTargetImage;
    public bool isBigSizeItem;
    public ItemState item;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isBigSizeItem) { ItemView.instance.Click(); }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentTargetImage = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentTargetImage == this) { currentTargetImage = null; }
    }
}
