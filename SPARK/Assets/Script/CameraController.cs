using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public int cameraMoveFlag;
    public GameObject Player;
    public Vector2 cameraPosition;
	// Use this for initialization
	void Start () {
        cameraMoveFlag = Player.GetComponent<PlayerController>().PlayerMoveFlag;
        cameraPosition = this.transform.position;
    }
    public void CameraMove(int moveFlag)
    {
        if (moveFlag == 1)
        {
            cameraPosition = Player.transform.position;
            this.transform.position = new Vector3(cameraPosition.x, transform.position.y,-10);
        }
    }

    // Update is called once per frame
    public void CameraUpdate () {
        cameraMoveFlag = Player.GetComponent<PlayerController>().PlayerMoveFlag;
        CameraMove(cameraMoveFlag);
    }
}
