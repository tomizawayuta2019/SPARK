using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumExt {

    /// <summary>
    /// enumでループする用の関数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void EnumForeach<T>(System.Action<T> action) where T : struct {
        foreach (T value in System.Enum.GetValues(typeof(T))) {
            action(value);
        }
    }
}
