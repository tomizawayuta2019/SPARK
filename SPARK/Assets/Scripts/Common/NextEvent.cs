using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextEvent : MonoBehaviour {
    [SerializeField]
    List<GameObject> eventObjs;
    [SerializeField]
    CallType type;
    [SerializeField]
    bool isActive = true;

    public enum CallType {
        Awake,
        Destroy,
        TriggerEnter,
    }

    private void Awake()
    {
        if (type != CallType.Awake) { return; }
        StartNextEvent();
    }

    private void OnDestroy()
    {
        if (type != CallType.Destroy) { return; }
        StartNextEvent();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type != CallType.TriggerEnter) { return; }
        StartNextEvent();
    }

    private void OnDisable()
    {
        if (type != CallType.Destroy) { return; }
        StartNextEvent();
    }

    private void StartNextEvent() {
        foreach (GameObject obj in eventObjs) {
            if (obj == null) { continue; }
            obj.SetActive(isActive);
        }
    }
}
