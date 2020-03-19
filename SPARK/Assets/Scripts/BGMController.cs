using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : SingletonMonoBehaviour<BGMController> {

    public enum BGMType
    {
        yakei,
        sinzitu,
        huyunohi,
    }

    [SerializeField] AudioSource source;
    [SerializeField] List<AudioClip> clips;

    public void SetBGM(BGMType type)
    {
        if (source.clip == null)
        {
            source.clip = clips[(int)type];
            source.Play();
        }
        else
        {
            StartCoroutine(Event(clips[(int)type]));
        }
    }

    private IEnumerator Event(AudioClip clip)
    {
        yield return StartCoroutine(VolumeChange(source, 1, 0));
        source.clip = clip;
        source.Play();
        yield return StartCoroutine(VolumeChange(source, 0, 1));
    }

    private IEnumerator VolumeChange(AudioSource target,float from,float to,float time = 1)
    {
        float delta = to - from;
        float nowTime = TimeManager.DeltaTime;

        while (nowTime < time)
        {
            target.volume = from + (delta * nowTime / time);
            yield return null;
            nowTime += TimeManager.DeltaTime;
        }

        target.volume = to;
    }

}
