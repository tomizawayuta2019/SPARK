using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildEffect : MonoBehaviour {
    [SerializeField]
    GameObject effectPrefab;
    [SerializeField]
    bool isKeepPrefabScale;//親のサイズではなく、プレファブのサイズと同じにするか
    GameObject effectInstance;

    private void Awake()
    {
        effectInstance = Instantiate(effectPrefab);

        if (isKeepPrefabScale)
        {
            transform.localScale = effectPrefab.transform.localScale;
            transform.eulerAngles = effectPrefab.transform.eulerAngles;
        }

        effectInstance.transform.SetParent(transform);
        effectInstance.transform.localPosition = Vector3.zero;
    }

    public void RemoveParent() {
        effectInstance.transform.SetParent(null);
        Animator anim = effectInstance.transform.GetChild(0).GetComponent<Animator>();
        anim.SetTrigger("ItemGet");
        PlayerController.instance.StartCoroutine(WaitAnim("Base Layer.itemGet", anim,() => Destroy(effectInstance)));
    }

    IEnumerator WaitAnim(string animName,Animator anim,System.Action cmp) {
        yield return null;
        while(anim.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash(animName)) {
            yield return null;
        }

        cmp();
    }
}
