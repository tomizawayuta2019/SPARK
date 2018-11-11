using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * ギミックの種類
 */
public class GimmickKind : MonoBehaviour {
    public bool isClickOnly = false;//位置に関係なく使用できるギミックか

    /// <summary>
    /// クリックされた際の処理
    /// </summary>
    public virtual void Click() {

    }
}
