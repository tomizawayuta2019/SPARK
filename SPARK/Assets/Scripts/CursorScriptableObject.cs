using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScriptableObject : ScriptableObject {
    public TextureSet[] spriteSets;
    
#if UNITY_EDITOR
    public void Copy(CursorScriptableObject source)
    {
        spriteSets = source.spriteSets;
    }
#endif
}
