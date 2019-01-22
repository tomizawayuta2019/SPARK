using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotationOffset : MonoBehaviour {
    [SerializeField] float offset;

    private Transform tran;
    private bool isLeft;

    private void Awake()
    {
        tran = transform;
    }

    public void LookToLeft()
    {
        isLeft = true;
        tran.localScale = new Vector3(-Mathf.Abs(tran.localScale.x), tran.localScale.y, tran.localScale.z);
        tran.localPosition = new Vector3(offset, 0, 0);
    }

    public void LookToRight()
    {
        isLeft = false;
        tran.localScale = new Vector3(Mathf.Abs(tran.localScale.x), tran.localScale.y, tran.localScale.z);
        tran.localPosition = Vector3.zero;
    }

    public void LookToBack()
    {
        if (isLeft) { LookToRight(); }
        else { LookToLeft(); }
    }
}
