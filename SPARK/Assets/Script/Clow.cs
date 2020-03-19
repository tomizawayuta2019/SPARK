using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clow : GimmickKind {

    float Count;
    public override void Click()
    {
        gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        //SEController.instance.PlaySE(SEController.SEType.bird,false);

    }
    private void Start()
    {
        Count = Random.Range(0, 4);
    }
    // Update is called once per frame
    void Update () {
        Count += Random.Range(0f, 1.1f) * Time.deltaTime;
        if (Count >= 4.0f)
        {
            Vector3 scale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            Count = 0;
        }
	}

}
