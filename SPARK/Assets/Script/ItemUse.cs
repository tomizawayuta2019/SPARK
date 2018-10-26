using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour {
    public GameObject player;
    public Vector3 item_MousePosition;
    // Use this for initialization

    void ItemUseIt()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        item_MousePosition = Input.mousePosition;
	}
}
