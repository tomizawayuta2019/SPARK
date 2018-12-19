using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * モンスターの行動
*/
public class GimmickMonster : MonoBehaviour {

    //シングルトン
    private static GimmickMonster monster;
    public static GimmickMonster MonsterInstance
    {
        get { return monster; }
    }
    private void Awake()
    {
        if (monster == null) { monster = this; }
        else { Destroy(gameObject); }
    }

    [SerializeField]
    int HP = 1;

    [SerializeField]
    float monsterSpeed = 0.3f;
    [SerializeField]
    GameObject dropItem;
    bool isCamera = true;//モンスターにカメラが追従中か
    [SerializeField]
    ShowScript.ADVType startADV,deathADV;

    [SerializeField]
    StagePosition stagePosition;

    [SerializeField]
    private GameObject wall;//移動に邪魔な障害物

    private void Start()
    {
        transform.position = stagePosition.GetPosition();
        ShowScript.instance.SetAction(new List<ShowTextAction>() {
            MonsterStart
        });

        ShowScript.instance.EventStart(startADV);
    }

    IEnumerator MonsterStart() {
        bool waitFlag = true;
        EventCamera.instance.StartEventCamera(gameObject, () => waitFlag = false);
        while (waitFlag) { yield return null; }
    }

    // Update is called once per frame
    void Update () {
        //モンスターが動くよ
        MonsterMove();
        if (ShowScript.instance.GetIsShow() && isCamera && Mathf.Abs(PlayerController.instance.transform.position.x - transform.position.x) < 15) {
            EventCamera.instance.EndEventCamera();
            isCamera = false;
        }
    }

    void MonsterMove()
    {
        if (wall != null) { return; }
        transform.Translate(monsterSpeed * TimeManager.DeltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag) {
            //ライトに当たったら、
            case "Lught":
                //３秒後に死ぬ
                HP -= 1;
                if (HP <= 0) {
                    IEnumerator coroutine = DeadMonster(3f);
                    StartCoroutine(coroutine);
                }
                break;
            case "Player":
                PlayerHit(collision.gameObject);
                break;
            case "Wall":
                wall = collision.gameObject;
                GetAnim().SetTrigger("AttackTrigger");
                break;
        }
    }

    private void PlayerHit(GameObject target) {
        GameController.instance.GameOver();
    }

    //　time秒後に死ぬ（time中にドロドロした演出を入れる）
    public IEnumerator DeadMonster(float time)
    {
        monsterSpeed = 0;
        yield return StartCoroutine(EventCamera.instance.StartEventCameraWait(gameObject));

        GetAnim().SetTrigger("DeathTrigger");
        yield return new WaitForSeconds(time);
        Destroy(gameObject);

        GameObject item = Instantiate(dropItem);
        item.transform.position = transform.position;

        ShowScript.instance.EventStart(deathADV);

        while (ShowScript.instance.GetIsShow()) { yield return null; }
        EventCamera.instance.EndEventCamera();
        yield return time;
    }

    //　time秒後に死ぬ（time中にドロドロした演出を入れる）
    public IEnumerator StopMonster(float time)
    {
        float swap = monsterSpeed;
        monsterSpeed = 0;
        yield return StartCoroutine(EventCamera.instance.StartEventCameraWait(gameObject));

        GetAnim().SetTrigger("DeathTrigger");
        yield return new WaitForSeconds(time);

        ShowScript.instance.EventStart(deathADV);

        while (ShowScript.instance.GetIsShow()) { yield return null; }
        EventCamera.instance.EndEventCamera();
        yield return time;

        monsterSpeed = swap;
    }

    private Animator GetAnim() {
        return transform.GetChild(0).GetComponent<Animator>();
    }
}
