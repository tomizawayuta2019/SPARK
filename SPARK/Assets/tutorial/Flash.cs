using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour {
    float time;
    float red = 0.0f;
    float green = 0.0f;
    float blue = 0.0f;
    public float alfa = 0.0f;

    Image image;

    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();

        red = image.color.r;
        green = image.color.g;
        blue = image.color.b;
        image.color = new Color(red, green, blue, alfa);
    }
	void resetTime()
    {
        time = 0;
    }
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > 0.4f)
        {
            time = 0;
            if (alfa == 0)
            {
                alfa = 1;
                image.color = new Color(red, green, blue, alfa);
            }
            else
            {
                alfa = 0;
                image.color = new Color(red, green, blue, alfa);
            }
        }
	}
}
