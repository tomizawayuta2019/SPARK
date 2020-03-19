using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll : MonoBehaviour {
    [SerializeField]
    GameObject target;

	// Update is called once per frame
	void Update () {
        if (target == null) {
            SEController.instance.PlaySE(SEController.SEType.doll);
            Destroy(this);
        }
	}
}
