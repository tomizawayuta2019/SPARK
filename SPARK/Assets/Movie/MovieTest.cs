using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MovieTest : MonoBehaviour {
    [SerializeField] VideoPlayer movie;

    bool isStartMovie;

    [ContextMenu("Play")]
    public void Play()
    {
        movie.Play();
    }

    private void Update()
    {
        if (movie.isPlaying) { isStartMovie = true; }

        if (Input.GetKeyDown(KeyCode.Escape) || (isStartMovie && !movie.isPlaying))
        {
            GetComponent<SceneMoveScript>().SceneMove("GameMain");
        }
    }
}
