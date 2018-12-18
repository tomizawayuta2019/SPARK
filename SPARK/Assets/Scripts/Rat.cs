using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour {
    [SerializeField]
    GameObject cap;
    [SerializeField]
    GameObject item;

    public void DropCap() {
        cap.SetActive(false);
        item.SetActive(true);
        SEController.instance.PlaySE(SEController.SEType.button);
    }
}
