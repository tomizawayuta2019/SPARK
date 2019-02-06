using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {

    [SerializeField] Vector2 speed;
    
	// Update is called once per frame
	void Update () {
        transform.Translate(speed * TimeManager.DeltaTime);
	}
}
