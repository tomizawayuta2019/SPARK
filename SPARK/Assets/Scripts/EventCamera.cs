using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCamera : SingletonMonoBehaviour<EventCamera> {
    [SerializeField]
    GameObject target;
    Coroutine coroutine;
    private Camera main;
    bool isChase = false;

    private void Start()
    {
        main = Camera.main;
    }

    public void StartEventCamera(GameObject target) {
        this.target = target;
        transform.position = main.transform.position;
        coroutine = StartCoroutine(MoveToTarget(target, () => { isChase = true;}));
        main.gameObject.SetActive(false);
        UIController.instance.list.Add(gameObject);
    }

    public IEnumerator StartEventCameraWait(GameObject target) {
        this.target = target;
        transform.position = main.transform.position;
        main.gameObject.SetActive(false);
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
        StopCoroutine(coroutine);
        StartCoroutine(MoveToTarget(main.gameObject, 
            () => {
                tag = "EventCamera";
                UIController.instance.list.Remove(gameObject);
                main.gameObject.SetActive(true);
            }));
        isChase = false;
    }

    private IEnumerator MoveToTarget(GameObject target,System.Action comp) {
        while (Mathf.Abs(transform.position.x - target.transform.position.x) > 0.1f) {
            Vector3 targetPos = transform.position;
            targetPos.x = target.transform.position.x;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 10 * Time.deltaTime);
            yield return null;
        }
        comp();
    }
}
