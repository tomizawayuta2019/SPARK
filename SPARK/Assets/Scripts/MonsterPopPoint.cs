using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPopPoint : MonoBehaviour {
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    Vector2 posPos;

    private void Start()
    {
        GameObject pop = Instantiate(prefab);
        pop.transform.position = posPos;
        Destroy(this);
    }
}
