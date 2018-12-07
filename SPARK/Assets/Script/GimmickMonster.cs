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
    float monsterSpeed = 0.3f;
    [SerializeField]
    GameObject dropItem;
    bool isCamera = true;//モンスターにカメラが追従中か
    [SerializeField]
    GameObject monsterStartADV;
    [SerializeField]
    GameObject monsterDestADV;

    [SerializeField]
    StagePosition stagePosition;

    [SerializeField]
    private GameObject wall;//移動に邪魔な障害物

    private void Start()
    {
        transform.position = stagePosition.GetPosition();
        ShowScript show = monsterStartADV.transform.Find("ADVParts").GetComponent<ShowScript>();
        show.SetAction(new List<ShowTextAction>() {
            MonsterStart
        });

        monsterStartADV.SetActive(true);
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
        if (!monsterStartADV.activeSelf && isCamera && Mathf.Abs(PlayerController.instance.transform.position.x - transform.position.x) < 15) {
            EventCamera.instance.EndEventCamera();
            isCamera = false;
        }
    }

    void MonsterMove()
    {
        if (wall != null) { return; }
        transform.Translate(monsterSpeed*Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag) {
            //ライトに当たったら、
            case "Lught":
                //３秒後に死ぬ
                IEnumerator coroutine = DeadMonster(3f);
                StartCoroutine(coroutine);
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

        monsterDestADV.SetActive(true);

        while (monsterDestADV.activeSelf) { yield return null; }
        EventCamera.instance.EndEventCamera();
        yield return time;
    }

    private Animator GetAnim() {
        return transform.GetChild(0).GetComponent<Animator>();
    }
}
