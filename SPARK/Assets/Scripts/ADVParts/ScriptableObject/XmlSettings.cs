using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SPARK/Create XmlSettings", fileName ="XmlSettings")]
public class XmlSettings : ScriptableObject
{
    public TextAsset[] xml;
    public ShowScript.ADVType[] advTypes;
}
