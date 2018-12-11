using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPopPoint : MonoBehaviour {
    [SerializeField]
    GameObject target;

    public enum MonsterType {
        start,
        dest,
    }

    [SerializeField]
    MonsterType type;

    private void Start()
    {
        if (type == MonsterType.start) {
            target.SetActive(true);
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (target == null) { return; }
        if (type == MonsterType.dest)
        {
            target.SetActive(true);
        }
    }
}
