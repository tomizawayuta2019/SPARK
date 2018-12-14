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
#if UNITY_EDITOR
            timePer = Input.GetKeyDown(KeyCode.UpArrow) ? timePer + 0.2f : Input.GetKeyDown(KeyCode.DownArrow) ? timePer - 0.2f : timePer;
            if (timePer < 0) { timePer = 0; }
#endif
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
