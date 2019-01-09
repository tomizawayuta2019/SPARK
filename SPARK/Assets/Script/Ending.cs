using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour {

    public Sprite[] endMovies = new Sprite[13];
    public GameObject nextImage; 
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
        float alpha = 255;
        nextImage.GetComponent<SpriteRenderer>().sprite = endMovies[count+1];
        while (count<12)
        {
            
            while (alpha >= 50)
            {
                Debug.Log("どこでバグ？");
                endSP.color = new Color(mc.r, mc.g, mc.b, alpha);
                alpha -= 10f;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            count++;
            alpha = 255;
            nextImage.GetComponent<SpriteRenderer>().sprite = endMovies[count + 1];
            endSP.sprite = endMovies[count];
        }
        endSP.sprite = endMovies[12];

        //2秒
        //yield return new WaitForSeconds(Time.deltaTime * 60 * 2);
    }
}
