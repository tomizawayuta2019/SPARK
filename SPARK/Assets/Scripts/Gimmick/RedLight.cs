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

    [SerializeField]
    Animator busAnim;

    bool isEventEnd = false;
    bool isEnterMonster = false;
    Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    public bool IsCanUseItem(ItemState item)
    {
        //return true;
        return !isEventEnd && item.itemType == ItemType.red_lighting;
    }

    public bool ItemUse(ItemState item)
    {
        SEController.instance.PlaySE(SEController.SEType.set_light);
        targetLight.color = targetColor;
        targetObj.SetActive(true);
        busAnim.SetTrigger("RedLightTrigger");
        return true;
    }

    private void Update()
    {
        if (!isEnterMonster) { rig.WakeUp(); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            busAnim.SetTrigger("LightDeleteTrigger");
            isEnterMonster = true;
        }
    }
}
