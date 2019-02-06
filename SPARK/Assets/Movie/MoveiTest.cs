using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MoveiTest : MonoBehaviour {
    [SerializeField] VideoPlayer movie;

    [ContextMenu("Play")]
    public void Play()
    {
        movie.Play();
    }
}
