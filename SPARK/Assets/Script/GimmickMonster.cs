using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * モンスターの行動
*/
public class GimmickMonster : MonoBehaviour
{

    //シングルトン
    private static GimmickMonster monster;
    public static GimmickMonster Instance
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
    bool isMove = true;
    [SerializeField]
    ShowScript.ADVType startADV, deathADV;
    private SpriteRenderer monsterSPR;

    [SerializeField]
    StagePosition stagePosition;

    [SerializeField]
    private float acceleration = 1; // 時間で加速するときの倍率
    [SerializeField] float acceTime;//何秒で最大速度に達するか
    float nowAcce = 0;
    [SerializeField]
    private float growing = 1; // 時間で大きくなるときの倍率

    protected virtual void Start()
    {
        monsterSPR = transform.GetChild(0).GetComponent<SpriteRenderer>();
        transform.position = stagePosition.GetPosition();

        ShowScript.instance.EventStart(startADV, new List<ShowTextAction>() {
            MonsterStart
        });
    }

    IEnumerator MonsterStart()
    {
        bool waitFlag = true;
        EventCamera.instance.StartEventCamera(gameObject, () => waitFlag = false);
        while (waitFlag) { yield return null; }
    }

    public void Stop()
    {
        isMove = false;
    }

    public void Restart()
    {
        isMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        // モンスターを動かすかの判定
        if ((!ShowScript.instance.GetIsShow() || isCamera) && isMove)
        {
            // モンスターの移動と拡大
            MonsterMove();
        }
        if (!ShowScript.instance.GetIsShow() && isCamera && Mathf.Abs(PlayerController.instance.transform.position.x - transform.position.x) < 15)
        {
            EventCamera.instance.EndEventCamera();
            isCamera = false;
        }
    }

    /// <summary>
    /// モンスターをTranslateで移動させるスクリプト　時間で加速、大きくなる
    /// </summary>
    void MonsterMove()
    {
        // 増加率(growing)*適当な値分増加する。growingで調整可能
        float bigger  = 0.00025f * growing;
        // 画像の縦サイズを取得する
        float sizeY = monsterSPR.bounds.size.y;
        // 縦と横に少しずつ増加
        transform.localScale += new Vector3(transform.localScale.x > 0 ? bigger : -bigger, bigger, 0);
        // 現在のサイズから上記で取得した縦サイズを引いて差を取得する
        float difSizeY = monsterSPR.bounds.size.y - sizeY;
        //  モンスターの移動に加速する値を追加していく　yにはy軸に拡大した分の二分の一上げていく
        transform.Translate((monsterSpeed * TimeManager.DeltaTime) + (TimeManager.DeltaTime * nowAcce), difSizeY/2, 0);
        nowAcce = acceleration < 0 ?
            Mathf.Clamp(nowAcce + TimeManager.DeltaTime * acceleration / acceTime, acceleration, 0) :
            Mathf.Clamp(nowAcce + TimeManager.DeltaTime * acceleration / acceTime, 0, acceleration);
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

    private void PlayerHit(GameObject target)
    {
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
    /// <summary>
    /// time秒までに徐々に透明になる
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator MonsterAlpha(float time)
    {
        int x = 0;
        Color mc = monsterSPR.color;
        float alpha = mc.a;
        float num = mc.a / (time / Time.deltaTime);
        while (time > 0)
        {
            alpha -= num;
            monsterSPR.color = new Color(mc.r, mc.g, mc.b, alpha);
            time -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        monsterSPR.color = new Color(mc.r, mc.g, mc.b, 0);
        yield return new WaitForSeconds(time);
    }

    private Animator GetAnim()
    {
        return transform.GetChild(0).GetComponent<Animator>();
    }
}
