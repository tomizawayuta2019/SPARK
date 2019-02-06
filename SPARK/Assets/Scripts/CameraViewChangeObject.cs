using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewChangeObject : MonoBehaviour {
    private static List<CameraViewChangeObject> list = new List<CameraViewChangeObject>();

    protected virtual void Awake()
    {
        list.Add(this);
    }

    public virtual void Update() {

    }

    public virtual void LateUpdate() {

    }

    public static void UpdateCameraView() {
        list.RemoveAll((item) => item == null);
        foreach (var item in list) {
            item.Update();
        }

        foreach (var item in list)
        {
            item.LateUpdate();
        }
    }

}
