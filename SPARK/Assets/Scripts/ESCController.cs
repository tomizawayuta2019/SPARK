using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCController : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            FadeManager.FadeState fade = new FadeManager.FadeState().Init();
            fade.outComp = () => SceneManager.LoadScene(0);
            FadeManager.instance.FadeStart(fade);
        }
	}
}
