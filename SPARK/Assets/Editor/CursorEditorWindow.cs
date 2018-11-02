using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CursorEditorWindow : EditorWindow {
    [MenuItem("Editor/CursorEditor")]
    static void Open() {
        GetWindow<CursorEditorWindow>();
    }

    private const string ASSET_PATH = "Assets/Resources/Data/CorsorScriptableObject.asset";
    public CursorScriptableObject target;
    

    void OnGUI() {
        if (!target) { Import(); }
        if (target.spriteSets == null)
        {
            target.spriteSets = new TextureSet[System.Enum.GetValues(typeof(CursorType)).Length];
            EnumExt.EnumForeach<CursorType>((value) => {
                target.spriteSets[(int)value].sprites = new Texture2D[2];
                target.spriteSets[(int)value].name = value.ToString();
            } );
        }

        Color defaultColor = GUI.backgroundColor;
        Color tabColor = Color.gray;

        using (new GUILayout.VerticalScope(EditorStyles.helpBox)) {
            GUI.backgroundColor = tabColor;

            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label("マウスカーソル設定");
            }

            GUI.backgroundColor = defaultColor;
        }

        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            if(GUILayout.Button("保存"))
            {
                Export();
            }
        }

        for (int i = 0; i < target.spriteSets.Length; i += 2) {
            using (new GUILayout.HorizontalScope(EditorStyles.helpBox,GUILayout.Height(130))) {
                //二行ごとに纏めて表示
                for (int j = i; j < i + 2 && j < target.spriteSets.Length; j++) {
                    GUI.backgroundColor = Color.green;
                    using (new GUILayout.VerticalScope(EditorStyles.toolbar))
                    {
                        GUILayout.Label(((CursorType)j).ToString());
                    }

                    GUI.backgroundColor = defaultColor;
                    for (int k = 0; k < 2; k++)
                    {
                        using (new GUILayout.VerticalScope(EditorStyles.toolbar))
                        {
                            GUILayout.Label(k == 0 ? "通常" : "押下");
                            target.spriteSets[j][k] = EditorGUILayout.ObjectField(target.spriteSets[j][k], typeof(Texture2D), false, GUILayout.Height(100), GUILayout.Width(100)) as Texture2D;
                        }
                    }
                }

                GUI.backgroundColor = defaultColor;
            }
        }
    }

    private void Export()
    {
        CursorScriptableObject sample = AssetDatabase.LoadAssetAtPath<CursorScriptableObject>(ASSET_PATH);
        if (!sample)
        {
            sample = CreateInstance<CursorScriptableObject>();
        }

        if (!AssetDatabase.Contains(sample as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            AssetDatabase.CreateAsset(sample, ASSET_PATH);
        }

        sample.Copy(target);

        sample.hideFlags = HideFlags.NotEditable;
        EditorUtility.SetDirty(sample);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void Import()
    {
        if (!target)
        {
            target = CreateInstance<CursorScriptableObject>();
        }

        CursorScriptableObject sample = AssetDatabase.LoadAssetAtPath<CursorScriptableObject>(ASSET_PATH);
        if (!sample) { return; }

        target.Copy(sample);
    }
}
