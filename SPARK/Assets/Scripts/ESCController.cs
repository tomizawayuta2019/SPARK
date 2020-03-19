using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ESCController : SingletonMonoBehaviour<ESCController> {
    [SerializeField] Text text;
    [SerializeField] GameObject target;
    System.Action action;
    System.Action noAction;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(2))
        {
            switch (SceneManager.GetSceneAt(0).buildIndex)
            {
                case 1:
                    //text.text = "ムービーをスキップしますか？";
                    //action = PushSip;
                    //target.SetActive(true);
                    PushSip();
                    break;
                case 2:
                    text.text = "タイトルに戻りますか？";
                    action = PushESC;
                    target.SetActive(true);
                    break;
            }
        }
        //if (/*Input.GetKeyDown(KeyCode.Escape)*/ Input.GetMouseButtonDown(2) && SceneManager.GetSceneAt(0).buildIndex != 1) {
        //    PushESC();
        //}
	}

    public void ReswapnCheck(GimmickMonster monster)
    {
        text.text = "やり直しますか？";
        target.SetActive(true);
        action = () => 
        {
            PlayerController.instance.transform.position = monster.playerDefPos;
            monster.Start();
            LoopMonster loop = monster.gameObject.GetComponent<LoopMonster>();
            if (loop != null) { loop.HP = 3; }
            GameController.instance.Restart();
        };

        noAction = () =>
        {
            PushESC();
        };
    }

    public void PushESC()
    {
        FadeManager.FadeState fade = new FadeManager.FadeState().Init();
        fade.outComp = () => SceneManager.LoadScene(0);
        FadeManager.instance.FadeStart(fade);
    }

    public void PushSip()
    {
        FadeManager.FadeState fade = new FadeManager.FadeState().Init();
        fade.outComp = () => SceneManager.LoadScene(2);
        FadeManager.instance.FadeStart(fade);
    }

    public void PushEnter()
    {
        target.SetActive(false);
        action.Invoke();
        noAction = null;
        action = null;
    }

    public void PushNo()
    {
        target.SetActive(false);
        if (noAction != null) { noAction.Invoke(); } 
        action = null;
        noAction = null;
    }
}
