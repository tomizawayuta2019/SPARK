using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : SingletonMonoBehaviour<UIController> {
    public List<GameObject> list;

    public bool IsCanInput { get {
            return list.Count == 0;
        } }

    private void Update()
    {
        PlayerController.instance.SetPlayerActive(IsCanInput);
    }
}
