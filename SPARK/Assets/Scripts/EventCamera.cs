using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCamera : SingletonMonoBehaviour<EventCamera> {
    [SerializeField]
    GameObject target;
    Coroutine coroutine;
    private Camera main;
    private Camera Main { get {
            if (main == null) { main = Camera.main; }
            return main; } }
    bool isChase = false;

    public void StartEventCamera(GameObject target,System.Action comp = null) {
        this.target = target;
        transform.position = Main.transform.position;
        if (comp != null) { coroutine = StartCoroutine(MoveToTarget(target, () => { isChase = true;comp(); })); }
        else { coroutine = StartCoroutine(MoveToTarget(target, () => { isChase = true; })); }
        Main.gameObject.SetActive(false);
        tag = "MainCamera";
        UIController.instance.list.Add(gameObject);
    }

    public IEnumerator StartEventCameraWait(GameObject target) {
        this.target = target;
        transform.position = Main.transform.position;
        Main.gameObject.SetActive(false);
        tag = "MainCamera";
        UIController.instance.list.Add(gameObject);

        yield return StartCoroutine(MoveToTarget(target,()=> { }));
    }

    private void Update()
    {
        if (isChase)
        {
            Vector3 targetPos = transform.position;
            targetPos.x = target.transform.position.x;
            transform.position = targetPos;
        }
    }

    public void EndEventCamera() {
        if (coroutine == null) { return; }
        StopCoroutine(coroutine);
        StartCoroutine(MoveToTarget(Main.gameObject, 
            () => {
                tag = "EventCamera";
                UIController.instance.list.Remove(gameObject);
                Main.gameObject.SetActive(true);
            }));
        isChase = false;
    }

    private IEnumerator MoveToTarget(GameObject target,System.Action comp) {
        while (Mathf.Abs(transform.position.x - target.transform.position.x) > 0.1f) {
            Vector3 targetPos = transform.position;
            targetPos.x = target.transform.position.x;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 10 * TimeManager.DeltaTime);
            yield return null;
        }
        comp();
    }
}
