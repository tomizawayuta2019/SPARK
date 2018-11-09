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

    float monsterSpeed = 0.3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //モンスターが動くよ
        MonsterMove();
    }

    void MonsterMove()
    {
        //時間かけるのってどうなんですか
        transform.Translate(monsterSpeed*Time.deltaTime, 0, 0);
        
        // デバッグ用
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Translate(monsterSpeed, 0, 0);
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Translate(-monsterSpeed, 0, 0);
        //}
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.Translate(0,monsterSpeed, 0);
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.Translate(0, -monsterSpeed, 0);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ライトに当たったら、
        if (collision.gameObject.CompareTag("Light"))
        {
            //３秒後に死ぬ
            IEnumerator coroutine = DeadMonster(3f);
            StartCoroutine(coroutine);
        }

        //playerdead;
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("プレイヤーを殺す");
        }
        if (collision.gameObject.CompareTag("Gimmick"))
        {
            monsterSpeed = 0;
        }
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
