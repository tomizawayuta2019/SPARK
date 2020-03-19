using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StagePosition {

    [Flags]
    public enum PositionType {
        //Local,  //ローカル座標（プレイヤーからの相対位置）
        LocalX = 1 << 0,
        LocalY = 1 << 1,
        LocalZ = 1 << 2,
    }

    [SerializeField]
    [EnumFlags]
    PositionType type;
    [SerializeField]
    Vector3 pos;

    /// <summary>
    /// この構造体で指定されたポジションを受け取る処理
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPosition() {
        Vector3 pos = this.pos;
        Vector3 pPos = PlayerController.instance.transform.position;

        if ((type & PositionType.LocalX) != 0) { pos.x += pPos.x; }
        if ((type & PositionType.LocalY) != 0) { pos.y += pPos.y; }
        if ((type & PositionType.LocalZ) != 0) { pos.z += pPos.z; }
        
        return pos;
    }
}