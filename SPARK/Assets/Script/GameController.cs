using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public Vector3 mousePostion;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    ItemBagControllr itemBagController;
	
	// Update is called once per frame
	void Update () {
        mousePostion = Input.mousePosition;
        playerController.PlayerUpdata();
        cameraController.CameraUpdate();
        itemBagController.ItemBagUpdate();
    }
}
