using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景（遠景・近景）表示と動作用クラス
/// </summary>
public class BackGroundScript : MonoBehaviour {
    public Camera targetCamera;
    public GameObject Player;
    public SpriteRenderer back;
    //Vector3 backDefPos;
    public SpriteRenderer front;
    //Vector3 frontDefPos;
    [Range(0f,1)]
    public float speed = 1;

    private GameObject backObj, frontObj;
    private Vector3 backDefPos, /*frontDefPos,*/ cameraDefPos;

    [SerializeField]
    List<OnCameraVisible> frontVisible = new List<OnCameraVisible>();
    float frontSize;
    [SerializeField]
    List<OnCameraVisible> backVisible = new List<OnCameraVisible>();
    float backSize;

    [SerializeField]
    public float cameraRange;

    private void Start()
    {
        backObj = new GameObject("back");
        frontObj = new GameObject("front");
        back.transform.SetParent(backObj.transform);
        front.transform.SetParent(frontObj.transform);
        backObj.transform.SetParent(transform);
        frontObj.transform.SetParent(transform);
        

        backDefPos = backObj.transform.localPosition;
        //frontDefPos = front.transform.position;
        cameraDefPos = targetCamera.transform.position;


        frontVisible.Add(front.gameObject.AddComponent<OnCameraVisible>());
        backVisible.Add(back.gameObject.AddComponent<OnCameraVisible>());
        frontVisible[0].Init(this);
        backVisible[0].Init(this);
        Init();
    }

    private void Init() {
        VisivleListInit(frontVisible);
        VisivleListInit(backVisible);
    }

    private void VisivleListInit(List<OnCameraVisible> target) {
        bool contFlag = false;
        int count = 0;
        do
        {
            count++;
            contFlag = false;
            //左端と右端のオブジェクト両方が画面外に出るまで生成を続ける
            if (target[0].IsVisible)
            {
                Add(target, true);
                contFlag = true;
            }
            if (target[target.Count - 1].IsVisible)
            {
                Add(target, false);
                contFlag = true;
            }
        } while (contFlag && count < 50);
        if (count == 50) { Debug.LogError("無限ループ発生してます"); }
    }

    private void VisivleListUpdate(List<OnCameraVisible> target)
    {
        if (target.Count < 2) {
            Debug.LogError("対象のリストが小さすぎます");
            Add(target, true);
            Add(target, false);
            //return;
        }
        bool contFlag = false;
        do
        {
            contFlag = false;
            //左端の要素が画面外にあるなら、左端に要素を追加
            if (target[0].IsVisible)
            {
                contFlag = true;
                Add(target, true);
            }
            //左端の二つの要素が画面外に出ていたら、一番左の要素を削除
            else if (!target[1].IsVisible)
            {
                contFlag = true;
                Remove(target, true);
            }

            //右端の要素が画面外にあるなら、右端に要素を追加
            if (target[target.Count - 1].IsVisible) {
                contFlag = true;
                Add(target, false);
            }
            //右端の二つの要素が画面外に出ていたら、一番右の要素を削除
            else if (!target[target.Count - 2].IsVisible)
            {
                contFlag = true;
                Remove(target, false);
            }

        } while (contFlag);
    }

    private void Add(List<OnCameraVisible> target,bool isHead) {
        OnCameraVisible newItem = Instantiate(target[0]);
        newItem.transform.SetParent(target[0].transform.parent);
        newItem.Init(this);
        target.Add(newItem);
        float sizeX = newItem.SizeX;
        Vector3 pos;
        if (isHead) {
            pos = target[0].transform.position;
            pos.x -= sizeX;
        }
        else {
            pos = target[target.Count - 2].transform.position;//追加前の末尾を参照
            pos.x += sizeX;
        }
        newItem.transform.position = pos;

        target.Sort((a, b) => (int)(a.transform.position.x - b.transform.position.x));
    }

    private void Remove(List<OnCameraVisible> target,bool isHead) {
        OnCameraVisible removeItem;
        if (isHead) {
            removeItem = target[0];
        }
        else {
            removeItem = target[target.Count - 1];
        }
        target.Remove(removeItem);
        Destroy(removeItem.gameObject);
    }

    // Update is called once per frame
    void Update () {
        Vector3 move = Vector3.zero;
        //デバッグ用
        if (Input.GetKey(KeyCode.LeftArrow)) { move += new Vector3(-10, 0, 0) * Time.deltaTime; }
        if (Input.GetKey(KeyCode.RightArrow)) { move += new Vector3(10, 0, 0) * Time.deltaTime; }
        Player.transform.position = Player.transform.position + move;

        Vector3 pos = backDefPos + ((targetCamera.transform.position - cameraDefPos) * speed);
        backObj.transform.localPosition = pos;

        VisivleListUpdate(frontVisible);
        VisivleListUpdate(backVisible);
    }
}

/// <summary>
/// 背景として表示するオブジェクトのデータ取得用クラス
/// </summary>
public class OnCameraVisible : MonoBehaviour {
    [SerializeField]
    public SpriteRenderer sprite;
    private BackGroundScript parent;
    private const float delta = 1f;
    public bool IsVisible { get {
            float posXDelta = parent.targetCamera.transform.position.x - transform.position.x;
            return Mathf.Abs(posXDelta) < delta + parent.cameraRange;
        } }

    private float? sizeX = null;
    public float SizeX {
        get {
            if (!sizeX.HasValue) {
                //sizeX = transform.lossyScale.x * sprite.sprite.rect.size.x / 50;
                sizeX = sprite.bounds.size.x;
            }
            return sizeX.Value;
        }
    }

    public void Init(BackGroundScript parent) {
        this.parent = parent;
        if (!sprite)
        {
            sprite = GetComponent<SpriteRenderer>();
        }
    }
}