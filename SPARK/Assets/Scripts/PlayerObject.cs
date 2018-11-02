using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : SingletonMonoBehaviour<PlayerObject> {
    Rigidbody2D rg;
    Vector3 move = new Vector3();

    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow)) { move.x += 3 * Time.deltaTime; }
        if (Input.GetKey(KeyCode.LeftArrow)) { move.x -= 3 * Time.deltaTime; }
    }

    private void FixedUpdate()
    {
        if (move == new Vector3()) { return; }
        transform.position = transform.position + move;
        rg.WakeUp();
        move = new Vector3();
    }

    protected override void Awake() {
		base.Awake();
	}

	protected override void OnDestroy() {
		base.OnDestroy();
	}

}