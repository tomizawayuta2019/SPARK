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
}
