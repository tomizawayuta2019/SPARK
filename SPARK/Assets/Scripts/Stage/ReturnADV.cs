using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnADV : MonoBehaviour {
    [SerializeField]
    float returnRange;

    [SerializeField]
    GameObject ADV;
    [SerializeField]
    ShowScript showScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") { return; }

        ADV.SetActive(true);
        showScript.Restart();

        StartCoroutine(ADVWait());
    }

    IEnumerator ADVWait() {
        while (ADV.activeSelf) {
            yield return null;
        }

        PlayerController.instance.targetPosition = PlayerController.instance.transform.position + new Vector3(returnRange, 0);
    }
}
