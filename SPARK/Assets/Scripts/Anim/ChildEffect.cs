using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildEffect : MonoBehaviour {
    [SerializeField]
    GameObject effectPrefab;
    [SerializeField]
    bool isKeepPrefabScale;//親のサイズではなく、プレファブのサイズと同じにするか
    [SerializeField]
    bool isKeepPrefabRotation = true;
    GameObject effectInstance;

    Quaternion defRotation;

    private void Awake()
    {
        effectInstance = Instantiate(effectPrefab);

        if (isKeepPrefabScale)
        {
            effectInstance.transform.localScale = effectPrefab.transform.localScale;
            effectInstance.transform.eulerAngles = effectPrefab.transform.eulerAngles;
        }

        if (isKeepPrefabRotation)
        {
            defRotation = effectInstance.transform.rotation;
        }

        effectInstance.transform.SetParent(transform);
        effectInstance.transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        if (isKeepPrefabRotation)
        {
            effectInstance.transform.rotation = defRotation;
        }
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
