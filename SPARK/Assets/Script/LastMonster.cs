using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastMonster : MonoBehaviour {
    
    [SerializeField]
    Canvas gameOver;

    [SerializeField]
    float monsterSpeed = 0.3f;
    bool isCamera = true;// モンスターにカメラが追従中か
    [SerializeField]
    ShowScript.ADVType monsterStartADV;
    bool isHit = false;

    // Update is called once per frame
    void Update()
    {
        //モンスターが動くよ
        MonsterMove();
        // 使うかわからない
        //if (!monsterStartADV.activeSelf && isCamera && Mathf.Abs(PlayerController.instance.transform.position.x - transform.position.x) < 15)
        //{
        //    EventCamera.instance.EndEventCamera();
        //    isCamera = false;
        //}
    }

    void MonsterMove()
    {
        if (isHit) { return; }
        transform.Translate(monsterSpeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ここにムービー
        if (collision.gameObject.tag == "Player" && !isHit)
        {
            isHit = true;
            ShowScript.instance.EventStart(monsterStartADV, null, () => { StartCoroutine(PlayerHit()); });
        }
    }
    // ゲームオーバーのふりしてエンディング
    private IEnumerator PlayerHit()
    {
        //
        gameOver.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        ShowScript.instance.EventStart(ShowScript.ADVType.Ending_Black, null, () => 
        {
            gameOver.gameObject.SetActive(false);
            //StartCoroutine(Ending.instance.EndingMovie());
            MovieController.instance.StartEndingMovie();
        });
    }
}
