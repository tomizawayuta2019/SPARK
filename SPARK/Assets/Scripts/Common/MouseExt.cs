using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseExt {

    private static float preTime;

    public static T GetMousePosGimmick<T>()
    {
        if (IsSameFrame()) { return default(T); }

        //クリックされた位置を取得
        var tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        T gimmick = default(T);
        RaycastHit2D hit;
        List<GameObject> hitObjects = new List<GameObject>();

        do
        {
            hit = Physics2D.Raycast(tapPoint, -Vector3.up);
            if (hit.collider == null) { break; }
            gimmick = hit.collider.GetComponent<T>();
            hit.collider.gameObject.SetActive(false);
            hitObjects.Add(hit.collider.gameObject);
        } while (gimmick == null);

        foreach (var item in hitObjects) { item.SetActive(true); }

        return gimmick;
    }

    private static bool IsSameFrame() {
        if (preTime == Time.time)
        {
            return true;
        }
        else {
            preTime = Time.time;
            return false;
        }
    }

}
