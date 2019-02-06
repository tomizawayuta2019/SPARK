using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMonster : GimmickMonster {
    [SerializeField]
    LoopGround loop;

    protected override void Start()
    {
        loop.gameObject.SetActive(true);
        loop.SetLoopObject(gameObject);
        base.Start();
    }
}
