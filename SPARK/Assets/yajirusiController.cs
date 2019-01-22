using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class yajirusiController : MonoBehaviour {
    [SerializeField]
    GameObject pointer;
    [SerializeField]
    itemController target;
    [SerializeField]
    ItemBagController ItemBagController;
    [SerializeField]
    GameObject yajirushi;
    [SerializeField]
    GameObject itemViewImage;

    Vector3 gob1Position = Vector3.zero;

    //int YajirusiFlag = 0;
    public void YajirusiController()
    {
        ///ui
        if (target != null)
        {
            if (target.state.itemName == "ロウソク" && ItemBagController.itemsContains("マッチ") != null)
            {
                yajirushi.SetActive(true);
                gob1Position = ItemBagController.itemsContains("マッチ").transform.position;
                setYajirusi(gob1Position, itemViewImage.transform.position, 500.0f, 200.0f);
            }
            else if (target.state.itemName == "灯篭" && ItemBagController.itemsContains("ロウソク") != null && ItemBagController.itemsContains("マッチ") == null)
            {
                yajirushi.SetActive(true);
                gob1Position = ItemBagController.itemsContains("ロウソク").transform.position;
                setYajirusi(gob1Position, itemViewImage.transform.position, 500.0f, 200.0f);
            }
            else
            {
                yajirushi.SetActive(false);
            }
        }
    }

    
    //positionにある、targetを指し、widthは横、heightは高さ
    public void setYajirusi(GameObject target,Vector3 position,float width,float height)
    {
        yajirushi.transform.position = position;
        yajirushi.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        yajirushi.transform.right = target.transform.position - yajirushi.transform.position;
    }

    //gob1とgob2の真ん中にある、gob2に指し、widthは横、heightは高さ
    /*例:
     * GameObject yaji = Instantiate(yajirusi,canvas.transform) as GameObject;
     * yaji.GetComponent<yajirusiController>().setYajirusi(Gob1, Gob2, 400.0f, 100.0f);
     */
    public void setYajirusi(GameObject gob1,GameObject gob2, float width, float height)
    {
        yajirushi.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        yajirushi.transform.position = (gob1.transform.position+gob2.transform.position)/2;
        yajirushi.transform.right = gob2.transform.position - yajirushi.transform.position;
    }
    public void setYajirusi(Vector3 gob1Position, Vector3 gob2Position, float width, float height)
    {
        yajirushi.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        yajirushi.transform.position = (gob1Position + gob2Position) / 2;
        yajirushi.transform.right = gob2Position - gob1Position;
    }

    //
    public void destroyYajirusi(float time)
    {
        Destroy(this.gameObject, time);
    }



    private void Update()
    {
        this.target = this.gameObject.GetComponent<ItemView>().target;
        YajirusiController();

        //print(target.state.itemName);
    }
}
