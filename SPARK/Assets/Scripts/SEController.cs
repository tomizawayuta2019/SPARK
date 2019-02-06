using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController : SingletonMonoBehaviour<SEController> {
    public enum SEType {
        none = -1,
        button = 0,
        turnOff = 1,
        Unlock = 2,
        engineLever = 3,
        bikkuri = 4,
        match = 5,
        message = 6,
        monster = 7,
        voise = 8,
        doll = 9,
        run = 10,
        door_metal,
        door_erec,
        light,
        wall,
        walk = 15,
        bird,
        get_item,
        close_door,
        car,
        car_break = 20,
        car_shoutotu,
        knife_drop = 22,
        knife_wind,
        knife_cut,
        rat_walk = 25,
        noise,
        piano_key,
        piano_open,
        dialy_page,
        phone_call,
        phone_get,
        monster_bite,
        monster_walk,
        set_light,
        movie_walk,
        drop_brooch,

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
