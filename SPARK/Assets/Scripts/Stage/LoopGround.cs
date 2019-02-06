using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一定距離まで歩いたら、元の場所に戻る処理
/// </summary>
public class LoopGround : MonoBehaviour {

    /// <summary>
    /// ワープ元
    /// </summary>
    [SerializeField]
    float loopFromX;

    /// <summary>
    /// ワープ先
    /// </summary>
    [SerializeField]
    float loopForX;

    bool isLoopLeftSide;

    [SerializeField]
    List<BackGroundScript> BG;

    PlayerController player;

    List<GameObject> targets = new List<GameObject>();

    private void Awake()
    {
        isLoopLeftSide = loopFromX < loopForX;
    }

    private void LateUpdate()
    {
        if (player == null) { player = PlayerController.instance; }
        if (isLoopLeftSide)
        {
            if (player.transform.position.x < loopFromX) { Loop(); }
        }
        else {
            if (player.transform.position.x > loopFromX) { Loop(); }
        }
    }

    private void Loop() {
        Vector3 pos = player.transform.position;
        float distanceX = loopForX - pos.x;
        foreach (var bg in BG) {
            bg.LoopBackGround(distanceX);
        }
        foreach (var go in targets) {
            if (go == null) { continue; }
            go.transform.position += new Vector3(distanceX, 0, 0);
        }
        pos.x = loopForX;
        player.transform.position = pos;
        GameController.instance.GetCameraController().CameraUpdate();
        CameraViewChangeObject.UpdateCameraView();
    }

    public void SetLoopObject(GameObject target)
    {
        targets.Add(target);
    }
}
