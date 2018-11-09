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
	
	// Update is called once per frame
	void Update () {
        //モンスターが動くよ
        MonsterMove();
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
        Debug.Log("playerHit");
    }

    //　time秒後に死ぬ（time中にドロドロした演出を入れる）
    private IEnumerator DeadMonster(float time)
    {
        while (time >= 0)
        {
            time -= Time.deltaTime;
            yield return time;
        }
        Destroy(gameObject);
        yield return time;
    }
}
