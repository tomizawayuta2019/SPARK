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

    [SerializeField] Animator anim;
    SpriteRenderer sr;

    [SerializeField] bool isZeroAlpha;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        if (isZeroAlpha)
        {
            SetAlpha(0);
            PlayerController.instance.PlayerInputActive = false;
        } 
    }

    private void SetAlpha(float value)
    {
        Color color = sr.color;
        color.a = value;
        sr.color = color;
    }

    protected virtual void Start()
    {
        //if (isMoveStart) { MoveStart(); }


        switch (type)
        {
            case HaruType.gameStart:
                StartCoroutine(Event());
                break;
            case HaruType.messageDrop:
                //ShowScript.instance.EventStart(adv, new List<ShowTextAction>() { MoveStartAction });
                break;
            default:
                break;
        }
    }

    private IEnumerator Event()
    {
        PlayerController.instance.WakeUp();
        yield return new WaitForSeconds(2.0f);
        ShowScript.instance.EventStart(adv, new List<ShowTextAction>() { WaitForMoveEnd }, () => { yajirusiInUIController.instance.StartTutorial(); });
    }

    public virtual void MoveStart() {
        if (isMoveEnd) { return; }
        isMoveStart = true;
        UIController.instance.list.Add(gameObject);
    }

    // Update is called once per frame
    void Update () {
        if (!isMoveStart)
        {
            anim.SetBool("IsWalk", false);
            return;
        }
        if (waitTime > 0) {
            waitTime -= TimeManager.DeltaTime;
            anim.SetBool("IsWalk", false);
            return;
        }

        Vector3 gotoPos = transform.position + new Vector3(speed * TimeManager.DeltaTime, 0, 0);
        //transform.position = Vector3.MoveTowards(transform.position, gotoPos,TimeManager.DeltaTime);
        transform.position = gotoPos;
        anim.SetBool("IsWalk", true);
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
        float time = 0;
        while (time < 1)
        {
            time += TimeManager.DeltaTime;
            SetAlpha(time);
            yield return null;
        }
        SetAlpha(1);
        MoveStart();

        while (!isMoveEnd) {
            yield return null;
        }

        Destroy(gameObject);
        UIController.instance.list.Remove(gameObject);
        PlayerController.instance.PlayerInputActive = true;
    }

}
