using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnADV : MonoBehaviour {
    [SerializeField]
    float returnRange;
    [SerializeField]
    ShowScript.ADVType adv;
    bool isWait = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isWait || collision.gameObject.tag != "Player") { return; }

        isWait = true;
        ShowScript.instance.EventStart(adv, null, () => { PlayerController.instance.PlayerInputActive = false; });

        PlayerController.instance.PlayerInputActive = false;
        //StartCoroutine(ADVWait());
    }

    //public IEnumerator ADVWait() {
    //    PlayerController.instance.PlayerInputActive = false;
    //    while (ShowScript.instance.GetIsShow()) {
    //        yield return null;
    //    }
        
    //    PlayerController.instance.targetPosition = PlayerController.instance.transform.position + new Vector3(returnRange, 0);
    //    yield return new WaitForSeconds(0.5f);
    //    isWait = false;
    //    PlayerController.instance.PlayerInputActive = true;
    //}
}
