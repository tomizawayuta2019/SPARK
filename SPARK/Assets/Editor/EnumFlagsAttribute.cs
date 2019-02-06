﻿using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
public sealed class EnumFlagsAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label) {
        prop.intValue = EditorGUI.MaskField(
            position,
            label,
            prop.intValue,
            prop.enumNames
        );
    }
}
#endif