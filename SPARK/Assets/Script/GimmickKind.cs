using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * ギミックの種類
 */
public class GimmickKind : MonoBehaviour {

    bool pass = false;
    

    public enum Kind{
        monster,
        item,
        stage,
        master,
        door,
    }

    private void Start()
    {
    }
    
    private void Update()
    {
        // モンスター召喚？
        //ActiveSelfObject();
    }

   

    /// <summary>
    /// 特定の位置に来たらモンスター召喚
    /// </summary>
    public void SetMonster()
    {

    }


    /// <summary>
    /// クリックされた際の処理
    /// </summary>
    public virtual void Click() {
    }
}