using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveScript : MonoBehaviour {

    public void SceneMove(int sceneNumber) {
        FadeManager.instance.FadeStart(new FadeManager.FadeState() { fadeTime = FadeManager.instance.defaultFadeTime, outComp = () => SceneManager.LoadScene(sceneNumber) });
    }

    public void SceneMove(string sceneName)
    {
        FadeManager.instance.FadeStart(new FadeManager.FadeState() { fadeTime = FadeManager.instance.defaultFadeTime, outComp = () => SceneManager.LoadScene(sceneName) });
    }
}
