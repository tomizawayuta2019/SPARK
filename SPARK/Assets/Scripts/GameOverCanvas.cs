using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCanvas :MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIController.instance.list.Add(gameObject);
	}
}
