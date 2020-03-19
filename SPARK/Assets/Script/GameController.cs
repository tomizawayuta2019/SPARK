﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonMonoBehaviour<GameController> {
    public Vector3 mousePostion;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    ItemBagController itemBagController;
    [SerializeField]
    GameOverCanvas gameOver;
	
	// Update is called once per frame
	void Update () {
        mousePostion = Input.mousePosition;
        playerController.PlayerUpdata();
        cameraController.CameraUpdate();
        itemBagController.ItemBagUpdate();
    }

    public void GameOver() {
        gameOver.gameObject.SetActive(true);
        StartCoroutine(Wait());
    }

    public void Restart()
    {
        gameOver.gameObject.SetActive(false);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        ESCController.instance.ReswapnCheck(GimmickMonster.Instance);
    }

    public CameraController GetCameraController() {
        return cameraController;
    }
}
