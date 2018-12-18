using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class yajirusiController : MonoBehaviour {

    //positionにある、targetを指し、widthは横、heightは高さ
    public void setYajirusi(Vector3 target,Vector3 position,float width,float height)
    {
        this.transform.position = position;
        this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        transform.right = target - transform.position;
    }

    //gob1とgob2の真ん中にある、gob2に指し、widthは横、heightは高さ
    /*例:
     * GameObject yaji = Instantiate(yajirusi,canvas.transform) as GameObject;
     * yaji.GetComponent<yajirusiController>().setYajirusi(Gob1, Gob2, 400.0f, 100.0f);
     */
    public void setYajirusi(GameObject gob1,GameObject gob2, float width, float height)
    {
        this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        this.transform.position = (gob1.transform.position+gob2.transform.position)/2;
        transform.right = gob2.transform.position - transform.position;
    }

    //
    public void destroyYajirusi(float time)
    {
        Destroy(this.gameObject, time);
    }
}
