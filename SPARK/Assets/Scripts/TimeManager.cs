using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム内の時間管理用クラス
/// </summary>
public static class TimeManager {
    private static float timePer = 1.0f;
    private static bool isStop = false;

    /// <summary>
    /// 前回のフレームからの経過時間
    /// </summary>
    public static float DeltaTime {
        get {
            if (isStop) { return 0; }
            return Time.deltaTime * timePer;
        }
    }

    /// <summary>
    /// ゲームの一時停止
    /// </summary>
    public static void GameStop() {
        isStop = true;
    }

    /// <summary>
    /// ゲームの再開
    /// </summary>
    public static void GameStart() {
        isStop = false;
    }

    /// <summary>
    /// ゲームの速度変更
    /// </summary>
    /// <param name="speed"></param>
    public static void GameSpeedChange(float speed) {
        if (speed < 0) { speed = 0; }
        timePer = speed;
    }
}
