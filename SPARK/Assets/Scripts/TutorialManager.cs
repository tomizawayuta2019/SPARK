using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    enum TutorialType {
        move,       //移動説明
        getItem,    //アイテム取得
        ItemView,   //アイテム確認
        useItem,    //アイテム使用
        useGimmick  //ギミック操作
    }

    [System.Serializable]
    struct TutorialStatus {
        public TutorialType type;
        public string[] targets;
        public float value;
    }

    [SerializeField]
    TutorialStatus tutorials;

    [SerializeField]
    GameObject moveArrow;
    [SerializeField]
    GameObject getItemArrow;

    bool isTutorialEnd;
    GameObject[] targets;

    private void Update()
    {
        if (isTutorialEnd) {
            TutorialStart(tutorials);
        }
    }

    void TutorialStart(TutorialStatus status) {
        if (targets.Length != status.targets.Length) {
            targets = new GameObject[status.targets.Length];
        }

        for (int i = 0; i < status.targets.Length; i++) {
            if (targets[i] != null && targets[i].name == status.targets[i]) { continue; }
            targets[i] = GameObject.Find(status.targets[i]);
            if (targets[i] == null) { return; }
        }


        isTutorialEnd = false;
    }

    IEnumerator Tutorial_Move(TutorialStatus status) {
        GameObject player = targets[0];
        moveArrow.SetActive(true);
        float defPosX = player.transform.position.x;

        //プレイヤーが右側に規定値以上移動したらOK
        while (player.transform.position.x - defPosX <= status.value) {
            yield return null;
        }

        moveArrow.SetActive(false);
        isTutorialEnd = true;
    }

    IEnumerator Tutorial_GetItem(TutorialStatus status)
    {
        GameObject item = targets[0];

        getItemArrow.SetActive(true);
        getItemArrow.transform.position = item.transform.position + new Vector3(0, 3, 0);

        //プレイヤーが右側に規定値以上移動したらOK
        while (item != null)
        {
            yield return null;
        }

        getItemArrow.SetActive(false);
        isTutorialEnd = true;
    }

    IEnumerator Tutorial_ViewItem(TutorialStatus status)
    {
        GameObject item = targets[0];


        yield return null;
        isTutorialEnd = true;
    }

    IEnumerator Tutorial_UseItem(TutorialStatus status)
    {
        yield return null;
        isTutorialEnd = true;
    }

    IEnumerator Tutorial_UseGimmick(TutorialStatus status)
    {
        yield return null;
        isTutorialEnd = true;
    }
}
