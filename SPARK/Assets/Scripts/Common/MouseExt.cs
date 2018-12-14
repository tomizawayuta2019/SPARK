using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseExt {

    private static List<float> preTime = new List<float>() { 0, 0, 0 };
    private static List<GameObject> list = new List<GameObject>() { null, null, null };

    public static T GetMousePosGimmick<T>()
    {
        if (IsSameFrame(0)) { return list[0].GetComponent<T>(); }

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

        if (hit.transform != null) { list[0] = hit.collider.gameObject; }
        else { list[0] = null; }
        return gimmick;
    }

    public static GameObject GetMousePosGameObject(System.Func<GameObject,bool> func) {
        if (IsSameFrame(1)) { return list[1]; }
        //クリックされた位置を取得
        var tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject obj = null;
        RaycastHit2D hit;
        List<GameObject> hitObjects = new List<GameObject>();

        do
        {
            hit = Physics2D.Raycast(tapPoint, -Vector3.up);
            if (hit.collider == null) { break; }
            if (func(hit.collider.gameObject)) { obj = hit.collider.gameObject; }
            hit.collider.gameObject.SetActive(false);
            hitObjects.Add(hit.collider.gameObject);
        } while (obj == null);

        foreach (var item in hitObjects) { item.SetActive(true); }

        list[1] = obj;
        return obj;
    }

    private static bool IsSameFrame(int num) {
        if (preTime[num] == Time.time)
        {
            return true;
        }
        else {
            preTime[num] = Time.time;
            return false;
        }
    }

}
