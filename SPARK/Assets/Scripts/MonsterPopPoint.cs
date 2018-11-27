using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPopPoint : MonoBehaviour {
    [SerializeField]
    GameObject target;

    private void Start()
    {
        target.SetActive(true);
        Destroy(this);
    }
}
