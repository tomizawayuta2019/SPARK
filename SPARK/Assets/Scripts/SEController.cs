using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController : SingletonMonoBehaviour<SEController> {
    public enum SEType {
        none = -1,
        button,
        turnOff,
        Unlock,
        engineLever,
        bikkuri,
        match,
        message,
        monster,
        voise,
        doll,
        run,
        door_metal,
        door_erec,
        light,
        wall,
        walk,
        bird,
    }

    public List<AudioClip> SEList;

    public AudioSource PlaySE(SEType type,bool isLoop = false) {
        AudioSource se = new GameObject(type.ToString()).AddComponent<AudioSource>();
        se.clip = SEList[(int)type];
        se.transform.SetParent(transform);
        se.gameObject.AddComponent<SEScript>();
        se.loop = isLoop;
        se.Play();

        return se;
    }
}

public class SEScript : MonoBehaviour {
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!audioSource.isPlaying) { Destroy(gameObject); }
    }
}
