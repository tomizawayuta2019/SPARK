using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * カラスだよ
 */ 
public class Clows : GimmickKind {
    //
    Vector3 playerPos;
    [SerializeField]
    GameObject clowsObj;
    public enum Fly
    {
        left = 1,
        up,
        right,
    }

    private struct Bird
    {
        public GameObject clow;
        public Fly type;
        public Animator Anime;
    }

    List<Bird> birds =new List<Bird>();
    

    private void Start()
    {
        int count = 0;
        foreach (Transform obj in clowsObj.transform)
        {
            birds.Add(new Bird() {
                clow = obj.gameObject,
                type = (count < 3) ? Fly.left : (count < 8) ? Fly.up : Fly.right,
                Anime = obj.gameObject.GetComponent<Animator>() });
            count++;
        }
        
    }

    public override void Click()
    {
        FlyAway();
        //SEController.instance.PlaySE(SEController.SEType.bird);
        //playerPos = PlayerController.instance.gameObject.transform.position;

    }
    // これで動かす
    public void ClowMoves()
    {
        FlyAway();
    }


    public void FlyAway()
    {
        float time = 0f;
        foreach(Bird bird in birds)
        {
           
            Debug.Log("obj="+bird.clow +"\nfloat=" + (float)bird.type);
            time = Random.Range(0.0f,1.0f);
            StartCoroutine(FlyClow(time, bird));
        }
    }
    IEnumerator FlyClow(float wait,Bird bird)
    {
        
        yield return new WaitForSeconds(wait);
        if ((float)bird.type <= 1) { bird.clow.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = true; }
        bird.Anime.SetInteger("FlyType", (int)bird.type);
        yield return new WaitForSeconds(1);
        bird.clow.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = false;
        bird.Anime.SetInteger("FlyType", 0);

    }
}

