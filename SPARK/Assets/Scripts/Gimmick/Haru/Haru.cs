using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haru : MonoBehaviour {
    protected enum HaruType {
        gameStart,
        messageDrop,
        phone,
    }

    [SerializeField]
    protected HaruType type;

    [SerializeField]
    float speed;
    [SerializeField]
    float waitTime;

    [SerializeField]
    protected ShowScript.ADVType adv;

    [SerializeField]
    protected bool isMoveStart;
    bool isMoveEnd;

    protected virtual void Start()
    {
        //if (isMoveStart) { MoveStart(); }


        switch (type)
        {
            case HaruType.gameStart:
                ShowScript.instance.EventStart(adv, new List<ShowTextAction>() { WaitForMoveEnd });
                break;
            case HaruType.messageDrop:
                //ShowScript.instance.EventStart(adv, new List<ShowTextAction>() { MoveStartAction });
                break;
            default:
                break;
        }
    }

    public virtual void MoveStart() {
        if (isMoveEnd) { return; }
        isMoveStart = true;
        UIController.instance.list.Add(gameObject);
    }

    // Update is called once per frame
    void Update () {
        if (!isMoveStart) { return; }
        if (waitTime > 0) {
            waitTime -= TimeManager.DeltaTime;
            return;
        }

        Vector3 gotoPos = transform.position + new Vector3(speed * TimeManager.DeltaTime, 0, 0);
        //transform.position = Vector3.MoveTowards(transform.position, gotoPos,TimeManager.DeltaTime);
        transform.position = gotoPos;
    }

    private void OnBecameInvisible()
    {
        MoveEnd();
    }

    protected virtual void MoveEnd() {
        //haruADV.SetActive(true);
        isMoveEnd = true;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    protected IEnumerator MoveStartAction() {
        isMoveStart = true;
        yield return null;
    }

    IEnumerator WaitForMoveEnd() {
        MoveStart();

        while (!isMoveEnd) {
            yield return null;
        }

        Destroy(gameObject);
        UIController.instance.list.Remove(gameObject);
    }

}
