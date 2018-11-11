using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPopPoint : MonoBehaviour {
    [SerializeField]
    GameObject target;
    [SerializeField]
    Vector2 posPos;

    private void Start()
    {
        target.SetActive(true);
        Destroy(this);
    }
}
