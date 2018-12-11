using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedChange<T> : MonoBehaviour {
    [SerializeField]
    public T start, end;

    [SerializeField]
    public float time, loopDelay;

    [SerializeField]
    public bool isPlayOnAwake, isLoop, isBouns;

    System.Action cmp;

    protected virtual void Awake()
    {
        if (!isPlayOnAwake) { return; }
        Play();
    }

    private void OnEnable()
    {
        if (!isPlayOnAwake) { return; }
        Play();
    }

    public void Play()
    {
        StartCoroutine(RotateChangeDelay(start, end));
    }

    public void Play(System.Action cmp) {
        this.cmp = cmp;
        Play();
    }

    public void Stop() {
        StopAllCoroutines();
    }

    /// <summary>
    /// 値の更新
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected virtual T UpdateValue(T value) {
        return value;
    }

    /// <summary>
    /// 時間による値の計算
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="per"></param>
    /// <returns></returns>
    protected virtual T LerpValue(T start,T end,float per) {
        return start;
    }

    IEnumerator RotateChangeDelay(T start, T end)
    {
        //Vector3 rot = transform.eulerAngles;
        //rot.z = start;
        T startValue = start;
        UpdateValue(startValue);
        T value = startValue;

        float nowTime = Time.deltaTime;
        while (nowTime < time)
        {
            value = LerpValue(start,end,nowTime / time);
            UpdateValue(value);
            yield return null;
            nowTime += Time.deltaTime;
        }

        value = end;
        UpdateValue(value);

        if (isLoop)
        {
            yield return new WaitForSeconds(loopDelay);
            if (isBouns)
            {
                StartCoroutine(RotateChangeDelay(end, start));
            }
            else
            {
                StartCoroutine(RotateChangeDelay(start, end));
            }
        }
        else if(cmp != null){
            cmp();
        }
    }
}
