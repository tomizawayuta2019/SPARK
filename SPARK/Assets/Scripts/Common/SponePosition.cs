using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponePosition : MonoBehaviour {
    [SerializeField] StagePosition position;

    private void Awake()
    {
        transform.position = position.GetPosition();
    }
}
