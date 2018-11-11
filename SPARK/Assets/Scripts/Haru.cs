using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haru : MonoBehaviour {
    [SerializeField]
    float speed;
    [SerializeField]
    Vector3 gotoPos;

    private void Start()
    {
        gotoPos.y = transform.position.y;
        gotoPos.z = transform.position.z;

        UIController.instance.list.Add(gameObject);
    }

    // Update is called once per frame
    void Update () {
        transform.position = Vector3.MoveTowards(transform.position, gotoPos, speed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - gotoPos.x) < 0.01f) {
            MoveEnd();
        }
	}

    private void MoveEnd() {
        Destroy(gameObject);
        UIController.instance.list.Remove(gameObject);
    }

}
