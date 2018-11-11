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

    private void Start()
    {
        EventCamera.instance.StartEventCamera(gameObject, () => monsterStartADV.SetActive(true));
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
        transform.Translate(monsterSpeed*Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ライトに当たったら、
        if (collision.gameObject.tag == "Light")
        {
            //３秒後に死ぬ
            IEnumerator coroutine = DeadMonster(3f);
            StartCoroutine(coroutine);
        }

        //playerdead;
        if (collision.gameObject.tag == "Player")
        {
            PlayerHit(collision.gameObject);
        }
    }

    private void PlayerHit(GameObject target) {
        GameController.instance.GameOver();
    }

    //　time秒後に死ぬ（time中にドロドロした演出を入れる）
    public IEnumerator DeadMonster(float time)
    {
        yield return StartCoroutine(EventCamera.instance.StartEventCameraWait(gameObject));

        while (time >= 0)
        {
            time -= Time.deltaTime;
            yield return time;
        }
        Destroy(gameObject);

        GameObject item = Instantiate(dropItem);
        item.transform.position = transform.position;

        monsterDestADV.SetActive(true);

        while (monsterDestADV.activeSelf) { yield return null; }
        EventCamera.instance.EndEventCamera();
        yield return time;
    }
}
