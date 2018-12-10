using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ScriptableObject試作

[CreateAssetMenu(menuName = "SPARK/Create ScenarioDataTable", fileName = "ScenarioDataTable")]
public class ScenarioDataTable : ScriptableObject
{
    [SerializeField]
    private int[] life;
    [SerializeField]
    private List<int> MaxLife = new List<int>();
    [SerializeField]
    private List<ScenarioData> data = new List<ScenarioData>();
}
