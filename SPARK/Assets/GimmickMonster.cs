using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * モンスターの行動
*/
[RequireComponent(typeof(GimmickKind))]
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

    float monsterSpeed = 0.1f;

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
        /*
        if (transform.position.x >= 7)
        {
            monsterSpeed = -0.2f;
        }
        else if (transform.position.x <= -7)
        {
            monsterSpeed = 0.2f;
        }
        transform.Translate(monsterSpeed, 0, 0);
        */
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(monsterSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-monsterSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0,monsterSpeed, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -monsterSpeed, 0);
        }
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
        Debug.Log("プレイヤーを殺す");
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
