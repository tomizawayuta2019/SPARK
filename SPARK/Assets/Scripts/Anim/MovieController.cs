using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieController : SingletonMonoBehaviour<MovieController> {
    [SerializeField] Ending ending;

    public void StartEndingMovie()
    {
        ending.gameObject.SetActive(true);
    }
}
