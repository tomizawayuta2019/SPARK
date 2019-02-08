using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMonster : GimmickMonster {
    [SerializeField]
    LoopGround loop;

    [SerializeField] int HP;
    [SerializeField] ShowScript.ADVType notDeathADV;

    protected override void Start()
    {
        loop.gameObject.SetActive(true);
        loop.SetLoopObject(gameObject);
        base.Start();
    }

    public override IEnumerator DeadMonster(float time)
    {
        if (--HP <= 0) { yield return PlayerController.instance.StartCoroutine(base.DeadMonster(time)); }
        else { yield return PlayerController.instance.StartCoroutine(NotDeathMonster(time)); }
    }

    private IEnumerator NotDeathMonster(float time)
    {
        Stop();
        yield return StartCoroutine(EventCamera.instance.StartEventCameraWait(gameObject));

        GetAnim().SetTrigger("NotDeathTrigger");
        yield return new WaitForSeconds(time);
        
        ShowScript.instance.EventStart(notDeathADV);

        while (ShowScript.instance.GetIsShow()) { yield return null; }
        EventCamera.instance.EndEventCamera();
        Restart();
    }
}
