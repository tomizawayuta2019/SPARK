using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCController : SingletonMonoBehaviour<ESCController> {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetSceneAt(0).buildIndex != 1) {
            PushESC();
        }
	}

    public void PushESC()
    {
        FadeManager.FadeState fade = new FadeManager.FadeState().Init();
        fade.outComp = () => SceneManager.LoadScene(0);
        FadeManager.instance.FadeStart(fade);
    }
}
