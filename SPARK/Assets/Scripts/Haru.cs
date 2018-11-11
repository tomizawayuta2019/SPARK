using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haru : MonoBehaviour {
    [SerializeField]
    float speed;

    [SerializeField]
    GameObject haruADV;

    [SerializeField]
    protected bool isMoveStart;

    private void Start()
    {
        if (isMoveStart) { MoveStart(); }
    }

    public virtual void MoveStart() {
        isMoveStart = true;
        UIController.instance.list.Add(gameObject);
    }

    // Update is called once per frame
    void Update () {
        if (!isMoveStart) { return; }

        Vector3 gotoPos = transform.position + new Vector3(10, 0, 0);
        transform.position = Vector3.MoveTowards(transform.position, gotoPos, speed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - gotoPos.x) < 0.01f) {
            MoveEnd();
        }
	}

    private void OnBecameInvisible()
    {
        MoveEnd();
    }

    protected virtual void MoveEnd() {
        Destroy(gameObject);
        UIController.instance.list.Remove(gameObject);
        haruADV.SetActive(true);
    }

}
