using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyInputExt {
#if UNITY_EDITOR
    /// <summary>
    /// 押されている数字ボタンの番号を取得する処理（同時押しなら若い方優先）
    /// </summary>
    /// <returns></returns>
    public static int? GetKeypadNumber() {
        for (int i = 256; i <= 265; i++) {
            if (Input.GetKey((KeyCode)i)) { Debug.Log(i); return i - 256; }
            Debug.Log((KeyCode)i);
        }
        return null;
    }
#endif
}
