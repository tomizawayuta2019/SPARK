using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : SingletonMonoBehaviour<UIController> {
    public List<GameObject> list;
    [SerializeField]
    private bool isCanInput = true;
    private bool IsCanInput { get {
            list.RemoveAll((item) => item == null);
            return list.Count == 0; ;
        } }

    private void LateUpdate()
    {
        PlayerController.instance.SetPlayerActive(isCanInput);

        if (!isCanInput && Input.GetMouseButton(0)) { return; }//マウス長押しに反応してほしくないので、ここでreturn
        isCanInput = IsCanInput;
    }
}
