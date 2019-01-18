using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour {

    public Sprite[] endMovies = new Sprite[13];
    public GameObject nextImage;
    public GameObject Camera;
    private SpriteRenderer endSP;


    private void Start()
    {
        endSP = GetComponent<SpriteRenderer>();
        StartCoroutine(EndingMovie());
    }

    public IEnumerator EndingMovie()
    {
        Color mc = endSP.color;
        int count = 0;
        float alpha = 1;
        //Debug.Log(gameObject.transform.localPosition.y);
        nextImage.GetComponent<SpriteRenderer>().sprite = endMovies[count+1];
        while (count<12)
        {   
            while (alpha >= 0)
            {
                endSP.color = new Color(mc.r, mc.g, mc.b, alpha);
                alpha -= (count > 2)? 2*Time.deltaTime:0.5f*Time.deltaTime;
                if (gameObject.transform.localPosition.y< 10f)
                {
                    gameObject.transform.Translate(0,3*Time.deltaTime,0);
                    nextImage.gameObject.transform.position = gameObject.transform.position;
                } 
                yield return new WaitForSeconds(Time.deltaTime);
            }
            count++;
            alpha = 1;
            nextImage.GetComponent<SpriteRenderer>().sprite =(count!=12) ? endMovies[count + 1]: endMovies[count];
            endSP.sprite = endMovies[count];
        }
        endSP.sprite = endMovies[12];

        //2秒
        //yield return new WaitForSeconds(Time.deltaTime * 60 * 2);
    }
}
