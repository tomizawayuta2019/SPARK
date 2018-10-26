using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * ギミックの種類
 */ 
public class GimmickKind : MonoBehaviour {

    bool pass = false;

    public enum Kind{
        monster,
        item,
        stage,
        password,
    }
    public Kind gimmickKind;
    private void Update()
    {
        // モンスター召喚？
        if (Input.GetMouseButtonDown(0))
        {
            if (gimmickKind == Kind.password)
            {
                pass = !gameObject.transform.GetChild(0).gameObject.activeSelf;
                gameObject.transform.GetChild(0).gameObject.SetActive(pass);
            }
        }
    }

    //　特定の位置に来たらモンスター召喚
    public void SetMonster()
    {

    }

    public void PassWordInput()
    {

    }

}
