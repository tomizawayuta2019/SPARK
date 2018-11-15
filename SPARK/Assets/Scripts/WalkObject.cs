using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkObject : MonoBehaviour {
    [SerializeField]
    AudioSource walkAudio;

    Vector3 defPos;

    private void Start()
    {
        defPos = transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (Vector3.Distance(defPos, transform.position) > 0.001f)
        {
            if (!walkAudio.isPlaying) {
                walkAudio.Play();
            }
        }
        else {
            walkAudio.Stop();
        }
        defPos = transform.position;
	}
}
